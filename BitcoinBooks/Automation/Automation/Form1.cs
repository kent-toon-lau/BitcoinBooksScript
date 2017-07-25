using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
   

namespace Automation
{
    public partial class Form1 : Form
    {
       
        private string source3 = @"C:\Users\Nobel360\Desktop\terms\";

        private IWebDriver _driver;
        public Form1()
        {
            InitializeComponent();
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://secure.clearbooks.co.uk/airmilylimited/accounting/money/import-manage/?bank_id=7502002&bank_import_log=16&page=1&importid=9309");


            //_csvBTN = new List<List<string>>();
            //_csvGBP = new List<List<string>>();

            //_csvBTN = CSVEditor.Read(source1);
            //_csvGBP = CSVEditor.Read(source2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                while (true)
                {
                    CompleteTransfer();
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }


        private void CompleteTransfer()
        {
            //get transaction ID from URL
            string transactionID = "";
            Uri uri = new Uri(_driver.Url);
            foreach(var query in uri.Query.Split('&'))
            {
                try
                {
                    if (query.Substring(0, 8) == "importid")
                        transactionID = "transaction-"+query.Substring(9, query.Length - 9);
                }
                catch(Exception e)
                { }

            }
            if (transactionID == "")
                throw new Exception("ID not set");



            //calculate the amount to input

            DateTime date = _driver.FindElement(By.Id(transactionID)).FindElements(By.TagName("td"))[0].Text.ToDateTime();
            double amountBTN = Math.Abs(_driver.FindElement(By.Id(transactionID)).FindElements(By.TagName("td"))[2].Text
                .ToDouble());
            string desc = _driver.FindElement(By.Id(transactionID)).FindElements(By.TagName("td"))[1].Text;
            string bitcoinAddress = desc.Substring(24).Trim();

            //display chats on screen with matching address

            int i = 0;
            foreach (var chat in SQL.GetChatsReferencing(bitcoinAddress))
            {
                i++;
                ChatHistory window = new ChatHistory(bitcoinAddress, chat);
                window.Show();
            }
            if (i == 0)
            {
                MessageBox.Show("No records found");
            }

            //double amountGBP = GetGBPValueFor(date, amountBTN, desc);

            //if (amountGBP == 0.0)
                //throw new Exception("Value not set!");

            //enter amount into box
            //_driver.FindElement(By.Id("bank[reporting_amount]")).SendKeys(amountGBP.ToString());

            //click button
            //foreach (var element in _driver.FindElements(By.TagName("button")))
            //{
            //    if (element.Text == "Confirm transfer")
            //    {
            //        element.Click();
            //        return;
            //    }
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                CompleteTransfer();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
            
        }

        //private double GetGBPValueFor(DateTime date, double btnAmount, string desc)
        //{
        //    foreach (var item in _csvGBP)
        //    {
        //        if (item[0].ToDateOnly() == date)
        //        {
        //            if (item[3] == desc)
        //            {
        //                return item[2].ToDouble();
        //            }
        //        }
        //    }
        //    return 0.0;
        //}


    }
}
