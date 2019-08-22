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

        public static void Crawl(string keyword)
        {
            var query = keyword.Replace(" ", "+");
            var uri = _address + query;

            // create the web request and get response
            WebRequest request = WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();

            if (dataStream != null)
            {
                StreamReader reader = new StreamReader(dataStream);

                string responseFromServer = reader.ReadToEnd();

                var indexesOfLinks = responseFromServer.AllIndexesOf("<div class=\"ZINbbc xpd O9g5cc uUPGi\"><div class=\"kCrYT\">");
                var indexesOfInfoTrack = responseFromServer.AllIndexesOf("<a href=\"/url?q=https://www.infotrack");

                var occurrencesOfInfoTrack = indexesOfInfoTrack.Count;
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

                Console.WriteLine(occurrencesOfInfoTrack);
                infoTrackFoundAt.ForEach(i => Console.WriteLine(i.ToString()));

                reader.Close();
            }

            dataStream?.Close();
            response.Close();
        }

        public static Stream GetDataStream(string keyword)
        {
            var query = keyword.Replace(" ", "+");
            var uri = _address + query;

            // create the web request and get response
            var request = WebRequest.Create(uri);
            var response = (HttpWebResponse)request.GetResponse();

            return response.GetResponseStream();
        }

        public static string ReadDataStream(Stream dataStream)
        {
            var reader = new StreamReader(dataStream);
            return reader.ReadToEnd();
        }

        public static List<int> IndexesOfLinks(string responseFromServer)
        {
            return responseFromServer.AllIndexesOf("<div class=\"ZINbbc xpd O9g5cc uUPGi\"><div class=\"kCrYT\">");
        }

        public static List<int> IndexesOfInfoTrack(string responseFromServer)
        {
            return responseFromServer.AllIndexesOf("<a href=\"/url?q=https://www.infotrack");
        }

        public static int OccurrencesOfInfoTrack(string responseFromServer)
        {
            return responseFromServer.AllIndexesOf("<a href=\"/url?q=https://www.infotrack").Count;
        }

        public static List<int> LocationsOfInfoTrack(string responseFromServer)
        {
            var indexesOfLinks = IndexesOfLinks(responseFromServer);
            var indexesOfInfoTrack = IndexesOfInfoTrack(responseFromServer);

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