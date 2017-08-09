using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUser
{
    public static class ExtensionMethods
    {
        public static DateTime ToDateTime(this string s)
        {
            return Convert.ToDateTime(s);
        }

        public static int ToInt(this string s)
        {
            return Int32.Parse(s);
        }

        public static double ToDouble(this string s)
        {
            return Double.Parse(s);
        }

        public static DateTime ToDateOnly(this string str)
        {
            //Expected format dd/mm/yyyy HH:mm

            //Assumes the string is actually a date time and in the right format, or it will throw exception
            //Times are truncated off

            var strDay = str.Substring(0, 2).ToInt();
            var strMonth = str.Substring(3, 2).ToInt();
            var strYear = str.Substring(6, 4).ToInt();

            return new DateTime(strYear, strMonth, strDay);
        }
    }
}
