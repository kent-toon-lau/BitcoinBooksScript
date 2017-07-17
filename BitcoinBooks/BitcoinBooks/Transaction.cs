using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinBooks
{
    public class Transaction
    {
        private double _standardBitcoinValue;

        public TransactionType Type { get; }
        public string Description { get; }
        public DateTime Date { get; }
        public double ConversionRate { get; }

        public double BTN_Value
        {
            get { return _standardBitcoinValue * 1000000; }
        }

        public double GBP_Value
        {
            get { return Math.Round(_standardBitcoinValue*ConversionRate, 2); }
        }

        public Transaction(List<string> data)
        {
            Date = data[0].ToDateTime();
            if (data[1] == " ")
            {
                Type = TransactionType.Out;
                _standardBitcoinValue = data[2].ToCurrency();
            }
            else
            {
                Type = TransactionType.In;
                _standardBitcoinValue = data[1].ToCurrency();
            }

            ConversionRate = Conversion.GetBTCToGBPRateOn(Date);
            Description = data[3];
        }

        

    }
}
