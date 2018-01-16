using Owin;
using Microsoft.Owin;
using Microsoft.AspNet.SignalR;
using System.Configuration;

[assembly: OwinStartup(typeof(MyWebChatApp.Startup))]

namespace MyWebChatApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Здесь происходят все соединения и конфигурации Hub-а
            // Сообщаем хабу о том, что у нас в качестве backplane выступает MS SQL Server.
            // Есть 2 варианта - запихнуть connection string сюда и пересобирать приложение или
            // дать ей имя и записать в web.config, который гораздо проще редактировать без пересобирания приложения.

            string sqlConnectionString = (ConfigurationManager.ConnectionStrings["mywebchat"].ConnectionString);
            GlobalHost.DependencyResolver.UseSqlServer(sqlConnectionString);
            app.MapSignalR();
        }
    }
}
/*В самой базе, кроме ее создания, нужно сделать:
 ALTER DATABASE mywebchat SET ENABLE_BROKER
 SELECT [name], [service_broker_guid], [is_broker_enabled] FROM [master].[sys].[databases]*/
