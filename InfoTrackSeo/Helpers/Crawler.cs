using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace InfoTrackSeo.Helpers
{
    public static class Crawler
    {
        // google search address that returns 100 results
        private const string Address = "https://www.google.com.au/search?gl=au&hl=en&pws=0&num=100&q=";

        /// <summary>
        /// Get the response from the search result as string
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static async Task<string> GetResultsAsString(string keyword)
        {
            string data;
            try
            {
                using (var client = new HttpClient())
                {
                    using (var res = await client.GetAsync(Address + HttpUtility.UrlEncode(keyword)))
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
                return "";
            }

            return data;
        }

        /// <summary>
        /// get indexes of all links on the page
        /// </summary>
        /// <param name="responseFromServer"></param>
        /// <returns></returns>
        private static List<int> IndexesOfLinks(string responseFromServer) =>
            responseFromServer.AllIndexesOf("<div class=\"ZINbbc xpd O9g5cc uUPGi\"><div class=\"kCrYT\">");

        /// <summary>
        /// get indexes of where the uri occurs on the results
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="responseFromServer"></param>
        /// <returns></returns>
        private static IEnumerable<int> IndexesOfUri(string uri, string responseFromServer) =>
            responseFromServer.AllIndexesOf($"<div class=\"kCrYT\"><a href=\"/url?q={uri}");

        /// <summary>
        /// count how many times the uri appears on the page as a search result
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="responseFromServer"></param>
        /// <returns></returns>
        public static int OccurrencesOfUri(string uri, string responseFromServer) =>
            responseFromServer.AllIndexesOf($"<div class=\"kCrYT\"><a href=\"/url?q={uri}").Count;

        /// <summary>
        /// find out which search result the uri is a part of, excludes adverts
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="responseFromServer"></param>
        /// <returns></returns>
        public static List<int> LocationsOfUri(string uri, string responseFromServer)
        {
            var indexesOfLinks = IndexesOfLinks(responseFromServer);
            var indexesOfInfoTrack = IndexesOfUri(uri, responseFromServer);

            var infoTrackFoundAt = new List<int>();
            var startIndex = 0;

            foreach (var t in indexesOfInfoTrack)
            {
                for (var j = startIndex; j < indexesOfLinks.Count; j++)
                {
                    if (t >= indexesOfLinks[j]) continue;
                    infoTrackFoundAt.Add(j);
                    startIndex = j;
                    break;
                }
            }

            return infoTrackFoundAt;
        }

        /// <summary>
        /// Get all indexes of a specified substring
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static List<int> AllIndexesOf(this string str, string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("The string to find may not be empty", nameof(value));
            var indexes = new List<int>();
            for (var index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index, StringComparison.Ordinal);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }
    }
}