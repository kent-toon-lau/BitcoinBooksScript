﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitcoinBooks
{
    class Program
    {
        private static ProgressBar _bar;
        private static int _finishedCount = 0;
        private static int _total = 0;
        private static List<Task> _tasks = new List<Task>();
        private static List<Transaction> _transactions = new List<Transaction>();

        static int CountOccurances(string id, List<string> list)
        {
            int i = 0;
            foreach (string item in list)
            {
                if (item == id)
                    i++;
            }
            return i;
        }



        static void Main(string[] args)
        {
            string source1 = @"C: \Users\Nobel360\Desktop\AprilActual.csv";
            string source2 = @"C: \Users\Nobel360\Desktop\AprilClear.csv";
            
            //string dest = new FileInfo(source).Directory.FullName + @"\oddnums_processed.csv";

            List<string> ids = new List<string>();
            List<List<string>> oddAmountOfID = new List<List<string>>();

            var actual = CSVEditor.Read(source1);
            var clear = CSVEditor.Read(source1);

            actual.Sort((x, y) => String.Compare(x.FirstOrDefault(), y.FirstOrDefault()));
            clear.Sort((x, y) => String.Compare(x.FirstOrDefault(), y.FirstOrDefault()));

            int i = 0;
            while (i < actual.Count)
            {
                if (actual[i][2].ToCurrency() != clear[i][2].ToCurrency())
                {
                    MessageBox.Show(actual[i][0]+"   |   "+ actual[i][1] + "   |   " + actual[i][2] + "   |   " + actual[i][3]+"\n\n"+ clear[i][0] + "   |   " + clear[i][1] + "   |   " + clear[i][2] + "   |   " + clear[i][3]);
                }
                i++;
            }


            //foreach (var list in lists)
            //{
            //    ids.Add(list[0].ExtractID());
            //}

            //foreach (string id in ids)
            //{
            //    int occurances = CountOccurances(id, ids);
            //    if (occurances % 2 != 0)
            //    {
            //        oddAmountOfID.Add(new List<string>() { id, occurances.ToString() });
            //    }
            //}
            //CSVEditor.Write(dest, oddAmountOfID);

            //try
            //{
            //    string source = @"C:\Users\Nobel360\Desktop\values.csv";
            //    //string source = args[0];
            //    string dest1 = new FileInfo(source).Directory.FullName + @"\remanining_outgoing.csv";

            //    var chunks = CSVEditor.Read(source);

            //    using (_bar = new ProgressBar())
            //    {
            //        foreach (var chunk in chunks.SplitLists())
            //        {
            //            _total++;
            //            _tasks.Add(Task.Factory.StartNew(() => ProcessList(chunk)));
            //        }
            //        Task.WaitAll(_tasks.ToArray());
            //    }


            //    _transactions.Sort((x, y) => DateTime.Compare(y.Date, x.Date));

            //    foreach (Transaction trans in _transactions)
            //    {
            //        if (trans.Type == null)
            //            throw new Exception(trans.ToString());
            //    }

            //    CSVEditor.Write(dest1, _transactions.ToTransactionsAndTransfersOut());
            //    Console.ReadKey();

            //}
            //catch (FileNotFoundException e)
            //{
            //    Console.WriteLine("\nThat CSV file could not be found. Please verify the Path and FileName");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(
            //        "\nThe File could not be processed. Please verify that the CSV file is not open in another process and that you have read and write permissions to the directory.");
            //}
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
