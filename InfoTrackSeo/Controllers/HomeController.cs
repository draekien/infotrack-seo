using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using InfoTrackSeo.Helpers;
using InfoTrackSeo.Models;

namespace InfoTrackSeo.Controllers
{
    public class HomeController : Controller
    {
        private const string Address = "https://www.google.com.au/search?gl=au&hl=en&pws=0&num=100&q=";
        /// <summary>
        /// home page pre-fills the form with default search terms
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // the default search terms
            const string uri = "https://www.infotrack.com.au";
            const string keyword = "online title search";

            return View(new HomeViewModel
            {
                Uri = uri,
                Keywords = keyword
            });
        }

        /// <summary>
        /// Form submission
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public async Task<JsonResult> Crawl(string uri, string keywords)
        {
            // create a new crawler and do a crawl using the provided search terms
            var crawlerV2 = new CrawlerV2(Address, keywords);

            await crawlerV2.Search();

            var data = crawlerV2.GetResponse();

            if (data == string.Empty)
                return new JsonResult() { Data = new {error = "Could not retrieve search engine results."}};

            // parent element
            const string htmlOfDivContainingLink = "<div class=\"ZINbbc xpd O9g5cc uUPGi\"><div class=\"kCrYT\">";
            // child element
            const string htmlOfAnchorElement = "<div class=\"kCrYT\"><a href=\"/url?q=";

            // create a new uri locator so we can figure out where all the links are
            var uriLocator = new ChildElementLocator(data, htmlOfDivContainingLink, htmlOfAnchorElement, uri);

            uriLocator.FindLocationsOfUri();
            uriLocator.CountUriOccurrences();

            var uriLocations = uriLocator.GetUriLocations();
            var uriCount = uriLocator.GetUriCount();

            // display "no results" if there are no links to infotrack in the search results
            if (uriCount == 0)
                keywords = $"{keywords} (no results)";

            return new JsonResult
            {
                Data = new
                {
                    success = new
                    {
                        uri,
                        keywords,
                        uriCount,
                        uriLocations
                    }
                }
            };
        }
    }
}