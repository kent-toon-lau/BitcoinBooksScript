using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ClearbooksAutomation
{
    public partial class Form1 : Form
    {
        private IWebDriver _driver;
        public Form1()
        {
            InitializeComponent();
            string source1 = @"C:\Users\Nobel360\Desktop\ContraBTN.csv";
            string source2 = @"C:\Users\Nobel360\Desktop\ContraGBP.csv";
            List<List<string>> csvBTN = new List<List<string>>();
            List<List<string>> csvGBP = new List<List<string>>();

            csvBTN = CSVEditor.Read(source1);
            csvGBP = CSVEditor.Read(source2);

            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://secure.clearbooks.co.uk/airmilylimited/accounting/money/import-manage/?bank_id=7502003&bank_import_log=6&importid=6237&page=1&tried=1&tab=transfer#6237");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //find equivilent bitcoin value here



            string amountToConvert = _driver.FindElement(By.Id("transaction-6237")).FindElements(By.TagName("td"))[2]
                .Text;



        }
    }
}
