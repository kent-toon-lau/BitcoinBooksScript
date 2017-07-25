using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation
{
    public class Chat
    {
        public string ChatID;
        public List<ChatMessage> Messages;

        public Chat(string chatID, List<ChatMessage> messages)
        {
            this.ChatID = chatID;
            this.Messages = messages;
        }

    }
}
