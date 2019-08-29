using System;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace InfoTrackSeo.Helpers
{
    public class CrawlerV2
    {

        private string Address { get; }
        private string Keywords { get; }
        private string Response { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="address"></param>
        /// <param name="keywords"></param>
        public CrawlerV2(string address, string keywords)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));
            if (string.IsNullOrEmpty(keywords))
                throw new ArgumentNullException(nameof(keywords));
            Address = address;
            Keywords = keywords;
        }

        /// <summary>
        /// Do the search and store it in Response
        /// </summary>
        /// <returns></returns>
        public async Task Search()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var res = await client.GetAsync(Address + HttpUtility.UrlEncode(Keywords)))
                    {
                        using (var content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();
                            if (string.IsNullOrEmpty(data))
                                throw new ArgumentNullException(nameof(data));
                            Response = data;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }

        /// <summary>
        /// Get the response
        /// </summary>
        /// <returns></returns>
        public string GetResponse() => Response;
    }
}