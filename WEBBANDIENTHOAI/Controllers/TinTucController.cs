using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANDIENTHOAI.Models;

namespace WEBBANDIENTHOAI.Controllers
{
    public class TinTucController : Controller
    {
        private ApplicationDbContext data = new ApplicationDbContext();
        // GET: TinTuc
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TinTuc_Partial()
        {
            var items = data.News.Take(4).ToList();
            return PartialView(items);
        }
    }
}