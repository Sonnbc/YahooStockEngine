using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrokerEmulator
{
    class SmaKeeper
    {
        private SortedDictionary<DateTime, decimal> quotes;

        private decimal Sum; 
        private DateTime lastQuote;

        public int Period {get; private set; } //minutes
        public int Capacity { get; private set; }

        public SmaKeeper(int count, int period)
        {
            Period = period;
            Capacity = count;
            lastQuote = DateTime.MinValue;
            quotes = new SortedDictionary<DateTime, decimal>();
        }

        private DateTime GetDateOnly(DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day);
        }

        public void Take(DateTime time, decimal price)
        {
            if (lastQuote.AddMinutes(Period) > time) return;
            
            lastQuote = time;
            quotes[time] = price;
            Sum += price;

            while (true)
            {
                var first = quotes.ElementAt(0);
                if (first.Key.AddMinutes(Period * Capacity) >= time) break;
                quotes.Remove(first.Key);
                Sum -= first.Value;
                //Console.WriteLine(Capacity + "      " + time + "    " + GetAverage());
            }
            
        }

        public decimal GetAverage()
        {
            return Sum / quotes.Count;
        }
    }

    class SmaStrategy : IStrategy
    {
        SmaKeeper longSma, shortSma;
        string symbol;

        DateTime firstCross;
        TimeSpan safety = new TimeSpan(0, 5, 0); //5 minutes
        decimal forseeConstant = 0.003m;


        public SmaStrategy(string ticker)
        {
            longSma = new SmaKeeper(21, 2);
            shortSma = new SmaKeeper(8, 2);

            symbol = ticker;
            firstCross = DateTime.MaxValue;
        }

        public bool TakeAction(int accountId, BrokerEmulator broker)
        {
            
            decimal currentPrice = broker.GetPrice(symbol);
            DateTime currentTime = broker.GetTime(symbol);
            longSma.Take(currentTime, currentPrice);
            shortSma.Take(currentTime, currentPrice);
            
            //Console.WriteLine(currentTime + " " + shortSma.GetAverage());

            //Sell all at the end of day
            if (currentTime.Hour == 15 && currentTime.Minute >= 58)
            {
                return SellAll(accountId, broker);
            }

            
            bool res = true;
            var predictor = forseeConstant * currentPrice;
            if (shortSma.GetAverage() - predictor > longSma.GetAverage())
            {
                if (firstCross == DateTime.MaxValue) firstCross = currentTime;
                if (firstCross.Add(safety) <= currentTime) res = BuyAll(accountId, broker);
            }
            else if (shortSma.GetAverage() - predictor < longSma.GetAverage())
            {
                res= SellAll(accountId, broker);
                firstCross = DateTime.MaxValue;
            }

           

            return res;
        }

         public bool BuyAll(int accountId, BrokerEmulator broker)
        {
            
            decimal balance = broker.GetBalance(accountId);
            decimal price = broker.GetPrice(symbol);
            DateTime time = broker.GetTime(symbol);
            int amount = (int) (balance / price);
            if (amount == 0) return true;

            Console.WriteLine("At time " + time + " buy " + amount + " shares of " + symbol + " at $" + price + " a share. -$" + amount*price);
            return broker.BuyMarketPrice(accountId, symbol, amount);
        }

        public bool SellAll(int accountId, BrokerEmulator broker)
        {
            int amount = broker.GetHoldingAmountForSymbol(accountId, symbol);
            decimal price = broker.GetPrice(symbol);
            DateTime time = broker.GetTime(symbol);
            if (amount == 0) return true;
            Console.WriteLine("At time " + time + " sell " + amount + " shares of " + symbol + " at $" + price + " a share. +$" + amount*price);
            return broker.SellMarketPrice(accountId, symbol, amount);
        }

        public bool TakeAction(int accountId, BrokerEmulator broker, string symbol)
        {
            throw new NotImplementedException();
        }
    }
}
