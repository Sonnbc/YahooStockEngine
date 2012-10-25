using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YahooStockEngine
{
    public class Quote
    {
        private string symbol;
        private decimal? averageDailyVolume;
        private decimal? bid;
        private decimal? ask;
        private decimal? bookValue;
        private decimal? changePercent;
        private decimal? change;
        private decimal? dividendShare;
        private DateTime? lastTradeTime;
        private decimal? earningsShare;
        private decimal? epsEstimateCurrentYear;
        private decimal? epsEstimateNextYear;
        private decimal? epsEstimateNextQuarter;
        private decimal? dailyLow;
        private decimal? dailyHigh;
        private decimal? yearlyLow;
        private decimal? yearlyHigh;
        private decimal? marketCapitalization;
        private decimal? ebitda;
        private decimal? changeFromYearLow;
        private decimal? percentChangeFromYearLow;
        private decimal? changeFromYearHigh;
        private decimal? percentChangeFromYearHigh;
        private decimal? lastTradePrice;
        private decimal? fiftyDayMovingAverage;
        private decimal? twoHunderedDayMovingAverage;
        private decimal? changeFromTwoHundredDayMovingAverage;
        private decimal? percentChangeFromFiftyDayMovingAverage;
        private string name;
        private decimal? open;
        private decimal? previousClose;
        private decimal? changeInPercent;
        private decimal? priceSales;
        private decimal? priceBook;
        private DateTime? exDividendDate;
        private decimal? pegRatio;
        private decimal? priceEpsEstimateCurrentYear;
        private decimal? priceEpsEstimateNextYear;
        private decimal? shortRatio;
        private decimal? oneYearPriceTarget;
        private decimal? dividendYield;
        private DateTime? dividendPayDate;
        private decimal? percentChangeFromTwoHundredDayMovingAverage;
        private decimal? peRatio;
        private decimal? volume;
        private string stockExchange;
        private DateTime lastUpdate;


        public DateTime LastUpdate
        {
            get { return lastUpdate; }
            set
            {
                lastUpdate = value;
            }
        }


        public string StockExchange
        {
            get { return stockExchange; }
            set
            {
                stockExchange = value;
            }
        }


        public decimal? Volume
        {
            get { return volume; }
            set
            {
                volume = value;
            }
        }

        public decimal? PeRatio
        {
            get { return peRatio; }
            set
            {
                peRatio = value;
            }
        }

        public decimal? PercentChangeFromTwoHundredDayMovingAverage
        {
            get { return percentChangeFromTwoHundredDayMovingAverage; }
            set
            {
                percentChangeFromTwoHundredDayMovingAverage = value;
            }
        }

        public Quote(string ticker)
        {
            symbol = ticker;
        }

        public DateTime? DividendPayDate
        {
            get { return dividendPayDate; }
            set
            {
                dividendPayDate = value;
            }
        }

        public decimal? DividendYield
        {
            get { return dividendYield; }
            set
            {
                dividendYield = value;
            }
        }


        public decimal? OneYearPriceTarget
        {
            get { return oneYearPriceTarget; }
            set
            {
                oneYearPriceTarget = value;
            }
        }

        public decimal? ShortRatio
        {
            get { return shortRatio; }
            set
            {
                shortRatio = value;
            }
        }


        public decimal? PriceEpsEstimateNextYear
        {
            get { return priceEpsEstimateNextYear; }
            set
            {
                priceEpsEstimateNextYear = value;
            }
        }


        public decimal? PriceEpsEstimateCurrentYear
        {
            get { return priceEpsEstimateCurrentYear; }
            set
            {
                priceEpsEstimateCurrentYear = value;
            }
        }


        public decimal? PegRatio
        {
            get { return pegRatio; }
            set
            {
                pegRatio = value;
            }
        }


        public DateTime? ExDividendDate
        {
            get { return exDividendDate; }
            set
            {
                exDividendDate = value;
            }
        }


        public decimal? PriceBook
        {
            get { return priceBook; }
            set
            {
                priceBook = value;
            }
        }


        public decimal? PriceSales
        {
            get { return priceSales; }
            set
            {
                priceSales = value;
            }
        }


        public decimal? ChangeInPercent
        {
            get { return changeInPercent; }
            set
            {
                changeInPercent = value;
            }
        }


        public decimal? PreviousClose
        {
            get { return previousClose; }
            set
            {
                previousClose = value;
            }
        }


        public decimal? Open
        {
            get { return open; }
            set
            {
                open = value;
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }


        public decimal? PercentChangeFromFiftyDayMovingAverage
        {
            get { return percentChangeFromFiftyDayMovingAverage; }
            set
            {
                percentChangeFromFiftyDayMovingAverage = value;
            }
        }


        public decimal? ChangeFromTwoHundredDayMovingAverage
        {
            get { return changeFromTwoHundredDayMovingAverage; }
            set
            {
                changeFromTwoHundredDayMovingAverage = value;
            }
        }


        public decimal? TwoHunderedDayMovingAverage
        {
            get { return twoHunderedDayMovingAverage; }
            set
            {
                twoHunderedDayMovingAverage = value;
            }
        }


        public decimal? FiftyDayMovingAverage
        {
            get { return fiftyDayMovingAverage; }
            set
            {
                fiftyDayMovingAverage = value;
            }
        }


        public decimal? LastTradePrice
        {
            get { return lastTradePrice; }
            set
            {
                lastTradePrice = value;
            }
        }


        public decimal? PercentChangeFromYearHigh
        {
            get { return percentChangeFromYearHigh; }
            set
            {
                percentChangeFromYearHigh = value;
            }
        }


        public decimal? ChangeFromYearHigh
        {
            get { return changeFromYearHigh; }
            set
            {
                changeFromYearHigh = value;
            }
        }


        public decimal? PercentChangeFromYearLow
        {
            get { return percentChangeFromYearLow; }
            set
            {
                percentChangeFromYearLow = value;
            }
        }


        public decimal? ChangeFromYearLow
        {
            get { return changeFromYearLow; }
            set
            {
                changeFromYearLow = value;
            }
        }


        public decimal? Ebitda
        {
            get { return ebitda; }
            set
            {
                ebitda = value;
            }
        }


        public decimal? MarketCapitalization
        {
            get { return marketCapitalization; }
            set
            {
                marketCapitalization = value;
            }
        }


        public decimal? YearlyHigh
        {
            get { return yearlyHigh; }
            set
            {
                yearlyHigh = value;
            }
        }


        public decimal? YearlyLow
        {
            get { return yearlyLow; }
            set
            {
                yearlyLow = value;
            }
        }


        public decimal? DailyHigh
        {
            get { return dailyHigh; }
            set
            {
                dailyHigh = value;
            }
        }


        public decimal? DailyLow
        {
            get { return dailyLow; }
            set
            {
                dailyLow = value;
            }
        }


        public decimal? EpsEstimateNextQuarter
        {
            get { return epsEstimateNextQuarter; }
            set
            {
                epsEstimateNextQuarter = value;
            }
        }


        public decimal? EpsEstimateNextYear
        {
            get { return epsEstimateNextYear; }
            set
            {
                epsEstimateNextYear = value;
            }
        }


        public decimal? EpsEstimateCurrentYear
        {
            get { return epsEstimateCurrentYear; }
            set
            {
                epsEstimateCurrentYear = value;
            }
        }


        public decimal? EarningsShare
        {
            get { return earningsShare; }
            set
            {
                earningsShare = value;
            }
        }


        public DateTime? LastTradeTime
        {
            get { return lastTradeTime; }
            set
            {
                lastTradeTime = value;
            }
        }


        public decimal? DividendShare
        {
            get { return dividendShare; }
            set
            {
                dividendShare = value;
            }
        }


        public decimal? Change
        {
            get { return change; }
            set
            {
                change = value;
            }
        }


        public decimal? ChangePercent
        {
            get { return changePercent; }
            set
            {
                changePercent = value;
            }
        }


        public decimal? BookValue
        {
            get { return bookValue; }
            set
            {
                bookValue = value;
            }
        }


        public decimal? Ask
        {
            get { return ask; }
            set
            {
                ask = value;
            }
        }


        public decimal? Bid
        {
            get { return bid; }
            set
            {
                bid = value;
            }
        }


        public decimal? AverageDailyVolume
        {
            get { return averageDailyVolume; }
            set
            {
                averageDailyVolume = value;
            }
        }


        public string Symbol
        {
            get { return symbol; }
            set
            {
                symbol = value;
            }
        }
    }
}
