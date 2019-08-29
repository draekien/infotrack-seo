using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoTrackSeo.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTrackSeoTest.Helpers.Extensions
{
    [TestClass()]
    public class FindIndexTests
    {
        [TestMethod()]
        public void FindAllIndexesOfSubstringTest_Equals()
        {
            // Arrange
            const string parentStr = "this is a test string test string string";
            const string testStr1 = "this";
            var expected1 = new List<int> {0};

            const string testStr2 = "test";
            var expected2 = new List<int> {10, 22 };

            const string testStr3 = "string";
            var expected3 = new List<int> {15, 27, 34};

            // Act
            var result1 = parentStr.FindAllIndexesOfSubstring(testStr1);
            var result2 = parentStr.FindAllIndexesOfSubstring(testStr2);
            var result3 = parentStr.FindAllIndexesOfSubstring(testStr3);

            // Assert
            CollectionAssert.AreEqual(expected1, result1);
            CollectionAssert.AreEqual(expected2, result2);
            CollectionAssert.AreEqual(expected3, result3);
        }

        [TestMethod()]
        public void FindAllIndexesOfSubstringTest_InvalidParentStr_Throws()
        {
            // Arrange
            const string parentStr = null;
            const string testStr = "Some String";
            
            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => parentStr.FindAllIndexesOfSubstring(testStr));
        }

        [TestMethod()]
        public void FindAllIndexesOfSubstringTest_InvalidChildStr_Throws()
        {
            // Arrange
            const string parentStr = "Some String";
            const string testStr = null;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => parentStr.FindAllIndexesOfSubstring(testStr));
        }
    }
}