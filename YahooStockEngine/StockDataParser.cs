using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YahooStockEngine
{
    public class StockDataParser
    {
        public static Quote Parse(XElement raw)
        {
            Quote quote = new Quote(raw.Attribute("symbol").Value);

            quote.Ask = GetDecimal(raw.Element("Ask").Value);
            quote.Bid = GetDecimal(raw.Element("Bid").Value);
            quote.AverageDailyVolume = GetDecimal(raw.Element("AverageDailyVolume").Value);
            quote.BookValue = GetDecimal(raw.Element("BookValue").Value);
            quote.Change = GetDecimal(raw.Element("Change").Value);
            quote.DividendShare = GetDecimal(raw.Element("DividendShare").Value);
            quote.LastTradeTime = GetDateTime(raw.Element("LastTradeDate").Value + " " + raw.Element("LastTradeTime").Value);
            quote.EarningsShare = GetDecimal(raw.Element("EarningsShare").Value);
            quote.EpsEstimateCurrentYear = GetDecimal(raw.Element("EPSEstimateCurrentYear").Value);
            quote.EpsEstimateNextYear = GetDecimal(raw.Element("EPSEstimateNextYear").Value);
            quote.EpsEstimateNextQuarter = GetDecimal(raw.Element("EPSEstimateNextQuarter").Value);
            quote.DailyLow = GetDecimal(raw.Element("DaysLow").Value);
            quote.DailyHigh = GetDecimal(raw.Element("DaysHigh").Value);
            quote.YearlyLow = GetDecimal(raw.Element("YearLow").Value);
            quote.YearlyHigh = GetDecimal(raw.Element("YearHigh").Value);
            quote.MarketCapitalization = GetDecimal(raw.Element("MarketCapitalization").Value);
            quote.Ebitda = GetDecimal(raw.Element("EBITDA").Value);
            quote.ChangeFromYearLow = GetDecimal(raw.Element("ChangeFromYearLow").Value);
            quote.PercentChangeFromYearLow = GetDecimal(raw.Element("PercentChangeFromYearLow").Value);
            quote.ChangeFromYearHigh = GetDecimal(raw.Element("ChangeFromYearHigh").Value);
            quote.LastTradePrice = GetDecimal(raw.Element("LastTradePriceOnly").Value);
            quote.PercentChangeFromYearHigh = GetDecimal(raw.Element("PercebtChangeFromYearHigh").Value); //missspelling in yahoo for field name
            quote.FiftyDayMovingAverage = GetDecimal(raw.Element("FiftydayMovingAverage").Value);
            quote.TwoHunderedDayMovingAverage = GetDecimal(raw.Element("TwoHundreddayMovingAverage").Value);
            quote.ChangeFromTwoHundredDayMovingAverage = GetDecimal(raw.Element("ChangeFromTwoHundreddayMovingAverage").Value);
            quote.PercentChangeFromTwoHundredDayMovingAverage = GetDecimal(raw.Element("PercentChangeFromTwoHundreddayMovingAverage").Value);
            quote.PercentChangeFromFiftyDayMovingAverage = GetDecimal(raw.Element("PercentChangeFromFiftydayMovingAverage").Value);
            quote.Name = raw.Element("Name").Value;
            quote.Open = GetDecimal(raw.Element("Open").Value);
            quote.PreviousClose = GetDecimal(raw.Element("PreviousClose").Value);
            quote.ChangeInPercent = GetDecimal(raw.Element("ChangeinPercent").Value);
            quote.PriceSales = GetDecimal(raw.Element("PriceSales").Value);
            quote.PriceBook = GetDecimal(raw.Element("PriceBook").Value);
            quote.ExDividendDate = GetDateTime(raw.Element("ExDividendDate").Value);
            quote.PeRatio = GetDecimal(raw.Element("PERatio").Value);
            quote.DividendPayDate = GetDateTime(raw.Element("DividendPayDate").Value);
            quote.PegRatio = GetDecimal(raw.Element("PEGRatio").Value);
            quote.PriceEpsEstimateCurrentYear = GetDecimal(raw.Element("PriceEPSEstimateCurrentYear").Value);
            quote.PriceEpsEstimateNextYear = GetDecimal(raw.Element("PriceEPSEstimateNextYear").Value);
            quote.ShortRatio = GetDecimal(raw.Element("ShortRatio").Value);
            quote.OneYearPriceTarget = GetDecimal(raw.Element("OneyrTargetPrice").Value);
            quote.Volume = GetDecimal(raw.Element("Volume").Value);
            quote.StockExchange = raw.Element("StockExchange").Value;

            quote.LastUpdate = DateTime.Now;

            return quote;
        }

        private static decimal? GetDecimal(string input)
        {
            if (input == null) return null;

            input = input.Replace("%", "");

            decimal value;

            if (Decimal.TryParse(input, out value)) return value;
            return null;
        }

        private static DateTime? GetDateTime(string input)
        {
            if (input == null) return null;

            DateTime value;

            if (DateTime.TryParse(input, out value)) return value;
            return null;
        }
    }
}
