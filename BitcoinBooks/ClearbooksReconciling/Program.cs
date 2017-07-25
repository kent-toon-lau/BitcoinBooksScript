using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearbooksReconciling
{
    class Program
    {
        static void Main(string[] args)
        {
            string source1 = @"C:\Users\Nobel360\Desktop\ContraBTN.csv";
            string source2 = @"C:\Users\Nobel360\Desktop\ContraGBP.csv";
            List<List<string>> csvBTN = new List<List<string>>();
            List<List<string>> csvGBP = new List<List<string>>();

            csvBTN = CSVEditor.Read(source1);
            csvGBP = CSVEditor.Read(source2);



        }
    }
}
