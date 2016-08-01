using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebsitePerformance.Models
{
    public class WebSiteContext : DbContext
    {
        public DbSet<FoundWebSiteAddress> Addresses { get; set; }
        public DbSet<FoundSiteMape> FoundSiteMapes { get; set; }
        public DbSet<FoundSitePage> FoundSitePages { get; set; }

        public WebSiteContext() : base("WebSiteDb") { }
        static WebSiteContext()
        {
            Database.SetInitializer<WebSiteContext>(new ContextInitializer());
        }
    }
}