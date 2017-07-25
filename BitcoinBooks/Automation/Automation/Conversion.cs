using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Automation
{
    public static class Conversion
    {

        private static Dictionary<DateTime, double> _conversionRates = new Dictionary<DateTime, double>();

        static Conversion()
        {
            char[] charsToTrim = {'\r'};
            string[] csv = Properties.Resources.btcValues.Split('\n');

            foreach (string s in csv)
            {
                try
                {
                    string[] values = s.Split(',');
                    values[1] = values[1].TrimEnd(charsToTrim);
                    _conversionRates.Add(values[0].ToDateTime(), values[1].ToDouble());
                }
                catch (Exception e)
                {
                }

            }
        }


        public static double GetGBPToBTCRateOn(DateTime date)
        {
            DateTime dateToCheck = new DateTime(date.Year, date.Month, date.Day);
            double rate = 0.0;
            _conversionRates.TryGetValue(dateToCheck, out rate);
            if (rate != 0.0)
                return (1/rate);
            return 0.0;
        }
    }
}


