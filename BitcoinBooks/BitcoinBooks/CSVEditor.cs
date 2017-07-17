using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinBooks
{
    public static class CSVEditor
    {
        public static List<List<string>> Read(string address)
        {
            try
            {
                List<List<string>> csv = new List<List<string>>();

                using (var reader = new StreamReader(address))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',').ToList();
                        if (values[0] != "")
                            csv.Add(values);
                    }
                }
                return csv;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static bool Write(string address, List<List<string>> data)
        {
                using (var writer = new StreamWriter(address))
                {
                    // Adapted from https://stackoverflow.com/questions/17339922/c-sharp-how-to-write-a-listliststring-to-csv-file
                    data.ForEach(line =>
                    {

                        var lineArray = line.Select(c =>
                            c.Contains(",") ? c.Replace(",".ToString(), "\\" + ",") : c).ToArray();
                        writer.WriteLine(string.Join(",", lineArray));

                    });
                }
                return true;

        }
    }
}
