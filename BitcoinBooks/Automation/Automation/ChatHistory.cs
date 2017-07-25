using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automation
{
    public partial class ChatHistory : Form
    {
        private string _address;
        private Chat _chat;

        public ChatHistory(string bitcoinAddress, Chat chat)
        {
            InitializeComponent();
            _address = bitcoinAddress;
            _chat = chat;

            address.Text = _address;

            this.Text += _chat.ChatID;
            foreach (var message in _chat.Messages)
            {
                messageBox.AppendText("\n"+message.Sent.ToString()+" - "+message.Sender+": "+message.Message);
            }

        }
    }
}
