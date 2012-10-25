using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YahooStockEngine;
using System.Xml.Linq;

namespace BrokerEmulator
{
    internal class BrokerEmulator
    {
        private List<Account> AccountList {get; set;}
        public Dictionary<string, Quote> CurrentQuotes { get; set; }

        public BrokerEmulator()
        {
            AccountList = new List<Account>();
            CurrentQuotes = new Dictionary<string, Quote>();
        }

        internal int CreateAccount(decimal balance)
        {
            Account account = new Account { Balance = balance, ID = AccountList.Count, Portfolio = new HoldingPortfolio() };
            AccountList.Add(account);
            return account.ID;
        }

        internal Account GetAccount(int id)
        {
            return AccountList[id];
        }

        internal decimal GetPrice(string symbol)
        {
            return (decimal) CurrentQuotes[symbol].LastTradePrice;
        }

        internal DateTime GetTime(string symbol)
        {
            return (DateTime)CurrentQuotes[symbol].LastTradeTime;
        }

        internal int GetHoldingAmountForSymbol(int accountId, string symbol)
        {
            return AccountList[accountId].GetAmount(symbol);
        }

        internal bool BuyMarketPrice(int accountID, string symbol, int amount)
        {
            Account account = AccountList[accountID];
            decimal price = GetPrice(symbol);
            return account.Buy(symbol, amount, price);
        }

        internal bool SellMarketPrice(int accountID, string symbol, int amount)
        {
            return BuyMarketPrice(accountID, symbol, -amount);
        }

        internal decimal GetPortfolioValue(int accountID)
        {
            return (from Holding holding in AccountList[accountID].Portfolio select holding.amount * GetPrice(holding.symbol)). Sum();
        }

        internal decimal GetBalance(int accountID)
        {
            return AccountList[accountID].Balance;
        }

        internal decimal GetEquity(int accountID)
        {
            return GetBalance(accountID) + GetPortfolioValue(accountID);
        }


    }
}
