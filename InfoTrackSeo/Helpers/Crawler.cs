using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace InfoTrackSeo.Helpers
{
    public static class Crawler
    {
        private static string _address = "https://www.google.com.au/search?gl=au&hl=en&pws=0&num=100&q=";

        /// <summary>
        /// Get data stream from google
        /// </summary>
        /// <param name="keyword">search terms to use</param>
        /// <returns></returns>
        public static Stream GetDataStream(string keyword)
        {
            var query = keyword.Replace(" ", "+");
            var uri = _address + query;

            // create the web request and get response
            var request = WebRequest.Create(uri);
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                return null;
            }

            return response.GetResponseStream();
        }

        /// <summary>
        /// Read the data stream to a string
        /// </summary>
        /// <param name="dataStream"></param>
        /// <returns>string</returns>
        public static string ReadDataStream(Stream dataStream)
        {
            var reader = new StreamReader(dataStream);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// get indexes of all links on the page
        /// </summary>
        /// <param name="responseFromServer"></param>
        /// <returns></returns>
        public static List<int> IndexesOfLinks(string responseFromServer)
        {
            return responseFromServer.AllIndexesOf("<div class=\"ZINbbc xpd O9g5cc uUPGi\"><div class=\"kCrYT\">");
        }

        /// <summary>
        /// get indexes of where the uri occurs on the results
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="responseFromServer"></param>
        /// <returns></returns>
        public static List<int> IndexesOfUri(string uri, string responseFromServer)
        {
            return responseFromServer.AllIndexesOf($"<a href=\"/url?q={uri}");
        }

        /// <summary>
        /// count how many times the uri appears on the page as a search result
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="responseFromServer"></param>
        /// <returns></returns>
        public static int OccurrencesOfUri(string uri, string responseFromServer)
        {
            return responseFromServer.AllIndexesOf($"<a href=\"/url?q={uri}").Count;
        }

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
                    infoTrackFoundAt.Add(j + 1);
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
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("The string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }
    }
}