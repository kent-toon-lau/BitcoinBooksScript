using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace BitcoinBooks
{
    class Conversion
    {
        private static string XeDotComSpotPriceUri = "http://www.xe.com/currencytables/?from=XBT&date=";
        private static Dictionary<DateTime, double> _conversionRates = new Dictionary<DateTime, double>();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateOfTransaction"> Pass in a specific date to retrieve whatever the exchange was at that date. </param>
        /// <returns> Returns the exchange rate of what 1 Bitcoin is to GBP. </returns>
        /// 
        public static double GetBTCToGBPRateOn(DateTime dateOfTransaction)
        {
            double exchangeRate = 0.0;
            if (_conversionRates.ContainsKey(dateOfTransaction))
            {
                // If we already have the exchange rate for a given date then...
                // use the recorded one and avoid connecting to the internet.
                _conversionRates.TryGetValue(dateOfTransaction, out exchangeRate);
            }
            else // This is a new search date that hasn't been recorded.
            {
                WebClient client = null;
                bool foundExchangeRate = false;
                while (!foundExchangeRate)
                {
                    #region try{}
                    try
                    {
                        client = new WebClient();

                        #region XE Website
                        var uri = $"{XeDotComSpotPriceUri}{dateOfTransaction:yyyy-MM-dd}";

                        var html = new HtmlDocument();
                        html.LoadHtml(client.DownloadString(uri));

                        var root = html.DocumentNode;
                        var p = root
                            .Descendants()
                            .Single(n => n.GetAttributeValue("class", "").Equals("historicalRateTable-wrap"))
                            .Descendants("tr")
                            .Single(n => n.FirstChild.InnerHtml.Contains("gbp-british-pound"))
                            .Descendants("td")
                            .First(n => n.GetAttributeValue("class", "").Equals("historicalRateTable-rateHeader"));
                        exchangeRate = Convert.ToDouble(p.InnerText);
                        #endregion

                        if (exchangeRate != 0.0)
                        {
                            foundExchangeRate = true;
                        }


                        //#region Currencies Website
                        //// YYYY-MM-DD, note: leading zeroes are not needed.
                        //// Opens up a web link to currencies and downloads the string outputted by the website.
                        //string date = dateOfTransaction.Year + "-" + dateOfTransaction.Month + "-" + dateOfTransaction.Day;
                        //var uri = String.Format("http://currencies.apps.grandtrunk.net/getrate/" + date + "/btc/gbp");

                        //client.UseDefaultCredentials = true;
                        //var data = client.DownloadString(uri);
                        //exchangeRate = Convert.ToDouble(data);
                        //#endregion

                    }
                    #endregion

                    #region catch{}
                    catch (Exception ex)
                    {
                        // Console.WriteLine("\nError: " + ex.Message);
                    }
                    #endregion

                    #region finally{}
                    finally
                    {
                        client?.Dispose();
                        client = null;
                    }
                    #endregion
                }

                _conversionRates.Add(dateOfTransaction, exchangeRate);   // Add date and exchangeRate to dictionary.
            }
            return exchangeRate;

        }
    }
}
