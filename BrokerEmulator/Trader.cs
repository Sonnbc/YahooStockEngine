using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YahooStockEngine;

namespace BrokerEmulator
{
    class Trader
    {
        public const String SYMBOL = "F";
        private IStrategy strategy;
        private BrokerEmulator broker;
        private int accountId;
        internal Trader(IStrategy strategyToUse, decimal balance = (decimal) 1e5)
        {
            strategy = strategyToUse;
            broker = new BrokerEmulator();
            accountId = broker.CreateAccount(balance);
        }

        internal decimal GetEquity() {
            return broker.GetEquity(accountId);
        }

        internal void TakeAction(DateTime time)
        {
           strategy.takeAction(accountId, broker);
        }

        internal void SetQuote(string symbol, Quote quote)
        {
            if (!broker.CurrentQuotes.ContainsKey(symbol))
            {
                broker.CurrentQuotes.Add(symbol, quote);
            }
            else
            {
               broker.CurrentQuotes[symbol] = quote;
            }
        }

        
        public static void Main(string[] args)
        {
            Trader trader = new Trader(new SimpleStrategy(SYMBOL, 0.01m, 0.02m));
            LinearDataProvider provider = new LinearDataProvider(new string[] { SYMBOL });

            DateTime time = provider.GetEarliestTime(SYMBOL);
            DateTime endTime = provider.GetLatestTime(SYMBOL);
            TimeSpan interval = new TimeSpan(0, 0, 30); //30 seconds

            Console.WriteLine("Initial equity: $" + trader.GetEquity()); 
            while (time < endTime)
            {
                //TODO: make broker take quote directly from provider instead of having to SetQuote()
                trader.SetQuote(SYMBOL, provider.GetCurrentQuote(SYMBOL));
                trader.TakeAction(time);
                DateTime? nextTime = provider.MoveToTime(SYMBOL, time + interval);
                if (nextTime == null) break;
                time = (DateTime)nextTime;
            }

            Console.WriteLine("Final equity: $" + trader.GetEquity());

            Console.ReadLine();
        }
    }
}
