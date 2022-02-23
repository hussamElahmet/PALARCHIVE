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
    public class ProjectController : MyController
    {

        private MarbleEntityFrameWork context;

        public ProjectController(){
            context = new MarbleEntityFrameWork();
        }
        // GET: Project
        public ActionResult Index(int? categoryId)
        {
            ProjectsPageModel result = new ProjectsPageModel();
            result.projects=context.Projects.ToList();
            result.projectCategories = (List<Category>)context.Categories.Where(x=>x.Type==2).ToList();
            foreach(var item in result.projects)
            {
                if(item.Cover_image_id !=null)
                {
                    item.Cover_Url = (from p in context.Projects
                                      join g in context.ProjectGalleries
                                      on p.Cover_image_id equals g.Id
                                      where g.Id==item.Cover_image_id
                                      select g.Url).SingleOrDefault().ToString();
                }
               
            }

            if(categoryId !=null)
            {
                result.projects = context.Projects.Where(x=>x.Category_id== categoryId).ToList();
            }

            return View(result);
        }

        public ActionResult Details(int id)
        {
            ProjectsPageModel result = new ProjectsPageModel();
            result.projects = context.Projects.Where(x => x.Id == id).ToList();
            result.projectGallery = context.ProjectGalleries.Where(x => x.Project_id == id).ToList();
            return View(result);
        }
    }
}