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
    public class ProductController : MyController
    {

        private MarbleEntityFrameWork context;

        public ProductController(){
            context = new MarbleEntityFrameWork();
        }
        // GET: Product
        public ActionResult Index(int? categoryId)
        {
            ProductsPageModel result = new ProductsPageModel();
            result.products=context.Products.ToList();
            result.productCategories = (List<Category>)context.Categories.Where(x=>x.Type==1).ToList();
            foreach(var item in result.products)
            {
                if(item.Cover_image_id !=null)
                {
                    item.Cover_Url = (from p in context.Products
                                      join g in context.ProductGalleries
                                      on p.Cover_image_id equals g.Id
                                      where g.Id==item.Cover_image_id
                                      select g.Url).SingleOrDefault().ToString();
                }
               
            }

            if(categoryId !=null)
            {
                result.products = context.Products.Where(x=>x.Category_id== categoryId).ToList();
            }

            return View(result);
        }

        public ActionResult Details(int id)
        {
            ProductsPageModel result = new ProductsPageModel();
            result.products = context.Products.Where(x => x.Id == id).ToList();
            result.productGallery = context.ProductGalleries.Where(x => x.Product_id == id).ToList();
            return View(result);
        }
    }
}