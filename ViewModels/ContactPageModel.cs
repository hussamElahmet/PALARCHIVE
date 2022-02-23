using marble.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace marble.ViewModels
{
    public class ContactPageModel
    {
        public Contact ContactInfo { set; get; }
    }

    public class pmContactForm
    {
        public string name { get; set; }
        public string email { get; set; }
        public string message { get; set; }
    }
}