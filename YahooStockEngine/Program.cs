using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace YahooStockEngine
{
    class Program
    {
        private const int INTERVAL = 21000; //21 seconds

        static void Main(string[] args)
        {
            StockDataCrawler crawler = new StockDataCrawler(Common.SymbolList);

            int countQueries = 0;
            while (true)
            {
                try
                {
                    Console.Write("{0, 5}. Fetching " + crawler.SymbolList.Count + " tickers at " + DateTime.Now.ToString() + ".........", countQueries++);
                    int countTickers = crawler.FetchAndSave();
                    Console.WriteLine("Updated " + countTickers + " ticker(s).");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                Thread.Sleep(INTERVAL);
            }
        }
    }
}
