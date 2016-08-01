using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebsitePerformance.Models
{
    public class ContextInitializer : DropCreateDatabaseAlways<WebSiteContext>
    {
        protected override void Seed(WebSiteContext db)
        {

        }
    }
}