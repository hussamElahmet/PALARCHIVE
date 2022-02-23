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
    public class AboutController : MyController
    {
        private MarbleEntityFrameWork context;
        public AboutController()
        {
            context = new MarbleEntityFrameWork();
        }
        // GET: About
        public ActionResult About()
        {
            AboutPageModel aboutModel = new AboutPageModel();
            aboutModel.aboutInfo = context.About.FirstOrDefault();
            return View(aboutModel);
        }
    }
}