using LuzFaltex.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LuzFaltex.Utilities.Tests
{
    public class BiDictionaryTests
    {
        

        [Fact]
        public void BiDictionary_Add()
        {
            BiDictionary<int, string> biDic = new BiDictionary<int, string>();

            biDic.Add(1, "one");

            Assert.Single(biDic);
        }

        [Fact]
        public void TestDic_Remove()
        {
            BiDictionary<int, string> biDic = new BiDictionary<int, string>()
            {
                {1, "one"},
                {2, "two"},
            };

            biDic.RemoveByKey(1);

            Assert.Single(biDic);
        }

        [Fact]
        public void TestDic_Equality()
        {
            IEquatable<BiDictionary<int, string>> dic1 = new BiDictionary<int, string>()
            {
                {1, "one"}
            };

            BiDictionary<int, string> dic2 = new BiDictionary<int, string>()
            {
                { 1, "one" }
            };

            Assert.True(dic1.Equals(dic2));
        }
    }
}
