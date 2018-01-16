using MyWebChatApp.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace MyWebChatApp.Models
{
    public class MessagesRepository
        
    {
        static Encoding enc8 = Encoding.UTF8;
        readonly string _connString = ConfigurationManager.ConnectionStrings["mywebchat"].ConnectionString;

        public IEnumerable<Messages> GetAllMessages()
        {
            var messages = new List<Messages>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT payloadid, payload, insertedon FROM SignalR.Messages_0 ORDER BY insertedon DESC", connection))
                {
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        messages.Add(item: new Messages {PayLoadId = Convert.ToInt64(reader["payloadid"]),PayLoad = GetNessesaryOnly(FromBytes((byte[])reader["payload"])), InsertedOn = Convert.ToDateTime(reader["insertedon"])});
                    }
                }

            }
            return messages;


        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                ChatHub.SendMessages();
            }
        }

        private string FromBytes(byte[] input)
        {
            string output = String.Empty;

            output = enc8.GetString(input);
            return output;
        }

        private string GetNessesaryOnly(string input)
        {
            string output = String.Empty;
            int startindex = input.IndexOf("[", StringComparison.CurrentCulture);
            int endindex = input.IndexOf("]", StringComparison.CurrentCulture);
            int len = endindex - startindex;
            if (len > 0 && startindex >=0 && endindex >=0)
            {
                output = input.Substring(startindex + 1, len -1 );
            }
                    
            return output;
        }
    }
}