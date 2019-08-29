using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfoTrackSeo.Models
{
    public class HomeViewModel
    {
        [Display(Name = "URI")]
        [Required]
        public string Uri { get; set; }

        [Display(Name = "Keywords")]
        [Required]
        public string Keywords { get; set; }

        /// <summary>
        /// Number of times InfoTrack's website appears in the search results
        /// </summary>
        [Display(Name = "Count of URI Occurrences")]
        public int LinkCount { get; set; }

        /// <summary>
        /// Where the url is found on the page.
        /// </summary>
        [Display(Name = "Location of URI Occurrences")]
        public List<int> LinkLocations { get; set; }
    }
}