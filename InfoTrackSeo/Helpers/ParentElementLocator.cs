using System;
using System.Collections.Generic;
using InfoTrackSeo.Helpers.Extensions;

namespace InfoTrackSeo.Helpers
{
    public class ParentElementLocator
    {
        protected string ResponseFromServer { get; }
        private string ParentString { get; }

        protected readonly IFindIndex FindIndex;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="responseFromServer"></param>
        /// <param name="parentString"></param>
        /// <param name="findIndex"></param>
        protected ParentElementLocator(string responseFromServer, string parentString, IFindIndex findIndex)
        {
            ResponseFromServer = responseFromServer ?? throw new ArgumentNullException(nameof(responseFromServer));
            ParentString = parentString ?? throw new ArgumentNullException(nameof(parentString));
            FindIndex = findIndex;
        }

        /// <summary>
        /// Get a list of indexes for where the parent string occurs
        /// </summary>
        /// <returns></returns>
        protected List<int> IndexesOfLinks() => FindIndex.FindAllIndexesOfSubstring(ResponseFromServer, ParentString);

    }

}