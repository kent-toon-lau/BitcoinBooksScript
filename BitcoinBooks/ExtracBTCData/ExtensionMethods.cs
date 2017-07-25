using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ExtracBTCData
{
    public static class ExtensionMethods
    {
        public static DateTime ToDateTime(this string s)
        {
            return Convert.ToDateTime(s);
        }

        public static bool CompareDates(this DateTime date1, DateTime date2)
        {
            if (date1.Year == date2.Year)
                if(date1.Month == date2.Month)
                    if (date1.Day == date2.Day)
                        return true;
            return false;
        }

        public static List<List<string>> ToSoldList(this List<List<string>> transactions)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (var transaction in transactions)
            {
                if (transaction[3] == "neo210211")
                {
                    if(transaction[15] != "")
                        data.Add(transaction);
                }
            }
            return data;
        }

        public static int FindPositionOfInComplete(this List<List<string>> transactions, string id, DateTime date, string btnAmount)
        {
            int i = 0;
            foreach (var transaction in transactions)
            {
                string t_id = transaction[0];
                DateTime t_date = transaction[1].ToDateTime();
                string t_amount = transaction[4];
                if ( (t_id == id) && (t_date.CompareDates(date)) && (t_amount == btnAmount) )
                {
                    return i;
                }
                else
                {
                    i++;
                }
            }
            return -1;
        }

        public static int FindPositionOf(this List<List<string>> transactions, string id, DateTime date, string btnAmount)
        {
            int i = 0;
            foreach (var list in transactions)
            {
                string t_id = list[3].Substring(14, 8).Trim();
                DateTime t_date = list[0].ToDateTime();
                string t_amount = list[2];
                if ((t_id == id) && (t_date.CompareDates(date)) && (t_amount == btnAmount))
                {
                    return i;
                }
                else
                {
                    i++;
                }
            }
            return -1;
        }


        public static List<List<string>> ToSucessGBPList(this List<List<string>> transactions, List<List<string>> gbpValues)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (var transaction in transactions)
            {
                string t_id = transaction[3].Substring(14, 8).Trim();
                DateTime t_date = transaction[0].ToDateTime();
                string t_amount = transaction[2];
                int index = gbpValues.FindPositionOfInComplete(t_id, t_date, t_amount);
                if (index < 0)
                    throw new Exception("Couldn't find item in the complete list");

                data.Add(new List<string>()
                {
                    transaction[0],
                    " ",
                    gbpValues[index][5],
                    transaction[3]
                });
            }
            return data;
        }

        public static List<List<string>> ToLeftoverList(this List<List<string>> transactions_sucess, List<List<string>> transactions_complete)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (var transaction in transactions_complete)
            {
                string t_id = transaction[0];
                DateTime t_date = transaction[1].ToDateTime();
                string t_amount = transaction[4];

                if (transactions_sucess.FindPositionOf(t_id, t_date, t_amount) == -1)
                {
                    data.Add(transaction);
                }
            }
            return data;
        }

        public static List<List<string>> ToBitcoinInsList(this List<List<string>> transactions_complete)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (var transaction in transactions_complete)
            {
                if ( (transaction[2] == "neo210211")&&(transaction[15] != ""))
                {
                    data.Add(transaction);
                }
            }
            return data;
        }

        public static List<List<string>> ToBTNList(this List<List<string>> transactions)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (var transaction in transactions)
            {
                string desc = "Bitcoin sell #"+transaction[0]+" "+transaction[2];
                data.Add(new List<string>()
                {
                    transaction[1].ToDateTime().ToString(),
                    " ",
                    transaction[4],
                    desc
                });
            }
            return data;
        }

        public static List<List<string>> ToGBPList(this List<List<string>> transactions)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (var transaction in transactions)
            {
                string desc = "Bitcoin sell #" + transaction[0] + " " + transaction[2];
                data.Add(new List<string>()
                {
                    transaction[1].ToDateTime().ToString(),
                    " ",
                    transaction[5],
                    desc
                });
            }
            return data;
        }

    }
}
