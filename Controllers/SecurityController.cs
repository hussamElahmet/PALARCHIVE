
using marble.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace marble.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        private MarbleEntityFrameWork db;

        public SecurityController()
        {
            db = new MarbleEntityFrameWork();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string Pass)
        {
            var users = db.User.FirstOrDefault(x => x.UserName == UserName && x.Pass == Pass);
            if (users != null)
            {
                FormsAuthentication.SetAuthCookie(users.UserName, false); //Cookie oluşturmak için
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.Hata = "Yanlış Kullanıcı";
                return View();
            }

        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();//Güvenli çıkış işlemi için form kimlik doğrulamasını kapatıyoruz.
            return RedirectToAction("Login");
        }

    }
}


