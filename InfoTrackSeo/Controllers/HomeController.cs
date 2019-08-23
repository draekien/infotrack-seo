﻿using System.Threading.Tasks;
using System.Web.Mvc;
using InfoTrackSeo.Helpers;
using InfoTrackSeo.Models;

namespace InfoTrackSeo.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// home page pre-fills the form with default search terms
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            // the default search terms
            const string uri = "https://www.infotrack.com.au";
            const string keyword = "online title search";

            var data = await Crawler.GetResultsAsString(keyword);

            // if there is no data then something went wrong (most likely google blocked us)
            if (data == "")
                return new JsonResult { Data = new { error = "Could not retrieve search engine results." } };

            var uriCount = Crawler.OccurrencesOfUri(uri.ToLower(), data);
            var uriLocations = Crawler.LocationsOfUri(uri.ToLower(), data);

            return View(new HomeViewModel
            {
                Uri = uri,
                Keyword = keyword,
                LinkCount = uriCount,
                LinkLocations = uriLocations
            });
        }

        /// <summary>
        /// Form submission
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<JsonResult> Crawl(string uri, string keyword)
        {
            var data = await Crawler.GetResultsAsString(keyword);

            // if there is no data then something went wrong (most likely google blocked us)
            if (data == "")
                return new JsonResult { Data = new { error = "Could not retrieve search engine results." } };

            var uriCount = Crawler.OccurrencesOfUri(uri.ToLower(), data);
            var uriLocations = Crawler.LocationsOfUri(uri.ToLower(), data);

            return new JsonResult
            {
                Data = new
                {
                    success = new
                    {
                        uri,
                        keyword,
                        uriCount,
                        uriLocations
                    }
                }
            };
        }
    }
}