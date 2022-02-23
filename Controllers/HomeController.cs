using marble.Context;
using marble.Models;
using marble.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace marble.Controllers
{
    public class HomeController : MyController
    {
        private MarbleEntityFrameWork db;

        public HomeController()
        {
            db = new MarbleEntityFrameWork();
        }

        public ActionResult Index()
        {
            HomePageModel home = new HomePageModel();
            home.SlidersList = (List<Slider>)db.Sliders.ToList();
            home.productCategories = (List<Category>)db.Categories.Where(x => x.Type == 1).ToList();
            home.projectCategories = (List<Category>)db.Categories.Where(x => x.Type == 2).ToList();
            home.designCategories = (List<Category>)db.Categories.Where(x => x.Type == 3).ToList();
            home.products = (List<Product>)db.Products.ToList();
            home.ContactInfo = db.Contact.FirstOrDefault();
            foreach (var item in home.products)
            {
                if (item.Cover_image_id != null)
                {
                    item.Cover_Url = (from p in db.Products
                                      join g in db.ProductGalleries
                                      on p.Cover_image_id equals g.Id
                                      where g.Id == item.Cover_image_id
                                      select g.Url).SingleOrDefault().ToString();
                }

            }
        return View(home);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ChangeLanguage(string lang)
        {
            new LanguageMang().SetLanguage(lang);
            return RedirectToAction("Index", "Home");

        }
    }
}