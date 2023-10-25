using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANDIENTHOAI.Models;
using WEBBANDIENTHOAI.Models.MFC;

namespace WEBBANDIENTHOAI.Areas.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        private ApplicationDbContext data = new ApplicationDbContext();
        // GET: Admin/ProductImage
        public ActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            var items = data.ProductImages.Where(x => x.ProductId == id).ToList();
            return View(items);
        }

        [HttpPost]
        public ActionResult AddImage(int productId, string url)
        {
            data.ProductImages.Add(new ProductImage
            {
                ProductId = productId,
                Image = url,
                IsDefault = false
            });
            data.SaveChanges();
            return Json(new { Success = true });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = data.ProductImages.Find(id);
            data.ProductImages.Remove(item);
            data.SaveChanges();
            return Json(new { success = true });
        }
    }
}