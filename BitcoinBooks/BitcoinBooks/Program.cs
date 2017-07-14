using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinBooks
{
    class Program
    {
        private static ProgressBar _bar;
        private static int _finishedCount = 0;
        private static int _total = 1;
        private static List<Task> _tasks = new List<Task>();
        private static List<Transaction> _transactions = new List<Transaction>();

        static void Main(string[] args)
        {
            try
            {
                string source = @"C:\Users\Nobel360\Desktop\values.csv";
                //string source = args[0];
                string dest1 = new FileInfo(source).Directory.FullName + @"\LBC Account.csv";
                string dest2 = new FileInfo(source).Directory.FullName + @"\Contra GBP.csv";
                string dest3 = new FileInfo(source).Directory.FullName + @"\Full Data.csv";

                var chunks = CSVEditor.Read(source);
                int listLength = (int)Math.Ceiling((double) chunks.Count / 100);

                using (_bar = new ProgressBar())
                {
                    foreach (var chunk in chunks.SplitLists(listLength))
                    {
                        _total++;
                        _tasks.Add(Task.Factory.StartNew(() => ProcessList(chunk)));
                    }
                    Task.WaitAll(_tasks.ToArray());
                }

                CSVEditor.Write(dest1, _transactions.ToOutsList());
                CSVEditor.Write(dest2, _transactions.ToInsList());
                CSVEditor.Write(dest3, _transactions.ToFullInsList());
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("\nThat CSV file could not be found. Please verify the Path and FileName");
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    "\nThe File could not be processed. Please verify that the CSV file is not open in another process and that you have read and write permissions to the directory.");
            }

            Console.ReadKey();
        }

        private static void ProcessList(List<List<string>> transactionsData)
        {
            foreach (var transactionData in transactionsData)
            {
                _transactions.Add(new Transaction(transactionData));
            }
            NotifyFinished();
        }

        private static void NotifyFinished()
        {
            _finishedCount++;
            _bar.Report((double)_finishedCount / _total);
        }


    }
}
