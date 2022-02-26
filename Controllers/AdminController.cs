using marble.Context;
using marble.Models;
using marble.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace marble.Controllers
{
    public class AdminController : MyController
    {
        // GET: Admin
        private MarbleEntityFrameWork context;

        public AdminController()
        {
            context = new MarbleEntityFrameWork();
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        #region PRODUCT 
        // GET: Products
       
        [Authorize(Roles = "A")]
        public ActionResult Details()
        {
            ProductsPageModel model = new ProductsPageModel();
            model.products = context.Products.ToList();
            model.productCategories = context.Categories.Where(x => x.Type == 1).ToList();
            model.productGallery = context.ProductGalleries.ToList();
            return View(model);
        }
       
        [Authorize(Roles = "A")]
        public ActionResult Add()
        {
            ProductsPageModel result = new ProductsPageModel();
            result.productCategories = context.Categories.Where(x => x.Type == 1).ToList();
            return View(result);
        }
       
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult Add(paramAdd param)
        {
            Product product = new Product();
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +Request.ApplicationPath.TrimEnd('/') + "/";
                //Checking file is available to save.  
                if (param.files.Count() > 0 && !string.IsNullOrEmpty(param.productName) && !string.IsNullOrEmpty(param.productDesc))
                {
                    try
                    {
                        product.Name = param.productName;
                        product.Name_En = param.productNameEn;
                        product.Details = param.productDesc;
                        product.Details_En = param.productDescEn;
                        product.Category_id = param.categoryNo;
                        context.Products.Add(product);
                        context.SaveChanges();
                        int newProductId = product.Id;
                        foreach (HttpPostedFileBase file in param.files)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/Content/Upload/") + InputFileName);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);
                            //assigning file uploaded status to ViewBag for showing message to user.  
                            ViewBag.UploadStatus = param.files.Count().ToString() + " files uploaded successfully.";
                            Product_Gallery gallery = new Product_Gallery();
                            gallery.Product_id = newProductId;
                            gallery.Url = file.FileName;
                            context.ProductGalleries.Add(gallery);
                            context.SaveChanges();
                            if (file == param.files[0])
                            {
                                product.Cover_image_id = gallery.Id;
                                context.Entry(product).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }
                 
                    }catch(Exception ex)
                    {
                    }

                }
                else
                {
                    ViewBag.UploadStatus = "You have to upload file please";
                }

            }
            ProductsPageModel result2 = new ProductsPageModel();
            result2.productCategories = context.Categories.Where(x => x.Type == 1).ToList();
            return View(result2);
        }
       
        [Authorize(Roles = "A")]
        public ActionResult DeleteProduct(int id)
        {
            Product product = new Product();
            product = context.Products.Where(x => x.Id == id).FirstOrDefault();
            product.Cover_image_id = null;
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();

            List<Product_Gallery> gallery = context.ProductGalleries.Where(x => x.Product_id == id).ToList();
            context.ProductGalleries.RemoveRange(gallery);
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Details");
        }
        #endregion



        #region Projects 
        // GET: Projects
        public ActionResult ProjectDetails()
        {
            ProjectsPageModel model = new ProjectsPageModel();
            model.projects = context.Projects.ToList();
            model.projectCategories = context.Categories.Where(x => x.Type == 2).ToList();
            model.projectGallery = context.ProjectGalleries.ToList();
            return View(model);
        }
        public ActionResult ProjectAdd()
        {
            ProjectsPageModel result = new ProjectsPageModel();
            result.projectCategories = context.Categories.Where(x => x.Type == 2).ToList();
            return View(result);
        }
        [HttpPost]
        public ActionResult ProjectAdd(paramAdd param)
        {
            Project project = new Project();
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                //Checking file is available to save.  
                if (param.files.Count() > 0 && !string.IsNullOrEmpty(param.productName) && !string.IsNullOrEmpty(param.productDesc))
                {
                    try
                    {
                        project.Name = param.productName;
                        project.Details = param.productDesc;
                        project.Category_id = param.categoryNo;
                        context.Projects.Add(project);
                        context.SaveChanges();
                        int newProductId = project.Id;
                        foreach (HttpPostedFileBase file in param.files)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/Content/Upload/") + InputFileName);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);
                            //assigning file uploaded status to ViewBag for showing message to user.  
                            ViewBag.UploadStatus = param.files.Count().ToString() + " files uploaded successfully.";
                            Project_Gallery gallery = new Project_Gallery();
                            gallery.Project_id = newProductId;
                            gallery.Url = file.FileName;
                            context.ProjectGalleries.Add(gallery);
                            context.SaveChanges();
                            if (file == param.files[0])
                            {
                                project.Cover_image_id = gallery.Id;
                                context.Entry(project).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                    }

                }
                else
                {
                    ViewBag.UploadStatus = "You have to upload file please";
                }

            }
            ProjectsPageModel result2 = new ProjectsPageModel();
            result2.projectCategories = context.Categories.Where(x => x.Type == 2).ToList();
            return View(result2);
        }

        public ActionResult DeleteProject(int id)
        {
            Project project = new Project();
            project = context.Projects.Where(x => x.Id == id).FirstOrDefault();
            project.Cover_image_id = null;
            context.Entry(project).State = EntityState.Modified;
            context.SaveChanges();

            List<Project_Gallery> gallery = context.ProjectGalleries.Where(x => x.Project_id == id).ToList();
            context.ProjectGalleries.RemoveRange(gallery);
            context.Projects.Remove(project);
            context.SaveChanges();
            return RedirectToAction("ProjectDetails");
        }
        #endregion


        #region Designs 
        // GET: Designs
        [Authorize(Roles = "A")]
        public ActionResult DesignDetails()
        {
            DesignsPageModel model = new DesignsPageModel();
            model.Designs = context.Designs.ToList();
            model.designCategories = context.Categories.Where(x => x.Type == 3).ToList();
            model.designGallery = context.DesignGalleries.ToList();
            return View(model);
        }
        [Authorize(Roles = "A")]
        public ActionResult DesignAdd()
        {
            DesignsPageModel result = new DesignsPageModel();
            result.designCategories = context.Categories.Where(x => x.Type == 3).ToList();
            return View(result);
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult DesignAdd(paramAdd param)
        {
            Design Design = new Design();
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                //Checking file is available to save.  
                if (param.files.Count() > 0 && !string.IsNullOrEmpty(param.productName) && !string.IsNullOrEmpty(param.productDesc))
                {
                    try
                    {
                        Design.Name = param.productName;
                        Design.Details = param.productDesc;
                        Design.Category_id = param.categoryNo;
                        context.Designs.Add(Design);
                        context.SaveChanges();
                        int newProductId = Design.Id;
                        foreach (HttpPostedFileBase file in param.files)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/Content/Upload/") + InputFileName);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);
                            //assigning file uploaded status to ViewBag for showing message to user.  
                            ViewBag.UploadStatus = param.files.Count().ToString() + " files uploaded successfully.";
                            Design_Gallery gallery = new Design_Gallery();
                            gallery.Design_id = newProductId;
                            gallery.Url = file.FileName;
                            context.DesignGalleries.Add(gallery);
                            context.SaveChanges();
                            if (file == param.files[0])
                            {
                                Design.Cover_image_id = gallery.Id;
                                context.Entry(Design).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                    }

                }
                else
                {
                    ViewBag.UploadStatus = "You have to upload file please";
                }

            }
            DesignsPageModel result2 = new DesignsPageModel();
            result2.designCategories = context.Categories.Where(x => x.Type == 3).ToList();
            return View(result2);
        }
        [Authorize(Roles = "A")]
        public ActionResult DeleteDesign(int id)
        {
            Design Design = new Design();
            Design = context.Designs.Where(x => x.Id == id).FirstOrDefault();
            Design.Cover_image_id = null;
            context.Entry(Design).State = EntityState.Modified;
            context.SaveChanges();

            List<Design_Gallery> gallery = context.DesignGalleries.Where(x => x.Design_id == id).ToList();
            context.DesignGalleries.RemoveRange(gallery);
            context.Designs.Remove(Design);
            context.SaveChanges();
            return RedirectToAction("DesignDetails");
        }
        #endregion


        #region About 
        [Authorize(Roles = "A")]
        public ActionResult About()
        {
            About about = new About();
            about = context.About.First();
            return View(about);
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult About(paramAbout param)
        {
            About about = new About();
            if (!string.IsNullOrEmpty(param.Details) && param.files[0]==null)
            {
                 about = context.About.First();
                about.Details = param.Details;
                about.Details_En = param.DetailsEn;
                context.Entry(about).State = EntityState.Modified;
                context.SaveChanges();
            }
            if (!string.IsNullOrEmpty(param.Details) && param.files[0]!=null)
            {
                foreach (HttpPostedFileBase file in param.files)
                {
                    try
                    {
                        about = context.About.First();

                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Content/Upload/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = param.files.Count().ToString() + " files uploaded successfully.";
                        about.Details = param.Details;
                        about.Url = file.FileName;
                        context.Entry(about).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
             about = context.About.First();
            return View(about);
        }

        #endregion


        #region Sliders
        [Authorize(Roles = "A")]
        public ActionResult SliderAdd()
        {
            Slider result = new Slider();
            return View(result);
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult SliderAdd(paramAdd param)
        {

            if (param.files[0] != null)
            {
                foreach (HttpPostedFileBase file in param.files)
                {
                    try
                    {

                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Content/Upload/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        Slider result = new Slider();
                        result.Url = file.FileName;
                        result.Order = 1;
                        context.Sliders.Add(result);
                        context.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            return RedirectToAction("SliderAdd");
        }

        [Authorize(Roles = "A")]
        public ActionResult SliderDetails()
        {
            HomePageModel result = new HomePageModel();
            result.SlidersList = context.Sliders.ToList();

            return View(result);
        }


        [Authorize(Roles = "A")]
        public ActionResult DeleteSlider(int id)
        {
            Slider slider = new Slider();
            slider = context.Sliders.Where(x => x.Id == id).FirstOrDefault();
            context.Sliders.Remove(slider);
            context.SaveChanges();

            return RedirectToAction("SliderDetails");
        }
        #endregion

        [Authorize(Roles = "A")]
        #region product-category
        public ActionResult productCategories()
        {
            HomePageModel result = new HomePageModel();
            result.productCategories = context.Categories.Where(x => x.Type == 1).ToList();
            return View(result);
        }

        [Authorize(Roles = "A")]
        public ActionResult DeleteProductCategory(int id)
        {
            List<Product> products = context.Products.Where(x => x.Category_id == id).ToList();
            foreach(var item in products)
            {
                item.Category_id = null;
                List<Product_Gallery> gallery = context.ProductGalleries.Where(x => x.Product_id == item.Id).ToList();
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
                context.ProductGalleries.RemoveRange(gallery);
                context.SaveChanges();
            }
            context.Products.RemoveRange(products);
            context.SaveChanges();
            Category category = new Category();
            category = context.Categories.Where(x => x.Category_id == id).FirstOrDefault();
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("productCategories");
        }


        [Authorize(Roles = "A")]
        public ActionResult CategoryAdd()
        {
            CategoryProductPageModel result = new CategoryProductPageModel();
            return View(result);
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult CategoryAdd(paramCategoryAdd param)
        {
            Category category = new Category();
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                //Checking file is available to save.  
                if (param.files.Count() > 0 && !string.IsNullOrEmpty(param.CategoryName) &&  param.Order.HasValue )
                {
                    try
                    {
                        category.Name = param.CategoryName;
                        category.Name_En = param.CategoryNameEn;
                        category.Order = param.Order;
                        category.Type = 1;

                        foreach (HttpPostedFileBase file in param.files)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/Content/Upload/") + InputFileName);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);
                            //assigning file uploaded status to ViewBag for showing message to user.  
                            ViewBag.UploadStatus = param.files.Count().ToString() + " files uploaded successfully.";
                            category.Url = file.FileName;
                            context.Categories.Add(category);
                            context.SaveChanges();
                          
                        }

                    }
                    catch (Exception ex)
                    {
                    }

                }
                else
                {
                    ViewBag.UploadStatus = "You have to upload file please";
                }

            }
            CategoryProductPageModel result2 = new CategoryProductPageModel();
            return View(result2);
        }

        #endregion
    }
}