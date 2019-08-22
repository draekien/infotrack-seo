using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using InfoTrackSeo.Models;

namespace InfoTrackSeo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // the default search
            var uri = "https://www.infotrack.com.au";
            var keyword = "online title search";
            var dataStream = Helpers.Crawler.GetDataStream(keyword);
            var responseFromServer = Helpers.Crawler.ReadDataStream(dataStream);
            var infoTrackCount = Helpers.Crawler.OccurrencesOfUri(uri, responseFromServer);
            var infoTrackPageLocation = Helpers.Crawler.LocationsOfUri(uri, responseFromServer);

            dataStream?.Close();

            return View(new HomeViewModel
            {
                Uri = uri,
                Keyword = keyword,
                LinkCount = infoTrackCount,
                LinkLocations = infoTrackPageLocation
            });
        }

        public async Task<JsonResult> Crawl(string uri, string keywords)
        {
            var dataStream = Helpers.Crawler.GetDataStream("online title search");
            var responseFromServer = Helpers.Crawler.ReadDataStream(dataStream);
            var uriCount = Helpers.Crawler.OccurrencesOfUri(uri, responseFromServer);
            var uriLocations = Helpers.Crawler.LocationsOfUri(uri, responseFromServer);

            dataStream?.Close();

            return new JsonResult
            {
                Data = new
                {
                    uri = uri,
                    keywords = keywords,
                    uriCount = uriCount,
                    uriLocations = uriLocations
                }
            };
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}