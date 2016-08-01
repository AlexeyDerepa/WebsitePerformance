using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebsitePerformance.Models;
using System.Data.Entity;

namespace WebsitePerformance.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        WebSiteContext db = new WebSiteContext(); 
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SearchSiteMape(FoundWebSiteAddress hostName)
        {
            int? number = ProcessingOfRequest.ProcessingSurface(hostName.UrlAddress);

            TempData["Message"] = (number == null) ? "Address does not found" : "Please go to: History requests";
            
            return View("Index");
        }
        [HttpPost]
        public async Task<ActionResult> SearchDeepSiteMape(FoundWebSiteAddress hostName)
        {
            int? number = ProcessingOfRequest.ProcessingDeep(hostName.UrlAddress);

            TempData["Message"] = (number == null) ? "Address does not found" : "Please go to: History requests";
            
            return View("Index");
        }
        public async Task<ActionResult> SearchSitePage(int id)
        {

            IEnumerable<FoundSitePage> allInfo;
            try
            {
                allInfo = await (from page in db.FoundSitePages
                                 join xml in db.FoundSiteMapes on page.FoundSiteMapeId equals xml.Id
                                 join addr in db.Addresses on xml.FoundWebSiteAddressId equals addr.Id
                                 where addr.Id == id
                                 orderby page.TimeMin
                                 select page).OrderByDescending(x => x.TimeMin).ToListAsync();
            }
            catch (Exception ex)
            {
                allInfo = new List<FoundSitePage>();
            }

            return View(allInfo);
        }
        public async Task<ActionResult> SpecificPartPages(int id)
        {
            IEnumerable<FoundSitePage> allInfo;
            try
            {
                allInfo = await (from page in db.FoundSitePages
                                 join xml in db.FoundSiteMapes on page.FoundSiteMapeId equals xml.Id
                                 where page.FoundSiteMapeId == id
                                 orderby page.TimeMin
                                 select page).OrderByDescending(x => x.TimeMin).ToListAsync();
            }
            catch (Exception ex)
            {
                allInfo = new List<FoundSitePage>();
            }

            return View("SearchSitePage", allInfo);
        }

        public async Task<ActionResult> HistoryRequests()
        {
            IEnumerable<FoundWebSiteAddress> allInfo;
            try
            {
                allInfo = await db.Addresses.OrderByDescending(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                allInfo = new List<FoundWebSiteAddress>();
            }
            return View(allInfo);
        }

        public async Task<ActionResult> HistoryXml(int id)
        {
            IEnumerable<FoundSiteMape> allInfo;
            try
            {
                allInfo = await (from xml in db.FoundSiteMapes
                                 join addr in db.Addresses on xml.FoundWebSiteAddressId equals addr.Id
                                 where xml.FoundWebSiteAddressId == id
                                 select xml).OrderByDescending(x =>x.TimeMin).ToListAsync();
            }
            catch (Exception ex)
            {
                allInfo = new List<FoundSiteMape>();
            }

            return View(allInfo);
        }

       //[HttpPost]
       // public ActionResult HistoryRequests(string hostName)
       // {
       //     return View(ProcessingOfRequest.Ping(hostName));
       // }

        public ActionResult PingSite()
        {
            ViewBag.List = null;
            return View();
        }

        [HttpPost]
        public ActionResult PingSite(FoundWebSiteAddress hostName)
        {
            ViewBag.List = ProcessingOfRequest.Ping(hostName.UrlAddress);
            return View();
        }

    }
}
