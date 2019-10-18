using System;
using System.Collections.Generic;
using System.Linq;
using InfoTrackSeo.Helpers.Extensions;

namespace InfoTrackSeo.Helpers
{
    public class ChildElementLocator : ParentElementLocator
    {
        private string Uri { get; }

        private string ChildString { get; }

        private List<int> UriLocations { get; set; }

        private int UriCount { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="responseFromServer"></param>
        /// <param name="parentString"></param>
        /// <param name="childString"></param>
        /// <param name="uri"></param>
        /// <param name="findIndex"></param>
        public ChildElementLocator(string responseFromServer, string parentString, string childString, string uri, IFindIndex findIndex) : base(responseFromServer, parentString, findIndex)
        {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
            ChildString = childString ?? throw new ArgumentNullException(nameof(childString));
        }

        /// <summary>
        /// Get a list of indexes for where the child string occurs
        /// </summary>
        /// <returns></returns>
        private IEnumerable<int> IndexesOfUri() => FindIndex.FindAllIndexesOfSubstring(ResponseFromServer, ChildString + Uri);

        /// <summary>
        /// count how many times the Uri is found
        /// </summary>
        public void CountUriOccurrences() => UriCount = IndexesOfUri().Count();

        /// <summary>
        /// return uri count
        /// </summary>
        /// <returns></returns>
        public int GetUriCount() => UriCount;

        /// <summary>
        /// Find locations of uri
        /// </summary>
        public void FindLocationsOfUri()
        {
            var indexesOfLinks = IndexesOfLinks();
            var indexesOfInfoTrack = IndexesOfUri();

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

            UriLocations = infoTrackFoundAt;
        }

        /// <summary>
        /// get uri locations
        /// </summary>
        /// <returns></returns>
        public List<int> GetUriLocations() => UriLocations;

    }
}