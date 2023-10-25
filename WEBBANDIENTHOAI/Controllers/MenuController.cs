using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANDIENTHOAI.Models;

namespace WEBBANDIENTHOAI.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext data = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        //ngoài trang index home
        public ActionResult MenuTop()
        {
            var items = data.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop", items);
        }
        //ngoài trang index home
        public ActionResult MenuProductCategory()
        {
            var items = data.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", items);
        }

        //ngoài trang index home
        public ActionResult MenuProductRight(int? id)
        {
            if (id != null)
            {
                ViewBag.CateId = id;
            }
            var items = data.ProductCategories.ToList();
            return PartialView("_MenuProductRight", items);
        }


        // trong trang Product
        public ActionResult MnTraiProduct(int? id)
        {
            if (id != null)
            {
                ViewBag.CateId = id;
            }
            var items = data.ProductCategories.ToList();
            return PartialView("_MnTraiProduct", items);
        }
    }
}