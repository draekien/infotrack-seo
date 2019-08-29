using System;
using System.Collections.Generic;
using InfoTrackSeo.Helpers.Extensions;

namespace InfoTrackSeo.Helpers
{
    public class ParentElementLocator
    {
        protected string ResponseFromServer { get; }
        private string ParentString { get; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="responseFromServer"></param>
        /// <param name="parentString"></param>
        protected ParentElementLocator(string responseFromServer, string parentString)
        {
            ResponseFromServer = responseFromServer ?? throw new ArgumentNullException(nameof(responseFromServer));
            ParentString = parentString ?? throw new ArgumentNullException(nameof(parentString));
        }

        /// <summary>
        /// Get a list of indexes for where the parent string occurs
        /// </summary>
        /// <returns></returns>
        protected List<int> IndexesOfLinks() => ResponseFromServer.FindAllIndexesOfSubstring(ParentString);

    }

}