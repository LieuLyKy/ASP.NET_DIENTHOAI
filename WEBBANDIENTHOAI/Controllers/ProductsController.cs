using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANDIENTHOAI.Models;
using WEBBANDIENTHOAI.Models.MFC;

namespace WEBBANDIENTHOAI.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext data = new ApplicationDbContext();
        // GET: Products

        // phần Product sản phẩm trang show all sản phẩm
        public ActionResult Index(int? page, string Searchtext, string sortOrder)
        {
            ViewBag.SortByName = String.IsNullOrEmpty(sortOrder) ?"ten_desc" : "";
            ViewBag.SortByPrice = (sortOrder == "dongia" ? "dongia_desc" : "dongia"); 
          
            IEnumerable<Product> items = data.Products.OrderByDescending(x => x.Id);
           
            switch(sortOrder)
            {
                case "ten_desc":
                    items = data.Products.OrderByDescending(x => x.Title);
                    break;
                case "dongia_desc":
                    items = data.Products.OrderByDescending(x => x.Price);
                    break;
                case "dongia":
                    items = data.Products.OrderBy(x => x.Price);
                    break;
                default: // mặc định sắp xếp theo tên sản phẩm
                    items = data.Products.OrderBy(x => x.Title);
                    break;

            }
          
            var pageSize = 5;
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
            //var items = data.Products.ToList();

            //return View(items);

        }

        // chi tiết sản phẩm sản phẩm 
        public ActionResult Detail(string alias, int id)
        {
            var item = data.Products.Find(id);
            // đếm số lượt truy cập vô sản phẩm đó
            if (item != null)
            {
                data.Products.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                data.Entry(item).Property(x => x.ViewCount).IsModified = true;
                data.SaveChanges();
            }

            return View(item);
        }

        // Trang sản phẩm nhưng theo danh mục ấn vào danh mục điện thoại thì sẻ sử lí show trang điện thoại
        public ActionResult ProductCategory(string alias, int id, string Searchtext, string sortOrder, int? page)
        {

            ViewBag.SortByName = String.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            ViewBag.SortByPrice = (sortOrder == "dongia" ? "dongia_desc" : "dongia");

            IEnumerable<Product> items = data.Products.OrderByDescending(x => x.Id);

            switch (sortOrder)
            {
                case "ten_desc":
                    items = data.Products.OrderByDescending(x => x.Title);
                    break;
                case "dongia_desc":
                    items = data.Products.OrderByDescending(x => x.Price);
                    break;
                case "dongia":
                    items = data.Products.OrderBy(x => x.Price);
                    break;
                default: // mặc định sắp xếp theo tên sản phẩm
                    items = data.Products.OrderBy(x => x.Title);
                    break;

            }

            //var items = data.Products.ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = data.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }
            var CateId = 5;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, CateId);
            ViewBag.CateId = id;
            ViewBag.Page = page;
            return View(items);
        }


        // sử lí từng phần sản phẩm theo tên sản phẩm ngoài trang chủ, Điện Thoai, Máy Tính, Đồng Hồ
        public ActionResult Partial_ItemsByCateId()
        {
            var items = data.Products.Where(x => x.IsHome && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }

        //
        public ActionResult ProductSales()
        {
            var items = data.Products.Where(x => x.IsSale && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }

       
    }
}