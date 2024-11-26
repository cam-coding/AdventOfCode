using AdventLibrary.Helpers;
using System.Collections.Generic;
using Xunit;

namespace AdventLibraryUnitTests.Helpers
{
    public class HashSetHelperUnitTests
    {
        [Fact]
        public void HashObjectSorted_Dictionary_SameDictSameHash()
        {
            var dict = new Dictionary<string, int>()
            {
                { "a", 1 },
                { "b", 2 },
            };

            Assert.Equal(HashSetHelper.HashObjectSorted(dict), HashSetHelper.HashObjectSorted(dict));
        }

        [Fact]
        public void HashObjectSorted_Dictionary_DifferentDictSameContentsSameHash()
        {
            var dict = new Dictionary<string, int>()
            {
                { "a", 1 },
                { "b", 2 },
            };
            var dict1 = new Dictionary<string, int>()
            {
                { "a", 1 },
                { "b", 2 },
            };

            Assert.Equal(HashSetHelper.HashObjectSorted(dict), HashSetHelper.HashObjectSorted(dict1));
        }

        [Fact]
        public void HashObjectSorted_Dictionary_DifferentDictSameContentsDifferentOrderSameHash()
        {
            var dict = new Dictionary<string, int>()
            {
                { "a", 1 },
                { "b", 2 },
            };
            var dict1 = new Dictionary<string, int>()
            {
                { "b", 2 },
                { "a", 1 },
            };

            Assert.Equal(HashSetHelper.HashObjectSorted(dict), HashSetHelper.HashObjectSorted(dict1));
        }

        [Fact]
        public void HashObjectSorted_List_SameListSameHash()
        {
            var list = new List<string>() { "a", "b" };

            Assert.Equal(HashSetHelper.HashObjectSorted(list), HashSetHelper.HashObjectSorted(list));
        }

        [Fact]
        public void HashObjectSorted_List_DifferentListSameContentsSameHash()
        {
            var list = new List<string>() { "a", "b" };
            var list2 = new List<string>() { "a", "b" };

            Assert.Equal(HashSetHelper.HashObjectSorted(list), HashSetHelper.HashObjectSorted(list2));
        }

        [Fact]
        public void HashObjectSorted_List_DifferentListSameContentsDifferentOrderSameHash()
        {
            var list = new List<string>() { "a", "b" };
            var list2 = new List<string>() { "b", "a" };

            Assert.Equal(HashSetHelper.HashObjectSorted(list), HashSetHelper.HashObjectSorted(list2));
        }
    }
}
