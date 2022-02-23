using marble.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace marble.Context
{
    public class MarbleEntityFrameWork : DbContext
    {
        public string ProviderConnectionString { get; set; }

        public MarbleEntityFrameWork() : base(nameOrConnectionString: "MarbleEntities")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
        public DbSet<Slider> Sliders { set; get; }
        public DbSet<Contact_Form> ContactForm { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<About> About { set; get; }
        public DbSet<Contact> Contact { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<Project> Projects { set; get; }
        public DbSet<Design> Designs { set; get; }
        public DbSet<Product_Gallery> ProductGalleries { set; get; }
        public DbSet<Project_Gallery> ProjectGalleries { set; get; }
        public DbSet<Design_Gallery> DesignGalleries { set; get; }
    
    }
}
