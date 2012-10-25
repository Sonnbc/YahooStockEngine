using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Linq;

namespace YahooStockEngine
{
    public static class Common
    {
        public static string[] SymbolList = {"AAPL", "AMZN", "BBY", "CEL", "DNDN", "DOLE", 
                                        "EBAY", "F", "FB", "GOOG", "GRPN", "GWRE",
                                        "KYAK", "LNKD", "MSFT", "NOK", "NTAP", "OPNT", "PANW", 
                                        "PTNR", "RVBD", "TSLA", "UBNT", "VRNG", "YHOO", "ZNGA"};

        private const string DATE_TIME_FORMAT = "yyyy-MM-dd hh-mm-ss tt";
        private const string FILE_NAME_FORMAT = @"{0} @ {1:" + DATE_TIME_FORMAT + "}";
        private const string REGEX_FILE_NAME_FORMAT = @"(?<symbol>.*) @ (?<time>.*)";

        private static string storagePath = @"D:\StockData\Yahoo";
        public static string StoragePath
        {
            get { return storagePath; }
            set { storagePath = value; }
        }

        public static void CreateDirectory(string symbol)
        {
            var path = Path.Combine(StoragePath, symbol);
            Directory.CreateDirectory(path);
        }

        public static string PathToSymbol(string symbol) {
            return Path.Combine(StoragePath, symbol);
        }

        public static string ComputePath(string symbol, DateTime? time)
        {
            string saveFileName = String.Format(FILE_NAME_FORMAT, symbol, time);
            string path = Path.Combine(PathToSymbol(symbol), saveFileName);

            return path;
        }

        public static DateTime GetEarliestTime(string symbol)
        {
            string path = PathToSymbol(symbol);
            return (from fileName in Directory.EnumerateFiles(path).Select(w => Path.GetFileNameWithoutExtension(w))
                    select GetTimeFromFileName(fileName)).Min();
        }
            
        public static DateTime GetEarliestTimeAfter(string symbol, DateTime time) {
            string path = PathToSymbol(symbol);
            return (from fileName in Directory.EnumerateFiles(path).Select(w => Path.GetFileNameWithoutExtension(w))
                    select GetTimeFromFileName(fileName)).Where(w => w > time). Min();
        }

        public static DateTime GetEarliestTime()
        {
            return (from symbol in Common.SymbolList select GetEarliestTime(symbol)).Min();
        }

        public static DateTime? GetEarliestTimeAfter(DateTime time)
        {
            try
            {
                return (from symbol in Common.SymbolList select GetEarliestTimeAfter(symbol, time)).Min();
            }
            catch (InvalidOperationException e)
            {
                return null;
            }
        }

        public static DateTime GetLatestTime(string symbol)
        {
            string path = PathToSymbol(symbol);
            return (from fileName in Directory.EnumerateFiles(path).Select(w => Path.GetFileNameWithoutExtension(w))
                    select GetTimeFromFileName(fileName)).Max();
        }

        
        public static DateTime GetLatestTime()
        {
            return (from symbol in Common.SymbolList select GetLatestTime(symbol)).Max();
        }

        public static string GetLatestQuotePath(string symbol)
        {
            return GetLatestQuotePathUpTo(symbol, DateTime.MaxValue);
        }

        public static string GetLatestQuotePathUpTo(string symbol, DateTime maxTime)
        {
            string pathToSymbol = PathToSymbol(symbol);
            string latest = null;
            foreach (string fileName in Directory.EnumerateFiles(pathToSymbol).Select(w => Path.GetFileNameWithoutExtension(w)))
            {
                DateTime? time = GetTimeFromFileName(fileName);
                if (time > maxTime) continue;
                if (latest == null || time > GetTimeFromFileName(latest))
                {
                    latest = fileName;
                }
            }

            if (latest == null) return null;

            return Path.Combine(pathToSymbol, latest);
        }

        public static DateTime GetTimeFromPath(string path)
        {
            return GetTimeFromFileName(Path.GetFileNameWithoutExtension(path));
        }

        private static DateTime GetTimeFromFileName(string fileName)
        {
            if (fileName == null)
            {
                throw new NotImplementedException();
            }

            string pattern = String.Format(REGEX_FILE_NAME_FORMAT);
            Match match = Regex.Match(fileName, pattern);
            if (!match.Success)
            {
                throw new NotImplementedException();
            }

            return ParseDateTime(match.Groups["time"].ToString());
        }

        private static DateTime ParseDateTime(string input)
        {
            if (input == null)
            {
                throw new NotImplementedException();
            }

            DateTime value;
            if (!DateTime.TryParseExact(input, DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out value))
            {
                throw new NotImplementedException();
            }
            return value;
        }




    }
}
