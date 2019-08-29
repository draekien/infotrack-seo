using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoTrackSeo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTrackSeoTest.Helpers
{
    [TestClass()]
    public class ChildElementLocatorTests
    {
        [TestMethod()]
        public async Task ChildElementLocatorTest()
        {
            // Arrange
            const string address = "https://www.google.com.au/search?gl=au&hl=en&pws=0&num=100&q=";
            const string keywords = "online title search";
            var crawlerV2 = new CrawlerV2(address, keywords);

            await crawlerV2.Search();

            var response = crawlerV2.GetResponse();

            var parentString = "<div class=\"ZINbbc xpd O9g5cc uUPGi\"><div class=\"kCrYT\">";
            var childString = "<div class=\"kCrYT\"><a href=\"/url?q=";
            var uri = "https://www.infotrack.com.au";

            var childElementLocator = new ChildElementLocator(response, parentString, childString, uri);

            // Act
            childElementLocator.FindLocationsOfUri();


            // Assert
            CollectionAssert.AllItemsAreUnique(childElementLocator.GetUriLocations());
            CollectionAssert.AllItemsAreNotNull(childElementLocator.GetUriLocations());
            CollectionAssert.AllItemsAreInstancesOfType(childElementLocator.GetUriLocations(), typeof(int));
        }

        [TestMethod()]
        public async Task CountUriOccurrencesTest_NotNull()
        {
            // Arrange
            const string address = "https://www.google.com.au/search?gl=au&hl=en&pws=0&num=100&q=";
            const string keywords = "online title search";
            var crawlerV2 = new CrawlerV2(address, keywords);

            await crawlerV2.Search();

            var response = crawlerV2.GetResponse();

            var parentString = "<div class=\"ZINbbc xpd O9g5cc uUPGi\"><div class=\"kCrYT\">";
            var childString = "<div class=\"kCrYT\"><a href=\"/url?q=";
            var uri = "https://www.infotrack.com.au";

            var childElementLocator = new ChildElementLocator(response, parentString, childString, uri);

            // Act
            childElementLocator.CountUriOccurrences();

            // Assert
            Assert.IsNotNull(childElementLocator.GetUriCount());
        }
    }
}