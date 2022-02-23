using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace marble.Controllers
{
    public class SetLangController : Controller
    {

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string lang = null;
            HttpCookie langCookie = Request.Cookies["culture"];
            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                var userLanguage = Request.UserLanguages;
                var userLang = userLanguage != null ? userLanguage[0] : "";
                if (userLang != "")
                {
                    lang = userLang;
                }
                else
                {
                    lang = LanguageMang.GetDefaultLanguage();
                }
            }
            new LanguageMang().SetLanguage(lang);
            return base.BeginExecuteCore(callback, state);
        }

        //// GET: SetLang
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult SetLanguage(string name)
        //{
        //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(name);
        //    Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

        //    //HttpContext.Current.Session["culture"] = name;

        //    return RedirectToAction("Index", "Home");
        //}
        //public ActionResult ChangeLanguage()
        //{
        //    if (Thread.CurrentThread.CurrentCulture.Name == "en-US")
        //    {
        //        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr-FR");
        //        ViewBag.CultBtn = "En";
        //    }
        //    else
        //    {
        //        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        //        ViewBag.CultBtn = "Fr";
        //    }

        //    Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

        //    HttpContext.Current.Session["culture"] = Thread.CurrentThread.CurrentCulture.Name;

        //    return RedirectToAction("Index", "Home");
        //}

    }
}