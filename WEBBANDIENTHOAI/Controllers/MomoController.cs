using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WEBBANDIENTHOAI.Models;
using WebBanHangOnline.Common;

namespace WEBBANDIENTHOAI.Controllers
{

    public class MomoController : Controller
    {
        private ApplicationDbContext data = new ApplicationDbContext();
        // GET: Momo
        public ActionResult Index(DatHangViewModel req)
        {

            var qrcode = new CreateQRCode();
            qrcode.CreateQRCodeMomo("0822507636", "aaa", "a2123", "25000000");
            byte[] imgBytes = qrcode.turnImageToByteArray(qrcode.picMomo);
            string imgString = Convert.ToBase64String(imgBytes);
            ViewBag.Bitmap = String.Format("img src=data:image/Bmp;base64,{0}", imgString);

            return View();
        }
    }
}