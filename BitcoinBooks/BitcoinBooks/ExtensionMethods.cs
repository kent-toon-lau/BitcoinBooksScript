using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;



namespace BitcoinBooks
{
    public static class ExtensionMethods
    {
        public static DateTime ToDateTime(this string str)
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

        public static DateTime ToDateTimeEng(this string str)
        {
            //Expected format dd/mm/yyyy HH:mm

            //Assumes the string is actually a date time and in the right format, or it will throw exception
            //Times are truncated off

            var strDay = str.Substring(0, 2).ToInt();
            var strMonth = str.Substring(3, 2).ToInt();
            var strYear = str.Substring(6, 4).ToInt();

            return new DateTime(strYear, strMonth, strDay);
        }

        public static double ToCurrency(this string str)
        {
            //Assumes the string is actually a number of some kind that can be represented as a double, or it will throw exception
            if (str == " ")
                return 0.0;
            return double.Parse(str, CultureInfo.InvariantCulture.NumberFormat);  

        }

        public static int ToInt(this string str)
        {
            //Assumes it actually is a valid int, will throw an exception if not
            return int.Parse(str, CultureInfo.InvariantCulture.NumberFormat);
        }

        public static List<List<string>> ToOutsList(this List<Transaction> transactions)
        { 
            List<List<string>> data = new List<List<string>>();
            foreach (Transaction transaction in transactions.Where(t => t.Type == TransactionType.Out))
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
            foreach (Transaction transaction in transactions.Where(t => t.Type == TransactionType.In))
            {
                if (transaction.Description.Substring(0, 7) != "Contact")
                {
                    data.Add(new List<string>()
                    {
                        transaction.Date.ToString("dd/MM/yyyy HH:mm"),
                        " ",
                        transaction.GBP_Value.ToString(),
                        transaction.Description
                    });
                }
            }
            return data;
        }

        public static List<List<string>> ToTransactionsAndTransfersOut(this List<Transaction> transactions)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (Transaction transaction in transactions.Where(t => t.Type == TransactionType.Out))
            {
                try
                {
                    if ((transaction.Description.Substring(0, 8) == "Internal") ||
                        (transaction.Description.Substring(0, 7) == "Send to"))
                    {
                        data.Add(new List<string>()
                        {
                            transaction.Date.ToString("dd/MM/yyyy HH:mm"),
                            " ",
                            transaction.BTN_Value.ToString(),
                            transaction.Description
                        });
                    }
                }
                catch (Exception e)
                {
                }

            }
            return data;
        }

        public static List<List<string>> ToDuplicateReserve(this List<Transaction> transactions)
        {
            List<List<string>> reserveList = new List<List<string>>();
            List<List<string>> data = new List<List<string>>();
            foreach (Transaction transaction in transactions.Where(t => t.Type == TransactionType.Out))
            {
                try
                {
                    if (transaction.Description.Substring(0, 16) == "\"Bitcoin reserve")
                    {
                        reserveList.Add(new List<string>()
                        {
                            transaction.Date.ToString("dd/MM/yyyy HH:mm"),
                            " ",
                            transaction.BTN_Value.ToString(),
                            transaction.Description
                        });
                    }
                }
                catch (Exception e)
                {
                }

            }

            foreach (var transaction in reserveList)
            {
                if (amountOfDupes(transaction[2], reserveList) > 0)
                {
                    data.Add(transaction);
                }
            }

            return data;
        }

        public static List<List<string>> ToReserveBounce(this List<Transaction> transactions)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (Transaction transaction in transactions.Where(t => t.Type == TransactionType.Out))
            {
                try
                {
                    if (transaction.Description.Substring(0, 15) == "Bitcoin reserve")
                    {
                        string[] desc_parts = transaction.Description.Split('|');
                        if (bounced(desc_parts[1], transactions))
                        {
                            data.Add(new List<string>()
                            {
                                transaction.Date.ToString("dd/MM/yyyy HH:mm"),
                                " ",
                                transaction.BTN_Value.ToString(),
                                desc_parts[1] + " - " + desc_parts[0]
                            });
                        }
                    }
                }
                catch (Exception e)
                {
                }

            }

