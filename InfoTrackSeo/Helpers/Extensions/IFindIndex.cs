using System.Collections.Generic;

namespace InfoTrackSeo.Helpers.Extensions
{
    public interface IFindIndex
    {
        List<int> FindAllIndexesOfSubstring(string str, string subStr);
    }
}