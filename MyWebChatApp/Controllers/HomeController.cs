using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebChatApp.Models;

namespace MyWebChatApp.Controllers
{
    public class HomeController : Controller
    {
        //Только чат. Остальное, что шаблон нагенерил самостоятельно, стерто а ненадобностью.
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult History()
        {
            ViewBag.Message = "Message history";

            return View();
        }

        public ActionResult GetMessages()
        {
            MessagesRepository _messageRepository = new MessagesRepository();
            return PartialView("_MessagesList", _messageRepository.GetAllMessages());
        }
    }
}