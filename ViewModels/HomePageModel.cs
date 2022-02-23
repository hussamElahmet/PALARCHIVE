using marble.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace marble.ViewModels
{
    public class HomePageModel
    { 

        public Contact ContactInfo { set; get; }
        public List<Slider> SlidersList { set; get; }
        public List<Category> productCategories { set; get; }
        public List<Category> projectCategories { set; get; }
        public List<Category> designCategories { set; get; }
        public List<Product> products { set; get; }

    }
}