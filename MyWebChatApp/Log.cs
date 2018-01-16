using System;

namespace MyWebChatApp
{
    public class Log
    {
        public void Write(string msg)
        {
            DateTime currtime = DateTime.Now;

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"log.txt", true))
            {
                string tmptxt = String.Format("{0:yyMMdd hh:mm:ss} {1}", currtime, msg);
                file.WriteLine(tmptxt);
                file.Close();
            }
        }
    }
}