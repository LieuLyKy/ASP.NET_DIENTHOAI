using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANDIENTHOAI.Models;
using WEBBANDIENTHOAI.Models.MFC;
namespace WEBBANDIENTHOAI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Quanliphu")]
    public class PostsController : Controller
    {
        private ApplicationDbContext data = new ApplicationDbContext();
        // GET: Admin/Posts
        public ActionResult Index()
        {
            var items = data.Posts.ToList();
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Posts model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.CategoryId = 2005;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WEBBANDIENTHOAI.Models.Common.Filter.FilterChar(model.Title);
             
                data.Posts.Add(model);
                data.Entry(model).State = System.Data.Entity.EntityState.Added;
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var item = data.Posts.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Posts model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = WEBBANDIENTHOAI.Models.Common.Filter.FilterChar(model.Title);
                data.Posts.Attach(model);
                data.Entry(model).State = System.Data.Entity.EntityState.Added;
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = data.Posts.Find(id);
            if (item != null)
            {
                data.Posts.Remove(item);
                data.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = data.Posts.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                data.Entry(item).State = System.Data.Entity.EntityState.Modified;
                data.SaveChanges();
                return Json(new { success = true, isAcive = item.IsActive });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = data.Posts.Find(Convert.ToInt32(item));
                        data.Posts.Remove(obj);
                        data.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

    }
}