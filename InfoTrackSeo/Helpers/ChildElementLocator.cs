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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="responseFromServer"></param>
        /// <param name="parentString"></param>
        /// <param name="childString"></param>
        /// <param name="uri"></param>
        public ChildElementLocator(string responseFromServer, string parentString, string childString, string uri) : base(responseFromServer, parentString)
        {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
            ChildString = childString ?? throw new ArgumentNullException(nameof(childString));
        }

        /// <summary>
        /// Get a list of indexes for where the child string occurs
        /// </summary>
        /// <returns></returns>
        private IEnumerable<int> IndexesOfUri() => ResponseFromServer.AllIndexesOf(ChildString+Uri);

        /// <summary>
        /// Count how many times the uri appears on the page
        /// </summary>
        /// <returns></returns>
        public int OccurrencesOfUri() => IndexesOfUri().Count();

        /// <summary>
        /// Find out where the uri appears on the page
        /// </summary>
        /// <returns></returns>
        public List<int> LocationsOfUri()
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

            return infoTrackFoundAt;
        }
    }
}