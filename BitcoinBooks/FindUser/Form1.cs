using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindUser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void userButton_Click(object sender, EventArgs e)
        {
            string user = SQL.GetUsernameOfAddress(Address.Text);
            if (user != "")
                MessageBox.Show(user);
            else
            {
                MessageBox.Show("No matching user found");
            }
        }
    }
}
