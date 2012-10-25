using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using YahooStockEngine;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace CS411DataFeed
{
    public static class DataFeeder
    {
        public const int GROUP_SIZE = 1000;
        public static List<string> WantedProperties = new List<string>( new string[] {"LastTradePriceOnly", "Volume", "LastTradeDate", "LastTradeTime", "Symbol"} );

        public static void Send(string data) {
            byte[] byteArray = Encoding.UTF8.GetBytes(data);

            WebRequest request = WebRequest.Create("http://htrade.web.engr.illinois.edu/hello.php");
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            request.GetResponse().Close();
            /*WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);
            reader.Close();
            dataStream.Close();
            response.Close();*/
            
        }

        public static string AddAccountInformation(string str)
        {
            XDocument xml = XDocument.Parse(str);
            xml.Root.Add(new XElement("Account", "nguyenb1"));
            xml.Root.Add(new XElement("Password", "asdasd"));
            return xml.ToString();
        }

        public static void Filter(XElement quote)
        {
            List<XElement> toRemove = new List<XElement>();
            foreach (XElement node in quote.Nodes())
            {
                if (!WantedProperties.Contains(node.Name.ToString()))
                {
                    toRemove.Add(node);
                }
            }
            foreach (XElement node in toRemove) node.Remove();
        }

        public static void TransferDataForSymbol(string symbol)
        {
            int count = 0;
            XElement group = new XElement("quotes", new XAttribute("symbol", symbol));

            
            foreach (string path in Directory.EnumerateFiles(Common.PathToSymbol(symbol)))
            {
                count++;
                if (count % GROUP_SIZE == 0)
                {
                    Console.WriteLine(symbol + "Sending " + count);

                    while (true)
                    {
                        try
                        {
                            Send(group.ToString());
                            group.RemoveNodes();
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Timeout");
                            Thread.Sleep(1000);
                        }
                    }

                }
                XElement content = XElement.Parse(File.ReadAllText(path));
                Filter(content);
                group.Add(content);
            }

            Send(group.ToString());
        }

        public static void Main(string [] args) {
            Parallel.ForEach(Common.SymbolList, symbol => TransferDataForSymbol(symbol));
        }  

    }
}
