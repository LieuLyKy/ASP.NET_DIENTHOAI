using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANDIENTHOAI.Models;
using WEBBANDIENTHOAI.Models.MFC;

namespace WEBBANDIENTHOAI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Quanliphu")]
    //[Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext data = new ApplicationDbContext();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var items = data.Categories;
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WEBBANDIENTHOAI.Models.Common.Filter.FilterChar(model.Title);
                data.Categories.Add(model);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var item = data.Categories.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                data.Categories.Attach(model);
                model.ModifiedDate = DateTime.Now;
                model.Alias = WEBBANDIENTHOAI.Models.Common.Filter.FilterChar(model.Title);
                data.Entry(model).Property(x => x.Title).IsModified = true;
                data.Entry(model).Property(x => x.Description).IsModified = true;
                data.Entry(model).Property(x => x.Alias).IsModified = true;
                data.Entry(model).Property(x => x.SeoDescription).IsModified = true;
                data.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
                data.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                data.Entry(model).Property(x => x.Position).IsModified = true;
                data.Entry(model).Property(x => x.ModifiedDate).IsModified = true;
                data.Entry(model).Property(x => x.Modifiedby).IsModified = true;
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = data.Categories.Find(id);
            if (item != null)
            {
                //var DeleteItem = db.Categories.Attach(item);
                data.Categories.Remove(item);
                data.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}