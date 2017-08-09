using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUser
{
    public class ChatMessage
    {
        public string Message;
        public DateTime Sent;
        public string Sender;

        public ChatMessage(string message, DateTime sent, string sender)
        {
            this.Message = message;
            this.Sent = sent;
            this.Sender = sender;
        }
    }
}
