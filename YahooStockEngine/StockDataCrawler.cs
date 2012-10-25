using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace YahooStockEngine
{
    class StockDataCrawler
    {
        private const string BASE_URL = @"http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20({0})&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
        
        public List<string> SymbolList { get; set; }

        public StockDataCrawler(params string[] symbols)
        {
            SymbolList = new List<string>(symbols);
        }


        public int FetchAndSave()
        {
            CreateDirectories();

            XDocument doc = FetchAll(SymbolList);

            //for testing purpose only
            doc.Save(Path.Combine(Common.StoragePath, "temp.xml"));

            XElement results = doc.Root.Element("results");
            int count = 0;


            foreach (string symbol in SymbolList)
            {
                Console.WriteLine(symbol);
                XElement rawQuote = results.Elements("quote").First(w => w.Attribute("symbol").Value == symbol);
                Quote quote = StockDataParser.Parse(rawQuote);

                string latestQuotePath = Common.GetLatestQuotePath(symbol);
                if (latestQuotePath != null)
                {
                    Quote latestQuote = StockDataParser.Parse(XElement.Load(latestQuotePath));
                    if (latestQuote == null || quote.LastTradeTime <= latestQuote.LastTradeTime) { continue; }
                }

                count++;
                string path = Common.ComputePath(symbol, quote.LastTradeTime);
                rawQuote.Save(path);
            }

            return count;
                
        }


        private void CreateDirectories()
        {
            foreach (string symbol in SymbolList)
            {
                Common.CreateDirectory(symbol);
            }
        }

        //Never use at this time
        private XDocument Fetch(string symbol)
        {
            return FetchAll(new string[] { symbol });
        }

        private XDocument FetchAll(IEnumerable<string> symbols)
        {

            string symbolList = String.Join("%2C", symbols.Select(w => "%22" + w + "%22").ToArray());
            string url = String.Format(BASE_URL, symbolList);

            XDocument doc = XDocument.Load(url);
            return doc;
        }

    }
}
