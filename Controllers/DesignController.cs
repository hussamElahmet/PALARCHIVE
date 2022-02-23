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
    public class DesignController : MyController
    {

        private MarbleEntityFrameWork context;

        public DesignController(){
            context = new MarbleEntityFrameWork();
        }
        // GET: Design
        public ActionResult Index(int? categoryId)
        {
            DesignsPageModel result = new DesignsPageModel();
            result.Designs=context.Designs.ToList();
            result.designCategories = (List<Category>)context.Categories.Where(x=>x.Type==3).ToList();
            foreach(var item in result.Designs)
            {
                if(item.Cover_image_id !=null)
                {
                    item.Cover_Url = (from p in context.Designs
                                      join g in context.DesignGalleries
                                      on p.Cover_image_id equals g.Id
                                      where g.Id==item.Cover_image_id
                                      select g.Url).SingleOrDefault().ToString();
                }
               
            }

            if(categoryId !=null)
            {
                result.Designs = context.Designs.Where(x=>x.Category_id== categoryId).ToList();
            }

            return View(result);
        }

        public ActionResult Details(int id)
        {
            DesignsPageModel result = new DesignsPageModel();
            result.Designs = context.Designs.Where(x => x.Id == id).ToList();
            result.designGallery = context.DesignGalleries.Where(x => x.Design_id == id).ToList();
            return View(result);
        }
    }
}