using System;
using System.Collections.Generic;

namespace InfoTrackSeo.Helpers.Extensions
{
    public class FindIndex : IFindIndex
    {
        /// <summary>
        /// Find all instances of subStr in str and return a list
        /// </summary>
        /// <param name="str"></param>
        /// <param name="subStr"></param>
        /// <returns></returns>
        public List<int> FindAllIndexesOfSubstring(string str, string subStr)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException(nameof(str));

            if (string.IsNullOrEmpty(subStr))
                throw new ArgumentNullException(nameof(subStr));

            var indexes = new List<int>();
            for (var index = 0; ; index += subStr.Length)
            {
                index = str.IndexOf(subStr, index, StringComparison.Ordinal);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }
    }
}