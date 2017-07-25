using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClearbooksAutomation
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
                writer.AutoFlush = true;
                foreach (var line in data)
                {
                    writer.WriteLine(string.Join(",", line));
                }
            }
            return true;
        }
    }
}
