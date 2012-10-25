using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrokerEmulator
{
    interface IStrategy
    {
        bool takeAction(int accountId, BrokerEmulator broker);
        bool takeAction(int accountId, BrokerEmulator broker, string symbol);
    }

    class SimpleStrategy : IStrategy
    {
        decimal? lastCheckPoint = null;
        string symbol;
        bool lastTrend = false, currentTrend = false; //true: bullish, false:bearish
        decimal upwardDelta = 0, downwardDelta = 0;
        public SimpleStrategy(string ticker, decimal up, decimal down)
        {
            symbol = ticker;
            upwardDelta = up;
            downwardDelta = down;
        }
        public bool takeAction(int accountId, BrokerEmulator broker)
        {
            decimal currentPrice = broker.GetPrice(symbol);
            if (lastCheckPoint == null)
            {
                lastCheckPoint = currentPrice;
                return true;
            } 

            bool res = true;

            decimal lastPrice = (decimal)lastCheckPoint;

            //Dont hold any shares overnight
            if (broker.GetTime(symbol).TimeOfDay >= new TimeSpan(15, 58, 0))
            {
                SellAll(accountId, broker);
            }
            if (currentPrice > lastPrice * (1 + upwardDelta)) {
                lastCheckPoint = currentPrice;
                res = BuyAll(accountId, broker); 
            }
            else if (currentPrice < lastPrice * (1 - downwardDelta))
            {
                lastCheckPoint = currentPrice;
                res = SellAll(accountId, broker);
            }

            currentTrend = currentPrice > lastPrice;
            if (lastTrend != currentTrend) 
            {
                lastCheckPoint = currentPrice;
            }
            
            lastTrend = currentTrend;

            return res;

        }

        public bool BuyAll(int accountId, BrokerEmulator broker)
        {
            
            decimal balance = broker.GetBalance(accountId);
            decimal price = broker.GetPrice(symbol);
            DateTime time = broker.GetTime(symbol);
            int amount = (int) (balance / price);
            Console.WriteLine("At time " + time + " buy " + amount + " shares of " + symbol + " at $" + price + " a share. -$" + amount*price);
            return broker.BuyMarketPrice(accountId, symbol, amount);
        }

        public bool SellAll(int accountId, BrokerEmulator broker)
        {
            int amount = broker.GetHoldingAmountForSymbol(accountId, symbol);
            decimal price = broker.GetPrice(symbol);
            DateTime time = broker.GetTime(symbol);
            Console.WriteLine("At time " + time + " sell " + amount + " shares of " + symbol + " at $" + price + " a share. +$" + amount*price);
            return broker.SellMarketPrice(accountId, symbol, amount);
        }

        public bool takeAction(int accountId, BrokerEmulator broker, string symbol)
        {
            throw new NotImplementedException();   
        }
    }
        
}
