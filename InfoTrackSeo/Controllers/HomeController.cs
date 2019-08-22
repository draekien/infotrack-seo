using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InfoTrackSeo.Models;

namespace InfoTrackSeo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dataStream = Helpers.Crawler.GetDataStream("online title search");
            var responseFromServer = Helpers.Crawler.ReadDataStream(dataStream);
            var infoTrackCount = Helpers.Crawler.OccurrencesOfInfoTrack(responseFromServer);
            var infoTrackPageLocation = Helpers.Crawler.LocationsOfInfoTrack(responseFromServer);

            return View(new HomeViewModel
            {
                InfoTrackCount = infoTrackCount,
                InfoTrackLinkLocations = infoTrackPageLocation
            });
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