using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WEBBANDIENTHOAI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            // trỏ đường dẫn đến trang liên hệ
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
             name: "ContactLienHe",
             url: "lien-he",
             defaults: new { controller = "ContactLienHe", action = "Index", alias = UrlParameter.Optional },
             namespaces: new[] { "WEBBANDIENTHOAI.Controllers" }
            );
                routes.MapRoute(
                 name: "ThuTucThanhToan",
                 url: "thanh-toan",
                 defaults: new { controller = "ShoppingCart", action = "ThuTucThanhToan", alias = UrlParameter.Optional },
                 namespaces: new[] { "WEBBANDIENTHOAI.Controllers" }
             );

            // Dẫn 1 đường dẫn / đến trang giỏ hàng
            routes.MapRoute(
                 name: "ShoppingCart",
                 url: "gio-hang",
                 defaults: new { controller = "ShoppingCart", action = "Index", alias = UrlParameter.Optional },
                 namespaces: new[] { "WEBBANDIENTHOAI.Controllers" }
             );

            //   Dẫn sổ danh sách danh mục sản phẩm xuống trỏ đường đẫn đến trang theo danh mục sản phẩm
            routes.MapRoute(
                  name: "CategoryProduct",
                  url: "danh-muc-san-pham/{alias}-{id}",
                  defaults: new { controller = "Products", action = "ProductCategory", id = UrlParameter.Optional },
                  namespaces: new[] { "WEBBANDIENTHOAI.Controllers" }
              );

            // Dẫn đến Trang chi tiết Sản Phẩm
            routes.MapRoute(
              name: "detailProduct",
              url: "chi-tiet/{alias}-p{id}",
              defaults: new { controller = "Products", action = "Detail", alias = UrlParameter.Optional },
              namespaces: new[] { "WEBBANDIENTHOAI.Controllers" }
          );

            // trỏ đường dẫn đến trang danh sách sản phẩm
            routes.MapRoute(
               name: "Products",
               url: "san-pham",
               defaults: new { controller = "Products", action = "Index", alias = UrlParameter.Optional },
               namespaces: new[] { "WEBBANDIENTHOAI.Controllers" }
           );

            // trỏ đường dẫn đến trang Home
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] { "WEBBANDIENTHOAI.Controllers" }
            );
        }
    }
}
