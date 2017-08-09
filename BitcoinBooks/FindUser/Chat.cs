using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUser
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

        public string OtherUser
        {
            get
            {
                foreach(ChatMessage message in Messages)
                {
                    if (message.Sender != "Sender: neo210211")
                    {
                        return message.Sender;
                    }
                }
                return "";
            }
    
        }

    }
}
