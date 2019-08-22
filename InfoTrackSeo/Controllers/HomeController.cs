using System;
using System.Collections.Generic;
using System.IO;
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
        /// <summary>
        /// home page, submits the default search
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // the default search
            var uri = "https://www.infotrack.com.au";
            var keyword = "online title search";

            return View(new HomeViewModel
            {
                Uri = uri,
                Keyword = keyword,
                LinkCount = 0,
                LinkLocations = new List<int>()
            });
        }

        /// <summary>
        /// Form submission
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public JsonResult Crawl(string uri, string keyword)
        {
            var dataStream = Helpers.Crawler.GetDataStream(keyword);
            if (dataStream == null)
                return new JsonResult
                {
                    Data = new
                    {
                        error = "Could not retrieve data stream."
                    }
                };
            var responseFromServer = Helpers.Crawler.ReadDataStream(dataStream);
            var uriCount = Helpers.Crawler.OccurrencesOfUri(uri, responseFromServer);
            var uriLocations = Helpers.Crawler.LocationsOfUri(uri, responseFromServer);

            dataStream.Close();

            return new JsonResult
            {
                Data = new
                {
                    success = new
                    {
                        uri = uri,
                        keyword = keyword,
                        uriCount = uriCount,
                        uriLocations = uriLocations,
                    }
                }
            };
        }
    }
}