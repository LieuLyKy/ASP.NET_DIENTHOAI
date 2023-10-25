using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANDIENTHOAI.Models;
using WEBBANDIENTHOAI.Models.MFC;

namespace WEBBANDIENTHOAI.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext data = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DangKy_Partial()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult DangKy(Subscribe req)
        {
            // check hộp lệ
            if (ModelState.IsValid)
            {
                data.Subscribes.Add(new Subscribe { Email = req.Email, CreatedDate = DateTime.Now });
                data.SaveChanges();
                return Json(new { Success = true });
            }
            return View("Partial_Subcrice", req);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Refresh()
        {
            var item = new ThongKeModel();

            ViewBag.Visitors_online = HttpContext.Application["visitors_online"];
            var hn = HttpContext.Application["HomNay"];
            item.HomNay = HttpContext.Application["HomNay"].ToString();
            item.HomQua = HttpContext.Application["HomQua"].ToString();
            item.TuanNay = HttpContext.Application["TuanNay"].ToString();
            item.TuanTruoc = HttpContext.Application["TuanTruoc"].ToString();
            item.ThangNay = HttpContext.Application["ThangNay"].ToString();
            item.ThangTruoc = HttpContext.Application["ThangTruoc"].ToString();
            item.TatCa = HttpContext.Application["TatCa"].ToString();
            return PartialView(item);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}