            return data;
        }


        public static bool bounced(string fullContact, List<Transaction> transactions)
        {
            string contactID = fullContact.Trim().Substring(0, 17).TrimEnd();
            foreach (Transaction transaction in transactions.Where(t => t.Type == TransactionType.In))
            {
                if (transaction.Description == contactID + " trade canceled")
                {
                    return true;
                }
            }
            return false;
        }

        private static int amountOfDupes(string amount, List<List<string>> items)
        {
            int i = -1;
            foreach (var trans in items)
            {
                if (trans[2] == amount)
                {
                    i++;
                }
            }
            return i;
        }

        public static List<List<string>> ToInsListBitcoin(this List<Transaction> transactions)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (Transaction transaction in transactions.Where(t => t.Type == TransactionType.In))
            {
                if (transaction.Description.Substring(0, 7) != "Contact")
                {
                    data.Add(new List<string>()
                    {
                        transaction.Date.ToString("dd/MM/yyyy HH:mm"),
                        " ",
                        transaction.BTN_Value.ToString(),
                        transaction.Description
                    });
                }
            }
            return data;
        }

        public static List<List<string>> ToCancelledAndTimeoutList(this List<Transaction> transactions)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (Transaction transaction in transactions.Where(t => t.Type == TransactionType.In))
            {
                if (transaction.Description.Substring(0, 7) == "Contact")
                {
                    data.Add(new List<string>()
                    {
                        transaction.Date.ToString("dd/MM/yyyy HH:mm"),
                        " ",
                        transaction.BTN_Value.ToString(),
                        transaction.Description
                    });
                }
            }
            return data;
        }

        public static List<List<string>> ToReserversList(this List<Transaction> transactions)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (Transaction transaction in transactions.Where(t => t.Type == TransactionType.Out))
            {
                try
                {
                    string desc = transaction.Description.Substring(0, 14);
                    if (desc == "\"Bitcoin reser")
                    {
                        data.Add(new List<string>()
                        {
                            transaction.Date.ToString("dd/MM/yyyy HH:mm"),
                            " ",
                            transaction.BTN_Value.ToString(),
                            transaction.Description
                        });
                    }
                }
                catch (Exception e)
                { }

            }
            return data;
        }

        public static List<List<string>> ToSalesList(this List<Transaction> transactions)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (Transaction transaction in transactions.Where(t => t.Type == TransactionType.Out))
            {
                try
                {
                    string desc = transaction.Description.Substring(0, 12);
                    if (desc == "Bitcoin sell")
                    {
                        data.Add(new List<string>()
                        {
                            transaction.Date.ToString("dd/MM/yyyy HH:mm"),
                            " ",
                            transaction.BTN_Value.ToString(),
                            transaction.Description
                        });
                    }
                }
                catch (Exception e)
                { }

            }
            return data;
        }

        private static int CountOccurances(List<string> incomingIDs, string id)
        {
            int i = 0;
            foreach (string checkID in incomingIDs)
            {
                if (checkID == id)
                    i++;
            }
            return i;
        }

        public static List<List<string>> ToSuccesfulSalesList(this List<Transaction> transactions)
        {
            List<List<string>> data = new List<List<string>>();
            List<string> incomingIDs = new List<string>();

            foreach (Transaction transaction in transactions.Where(t => t.Type == TransactionType.In))
            {
                try
                {
                    if (transaction.Description.Substring(0, 7) == "Contact")
                    {
                        string id = transaction.Description.Substring(9, 7);
                        incomingIDs.Add(id);
                    }
                }
                catch (Exception e)
                { }
            }


            foreach (Transaction transaction in transactions.Where(t => t.Type == TransactionType.Out))
            {
                try
                {
                    string desc = transaction.Description.Substring(0, 12);
                    if (desc == "Bitcoin sell")
                    {
                        string id = transaction.Description.Substring(14, 7);
                        if (!incomingIDs.Contains(id))
                        {
                            data.Add(new List<string>()
                            {
                                transaction.Date.ToString("dd/MM/yyyy HH:mm"),
                                " ",
                                transaction.BTN_Value.ToString(),
                                transaction.Description
                            });
                        }
                    }
                    else if (transaction.Description.Substring(0, 7) == "Contact")
                    {
                        string id = transaction.Description.Substring(9, 7);
                        int occurances = CountOccurances(incomingIDs, id);
                        if (CountOccurances(incomingIDs, id) == 1)
                        {
                            data.Add(new List<string>()
                            {
                                transaction.Date.ToString("dd/MM/yyyy HH:mm"),
                                " ",
                                transaction.BTN_Value.ToString(),
                                transaction.Description
                            });
                        }
                    }
                }
                catch (Exception e)
                { }

            }
            return data;
        }

        public static List<List<string>> ToBoughtBitCoinsList(this List<Transaction> transactions)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (Transaction transaction in transactions.Where(t => t.Type == TransactionType.In))
            {
                string s = transaction.Description.Substring(9,7);
                if ( (Regex.IsMatch(s, "[0-9][0-9][0-9][0-9][0-9][0-9][0-9]"))&&(transaction.Description.Substring(17,1) == "r") )
                {
                    data.Add(new List<string>()
                    {
                        transaction.Date.ToString("dd/MM/yyyy HH:mm"),
                        transaction.BTN_Value.ToString(),
                        " ",
                        transaction.Description
                    });
                }
            }
            return data;
        }


        public static List<List<string>> ToFeesList(this List<Transaction> transactions)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (Transaction transaction in transactions.Where(t => t.Type == TransactionType.Out))
            {
                if (transaction.Description.Substring(0, 3) == "fee")
                {
                    data.Add(new List<string>()
                    {
                        transaction.Date.ToString("dd/MM/yyyy HH:mm"),
                        " ",
                        transaction.BTN_Value.ToString(),
                        transaction.Description
                    });
                }
            }
            return data;
        }

        public static List<List<string>> ToFullInsList(this List<Transaction> transactions)
        {
            List<List<string>> data = new List<List<string>>();
            data.Add(new List<string>()
            {
                "Date",
                "GBP Value",
                "BTN Value (x1,000,000)",
                "Conversion Rate (x1,000,000)",
                "Description"
            });

            foreach (Transaction transaction in transactions.Where(t => t.Type == TransactionType.In))
            {
                data.Add(new List<string>()
                {
                    transaction.Date.ToString("dd/MM/yyyy HH:mm"),
                    transaction.GBP_Value.ToString(),
                    transaction.BTN_Value.ToString(),
                    (transaction.ConversionRate*1000000).ToString(),
                    transaction.Description
                });
            }
            return data;
        }


        //Adpated from https://stackoverflow.com/questions/11463734/split-a-list-into-smaller-lists-of-n-size
        public static IEnumerable<List<List<string>>> SplitLists(this List<List<string>> data, int nSize = 25)
        {
            for (int i = 0; i < data.Count; i += nSize)
            {
                yield return data.GetRange(i, Math.Min(nSize, data.Count - i));
            }
        }

        public static string ExtractID(this string working)
        {
            string id;
            string str = working.Trim();
            if (str.Substring(0, 2) == "Bi")
            {
                id = str.Substring(14, 8).TrimEnd();
            }
            else if (str.Substring(0, 2) == "Co")
            {
                id = str.Substring(9, 8).TrimEnd();
            }
            else
            {
                id = "N/A";
            }
            return id;
        }
    }
}
