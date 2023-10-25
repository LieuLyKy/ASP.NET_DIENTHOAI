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
    public class ProductsController : Controller
    {
        private ApplicationDbContext data = new ApplicationDbContext();
        // GET: Admin/Products
        public ActionResult Index(int? page, string Searchtext)
        {
            IEnumerable<Product> items = data.Products.OrderByDescending(x => x.Id);
            var pageSize = 8;
            if (page == null)
            {
                page = 1;
            }
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
            ViewBag.ProductCategory = new SelectList(data.ProductCategories.ToList(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        if (i + 1 == rDefault[0])
                        {
                            model.Image = Images[i];
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = true
                            });
                        }
                        else
                        {
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false
                            });
                        }
                    }
                }
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                if (string.IsNullOrEmpty(model.SeoTitle))
                {
                    model.SeoTitle = model.Title;
                }
                if (string.IsNullOrEmpty(model.Alias))
                    model.Alias = WEBBANDIENTHOAI.Models.Common.Filter.FilterChar(model.Title);
                data.Products.Add(model);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(data.ProductCategories.ToList(), "Id", "Title");
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = new SelectList(data.ProductCategories.ToList(), "Id", "Title");
            var item = data.Products.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = WEBBANDIENTHOAI.Models.Common.Filter.FilterChar(model.Title);
                data.Products.Attach(model);
                data.Entry(model).State = System.Data.Entity.EntityState.Modified;
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = data.Products.Find(id);
            if (item != null)
            {
                data.Products.Remove(item);
                data.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        // Sự lí hiện thị tổng
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = data.Products.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                data.Entry(item).State = System.Data.Entity.EntityState.Modified;
                data.SaveChanges();
                return Json(new { success = true, isAcive = item.IsActive });
            }

            return Json(new { success = false });
        }
        // sử lí hiện tran Home
        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = data.Products.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                data.Entry(item).State = System.Data.Entity.EntityState.Modified;
                data.SaveChanges();
                return Json(new { success = true, IsHome = item.IsHome });
            }

            return Json(new { success = false });
        }

        // sử lí phần hiện sale
        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = data.Products.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                data.Entry(item).State = System.Data.Entity.EntityState.Modified;
                data.SaveChanges();
                return Json(new { success = true, IsSale = item.IsSale });
            }

            return Json(new { success = false });
        }
    }
}
    
