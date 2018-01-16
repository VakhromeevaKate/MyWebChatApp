using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebChatApp.Models
{
    public class Messages
    {
        public Int64 PayLoadId { get; set; }
        public string PayLoad { get; set; }
        public DateTime InsertedOn { get; set; }
    }
}
