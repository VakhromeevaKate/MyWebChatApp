using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading;
using Microsoft.AspNet.SignalR.SqlServer;
using System.Data.EntityClient;
using System.Data;
using System.Data.Common;
using System.Diagnostics;


namespace MyWebChatApp.Hubs
{
    public class ChatHub : Hub
    {

       
        public void Send(string name, string message)
        {
            // Зову метод addNewMessageToPage method - чтобы обновить список отправленных сообщений у всех подключенных клиентов.
            try
            {
                Thread serverTread = new Thread(Clients.All.addNewMessageToPage(name, message));
                serverTread.Start();
                Thread.Sleep(0);
            }
            catch (Exception e)
            {
                //что-то пошло не так.
                /*Log applog = new Log();
                applog.Write(e.Message);*/
            }

        }

        [HubMethodName("sendMessages")]
        public static void SendMessages()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.UpdateMessages();
        }

        [HubMethodName("getAllMessages")]
        public void GetAllMessages()
        {
            try
            {
                IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                Thread serverTread = new Thread(Clients.All.GetMessages());
                serverTread.Start();
                Thread.Sleep(0);
            }
            catch (Exception e)
            {
                //что-то пошло не так.
                /*Log applog = new Log();
                applog.Write(e.Message);*/
            }


        }
        
    }
}