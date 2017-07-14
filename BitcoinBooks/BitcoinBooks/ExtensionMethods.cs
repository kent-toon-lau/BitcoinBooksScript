using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinBooks
{
    public static class ExtensionMethods
    {
        public static DateTime ToDateTime(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                //Expected format MM/dd/yyyy HH:mm

                //Assumes the string is actually a date time and in the right format, or it will throw exception
                //Times are truncated off

                var strMonth = str.Substring(0, 2).ToInt();
                var strDay = str.Substring(3, 2).ToInt();
                var strYear = str.Substring(6, 4).ToInt();
                var strHours = str.Substring(11, 2).ToInt();
                var strMins = str.Substring(14, 2).ToInt();

                return new DateTime(strYear, strMonth, strDay, strHours, strMins, 0);
            }
            return new DateTime();
        }

        public static double ToCurrency(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                //Assumes the string is actually a number of some kind that can be represented as a double, or it will throw exception
                if (str == " ")
                    return 0.0;
                return double.Parse(str, CultureInfo.InvariantCulture.NumberFormat);  
            }
            return 0.0;
        }

        public static int ToInt(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                //Assumes it actually is a valid int, will throw an exception if not
                return int.Parse(str, CultureInfo.InvariantCulture.NumberFormat);
            }
            return 0;

        }

        public static List<List<string>> ToOutsList(this List<Transaction> transactions)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (Transaction transaction in transactions)
            {
                data.Add(new List<string>()
                {
                    transaction.Date.ToString("dd/MM/yyyy HH:mm"),
                    " ",
                    transaction.BTN_Value.ToString(),
                    transaction.Description
                });
            }
            return data;
        }

        public static List<List<string>> ToInsList(this List<Transaction> transactions)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (Transaction transaction in transactions)
            {
                data.Add(new List<string>()
                {
                    transaction.Date.ToString("dd/MM/yyyy HH:mm"),
                    transaction.GBP_Value.ToString(),
                    " ",      
                    transaction.Description
                });
            }
            return data;
        }

        public static List<List<string>> ToFullInsList(this List<Transaction> transactions)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (Transaction transaction in transactions)
            {
                data.Add(new List<string>()
                {
                    transaction.Date.ToString("dd/MM/yyyy HH:mm"),
                    transaction.GBP_Value.ToString(),
                    transaction.BTN_Value.ToString(),
                    transaction.ConversionRate.ToString(),
                    transaction.Description
                });
            }
            return data;
        }


        //Adpated from https://stackoverflow.com/questions/11463734/split-a-list-into-smaller-lists-of-n-size
        public static IEnumerable<List<List<string>>> SplitLists(this List<List<string>> data, int nSize = 20)
        {
            for (int i = 0; i < data.Count; i += nSize)
            {
                yield return data.GetRange(i, Math.Min(nSize, data.Count - i));
            }
        }
    }
}
