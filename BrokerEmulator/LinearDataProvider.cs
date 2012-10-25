using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using YahooStockEngine;
using System.Xml.Linq;
using System.Reflection;

namespace BrokerEmulator
{
    class TimedQuotes: SortedDictionary<DateTime, Quote> {};
    class DataStore: Dictionary<string, TimedQuotes> {};

    class LinearDataProvider
    {
        public String[] SymbolList {get; private set;}

        private DataStore dataStore;
        private Dictionary<string, TimedQuotes.Enumerator> enumeratorList;

        private TimedQuotes.Enumerator GetCurrentEnumerator(string symbol)
        {
            if (!enumeratorList.ContainsKey(symbol))
            {
                enumeratorList.Add(symbol, dataStore[symbol].GetEnumerator());
                MoveNext(symbol);
            }
            return enumeratorList[symbol];
        }

        public Quote GetCurrentQuote(string symbol)
        {
            return GetCurrentEnumerator(symbol).Current.Value;
        }

        public LinearDataProvider(String[] symbolList)
        {
            SymbolList = symbolList;

            dataStore = new DataStore();
            enumeratorList = new Dictionary<string, TimedQuotes.Enumerator>();

            foreach (string symbol in symbolList)
            {
                dataStore.Add(symbol, new TimedQuotes());
                TimedQuotes currentData = dataStore[symbol];
                string path = Common.PathToSymbol(symbol);
                foreach (string filePath in Directory.EnumerateFiles(path))
                {
                    DateTime time = Common.GetTimeFromPath(filePath);
                    Quote quote = StockDataParser.Parse(XElement.Load(filePath));
                    currentData.Add(time, quote);
                }

                SmoothData(currentData);
            }

            
        }

        //try best to replace a null field in a quote by the value from the previous quote
        private void SmoothData(TimedQuotes currentData)
        {
            Quote lastQuote = null;
            foreach (Quote quote in currentData.Values)
            {
                foreach (PropertyInfo property in quote.GetType().GetProperties())
                {
                    if (property.GetValue(quote, null) == null && lastQuote != null)
                    {
                        property.SetValue(quote, property.GetValue(lastQuote, null), null);
                    }
                }
                lastQuote = quote;
            }
        }

        private bool MoveNext(string symbol)
        {
            //Simply calling enumeratorList[symbol].MoveNext() does not work. Dont know why so this is a workaround.
            TimedQuotes.Enumerator enumerator = enumeratorList[symbol];
            bool result = enumerator.MoveNext();
            enumeratorList[symbol] = enumerator;
            return result;
        }

        //move the current enumerator to the earliest point after time where a quote is stored
        //return the value of that point
        public DateTime? MoveToTime(string symbol, DateTime time)
        {

            while (enumeratorList[symbol].Current.Key < time)
            {
                if (!MoveNext(symbol))
                {
                    return null;
                }
            }
            return enumeratorList[symbol].Current.Key;
        }

        public DateTime GetEarliestTime(string symbol)
        {
            return dataStore[symbol].ElementAt(0).Key;
        }

        public DateTime GetLatestTime(string symbol)
        {
            return dataStore[symbol].ElementAt(dataStore[symbol].Count - 1).Key;
        }
   
    }
}
