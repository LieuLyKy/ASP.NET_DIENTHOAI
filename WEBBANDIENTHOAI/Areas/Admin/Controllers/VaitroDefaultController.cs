using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANDIENTHOAI.Models;

namespace WEBBANDIENTHOAI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VaitroDefaultController : Controller
    {
        private ApplicationDbContext data = new ApplicationDbContext();
        // GET: Admin/VaitroDefault
        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var items = data.Roles.ToList();
            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole model)
        {
            // nếu  model hợp lệ
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(data));
                roleManager.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        
    }
}