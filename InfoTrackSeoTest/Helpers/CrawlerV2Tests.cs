using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoTrackSeo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace InfoTrackSeoTest.Helpers
{
    [TestClass()]
    public class CrawlerV2Tests
    {
        [TestMethod()]
        public async Task GetResponse_ValidInputs_NotNull()
        {
            // Arrange
            const string address = "https://www.google.com.au/search?gl=au&hl=en&pws=0&num=100&q=";
            const string keywords = "online title search";
            CrawlerV2 crawlerV2 = new CrawlerV2(address, keywords);

            // Act
            await crawlerV2.Search();

            // Assert
            Assert.IsNotNull(crawlerV2.GetResponse());
        }

        [TestMethod()]
        public void GetResponse_InvalidAddress_Throw()
        {
            // Arrange
            const string nullAddress = null;
            var emptyAddress = string.Empty;
            const string keywords = "online title search";

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CrawlerV2(nullAddress, keywords));
            Assert.ThrowsException<ArgumentNullException>(() => new CrawlerV2(emptyAddress, keywords));
        }

        [TestMethod()]
        public void GetResponse_InvalidKeywords_throw()
        {
            // Arrange
            const string address = "https://www.google.com.au/search?gl=au&hl=en&pws=0&num=100&q=";
            const string nullKeywords = null;
            var emptyKeywords = string.Empty;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CrawlerV2(address, nullKeywords));
            Assert.ThrowsException<ArgumentNullException>(() => new CrawlerV2(address, emptyKeywords));
        }

        [TestMethod()]
        public async Task GetResponse_InvalidAddressAndKeywords_throw()
        {
            // Arrange
            const string address = "not an address";
            const string keywords = "some keyworkd";
            var crawlerV2 = new CrawlerV2(address, keywords);
            
            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => crawlerV2.Search());
        }
    }
}