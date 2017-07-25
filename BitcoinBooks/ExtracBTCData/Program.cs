using System;
using System.Collections.Generic;
using ExtraBTCData;

namespace ExtracBTCData
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = @"C:\Users\Nobel360\Desktop\leftovers.csv";

            //string source2 = @"C:\Users\Nobel360\Desktop\outgoing_btn.csv";
            //string dest = @"C:\Users\Nobel360\Desktop\leftovers.csv";
            //string dest2 = @"C:\Users\Nobel360\Desktop\outgoing_GBP.csv";
            //string dest = @"C:\Users\Nobel360\Desktop\bitcoin_in.csv";

            string dest = @"C:\Users\Nobel360\Desktop\outgoing_leftovers_btn.csv";
            string dest2 = @"C:\Users\Nobel360\Desktop\outgoing_leftovers_gbp.csv";

            List<List<string>> outgoing_complete = CSVEditor.Read(source);

            CSVEditor.Write(dest, outgoing_complete.ToBTNList());
            CSVEditor.Write(dest2, outgoing_complete.ToGBPList());
        }
    }
}