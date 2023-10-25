using PagedList;
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
    public class NewsController : Controller
    {
        private ApplicationDbContext data = new ApplicationDbContext();
        // GET: Admin/News
        public ActionResult Index(string Searchtext, int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<News> items = data.News.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(x => x.Alias.Contains(Searchtext) || x.Title.Contains(Searchtext));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.CategoryId = 2004;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WEBBANDIENTHOAI.Models.Common.Filter.FilterChar(model.Title);
                data.News.Attach(model);
                data.Entry(model).State = System.Data.Entity.EntityState.Added;
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var item = data.News.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = WEBBANDIENTHOAI.Models.Common.Filter.FilterChar(model.Title);
                data.News.Attach(model);
                data.Entry(model).State = System.Data.Entity.EntityState.Modified;
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = data.News.Find(id);
            if (item != null)
            {
                data.News.Remove(item);
                data.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = data.News.Find(id);
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
                        var obj = data.News.Find(Convert.ToInt32(item));
                        data.News.Remove(obj);
                        data.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}