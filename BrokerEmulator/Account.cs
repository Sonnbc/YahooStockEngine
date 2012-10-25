using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace BrokerEmulator
{
    class Account
    {
        public decimal Balance { get; set; }
        public int ID { get; set; }
        public HoldingPortfolio Portfolio { get; set; }

        public bool Buy(string ticker, int amount, decimal price)
        {
            if (!Portfolio.Contains(ticker)) {
                Portfolio.Add(new Holding() {symbol = ticker, amount = 0});
            }
        
            decimal newBalance = Balance - price * amount;
            if (newBalance < 0) return false;
            
            int newAmount = Portfolio[ticker].amount + amount;
            if (newAmount < 0) return false;

            Balance = newBalance;
            Portfolio[ticker].amount = newAmount;
            return true;
        }

        public int GetAmount(string symbol)
        {
            if (!Portfolio.Contains(symbol)) 
            {
                return 0;
            }
            return Portfolio[symbol].amount;
        }
     }

    class Holding
    {
        public string symbol;
        public int amount;
    }

    class HoldingPortfolio : KeyedCollection<string, Holding>
    {
        protected override string GetKeyForItem(Holding item)
        {
            return item.symbol;
        }
    }
}
