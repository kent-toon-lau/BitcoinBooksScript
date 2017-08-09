using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUser
{
    public static class SQL
    {
        public static List<Chat> allChats = new List<Chat>();

        static SQL()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "lbcbitcoin.database.windows.net";
                builder.UserID = "kent";
                builder.Password = "beier360!";
                builder.InitialCatalog = "LBC";
 
                List<string> chatIDs = new List<string>();
                using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
                { 
                    conn.Open();
                    string select = "SELECT * FROM [Chat]";
                    using (SqlCommand command = new SqlCommand(select, conn))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                chatIDs.Add(reader[0].ToString());
                            }
                        }
                    }


                    foreach (string id in chatIDs)
                    {
                        List<ChatMessage> messages = new List<ChatMessage>();
                        using (SqlCommand command = new SqlCommand("SELECT * FROM [Message] WHERE ChatID = @0", conn))
                        {
                            command.Parameters.Add(new SqlParameter("0", id));
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    messages.Add(new ChatMessage(reader[3].ToString(), reader[2].ToString().ToDateTime(),
                                        reader[4].ToString()));
                                }
                            }
                        }

                        allChats.Add(new Chat(id, messages));
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }


        public static List<Chat> GetChatsReferencing(string destAddress)
        {
            List<Chat> data = new List<Chat>();
            foreach (Chat chat in allChats)
            {
                foreach (var message in chat.Messages)
                {
                    if (message.Message.Contains(destAddress))
                    {
                        data.Add(chat);
                    }
                }
            }

            return data;
        }

        public static string GetUsernameOfAddress(string destAddress)
        {
            foreach (Chat chat in allChats)
            {
                foreach (var message in chat.Messages)
                {
                    if (message.Message.Contains(destAddress))
                    {
                        return chat.OtherUser;
                    }
                }
            }
            return "";
        }
    }
}
