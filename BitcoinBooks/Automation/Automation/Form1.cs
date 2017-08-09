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
using OpenQA.Selenium.Support.UI;


namespace Automation
{
    public partial class Form1 : Form
    {
        private string accNum = "71612657";
        private string source;
        List<List<string>> _csvValues;

        private IWebDriver _driver;
        public Form1()
        {
            InitializeComponent();
            source = @"C:\Users\Nobel360\Desktop\"+accNum+".csv";
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://secure.clearbooks.co.uk/airmilylimited/accounting/banking/list/");

            _csvValues = CSVEditor.Read(source);


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
            ////get transaction ID from URL
            //string transactionID = "";
            //Uri uri = new Uri(_driver.Url);
            //foreach(var query in uri.Query.Split('&'))
            //{
            //    try
            //    {
            //        if (query.Substring(0, 8) == "importid")
            //            transactionID = "transaction-"+query.Substring(9, query.Length - 9);
            //    }
            //    catch(Exception e)
            //    { }

            //}
            //if (transactionID == "")
            //    throw new Exception("ID not set");

            //SelectElement dropdown = new SelectElement(_driver.FindElement(By.Id("bank_bank_to")));
            //dropdown.SelectByText("Boon");
            //SelectElement dropdown = new SelectElement(_driver.FindElement(By.Id("bank_bank_from")));
            //dropdown.SelectByText("Contra GBP");
            //System.Threading.Thread.Sleep(2000);

            //calculate the amount to input

            //DateTime date = _driver.FindElement(By.Id(transactionID)).FindElements(By.TagName("td"))[0].Text.ToDateTime();
            //double amountBTN = Math.Abs(_driver.FindElement(By.Id(transactionID)).FindElements(By.TagName("td"))[2].Text
                //.ToDouble());
            //double amountBTN = Math.Abs(_driver.FindElement(By.Id(transactionID)).FindElements(By.TagName("td"))[3].Text
                //.ToDouble());
            //string desc = _driver.FindElement(By.Id(transactionID)).FindElements(By.TagName("td"))[1].Text;
            //string bitcoinAddress = desc.Substring(24).Trim();

            //display chats on screen with matching address

            //int i = 0;
            //foreach (var chat in SQL.GetChatsReferencing(bitcoinAddress))
            //{
            //i++;
            //ChatHistory window = new ChatHistory(bitcoinAddress, chat);
            //window.Show();
            // }
            // if (i == 0)
            // {
            //MessageBox.Show("No records found");
            //}

            //double amountGBP = GetGBPValueFor(date, amountBTN, desc);

            //if (amountGBP == 0.0)
            //throw new Exception("Value not set!");

            //_driver.FindElement(By.Id("bank[reporting_amount]")).SendKeys(Math.Round(amountGBP, 2).ToString());
            //foreach (var element in _driver.FindElements(By.TagName("button")))
            //{
            //if (element.Text == "Confirm transfer")
            //{
            //element.Click();
            //System.Threading.Thread.Sleep(500);
            //return;
            //}
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var line in _csvValues)
                {
                    _driver.Navigate().GoToUrl("https://secure.clearbooks.co.uk/airmilylimited/accounting/banking/list/");
                    _driver.FindElement(By.LinkText(String.Format("BEIER360 Limited ({0})", accNum))).Click();
                    _driver.FindElement(By.LinkText("Money out")).Click();
                    _driver.FindElement(By.Id("money-out-amount")).Clear();
                    _driver.FindElement(By.Id("money-out-amount")).SendKeys(line[1]);
                    //string day = line[0].Substring(0, 2);
                    //string month = line[0].Substring(3, 2);
                    //string year = line[0].Substring(6, 4);
                    //_driver.FindElement(By.Name("manual_bank_date")).SendKeys(day + month + year);       
                    MessageBox.Show(line[0]);
                            
                    _driver.FindElement(By.Id("money-out-description")).SendKeys(line[2]);
                    _driver.FindElement(By.Id("money-out-submit")).Click();
                    _driver.FindElement(By.LinkText("Transaction")).Click();

                    SelectElement dropdown = new SelectElement(_driver.FindElement(By.Id("invoice_1_entity_id")));
                    dropdown.SelectByText("BEIER360 Limited");

                    SelectElement dropdown2 = new SelectElement(_driver.FindElement(By.Id("item[1][1][type]")));
                    MessageBox.Show(String.Format("Loans Receivable {0}",accNum));

                    SelectElement dropdown3 = new SelectElement(_driver.FindElement(By.Id("item_1_1_vat_rate")));
                    MessageBox.Show("Exclude from VAT return");

                    SelectElement dropdown4 = new SelectElement(_driver.FindElement(By.Id("invoice_1_vat_treatment")));
                    MessageBox.Show("Out of Scope - Not on VAT Return");
                    foreach (var element in _driver.FindElements(By.TagName("button")))
                    {
                        if (element.Text == "Add new transaction")
                        {
                            element.Click();
                            System.Threading.Thread.Sleep(500);
                            break;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private double GetGBPValueFor(DateTime date, double btnAmount, string desc)
        //{
        //    //foreach (var item in _csvGBP)
        //    //{
        //    //    if (item[0].ToDateOnly() == date)
        //    //    {
        //    //        if (item[3] == desc)
        //    //        {
        //    //            return item[2].ToDouble();
        //    //        }
        //    //    }
        //    //}
        //    //return 0.0;


        //    return (btnAmount/1000000) * Conversion.GetBTCToGBPRateOn(date);
        //}


    }
}
