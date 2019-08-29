using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace InfoTrackSeo.Helpers
{
    public class CrawlerV2
    {

        private string Address { get; }
        private string SearchTerm { get; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="address"></param>
        /// <param name="searchTerm"></param>
        public CrawlerV2(string address, string searchTerm)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
            SearchTerm = searchTerm ?? throw new ArgumentNullException(nameof(searchTerm));
        }

        /// <summary>
        /// Get response from the search as string
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetResultsAsString()
        {
            string data;
            try
            {
                using (var client = new HttpClient())
                {
                    using (var res = await client.GetAsync(Address + HttpUtility.UrlEncode(SearchTerm)))
                    {
                        using (var content = res.Content)
                        {
                            data = await content.ReadAsStringAsync();
                        }
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return data;
        }
    }
}