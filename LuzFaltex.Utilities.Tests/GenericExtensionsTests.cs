using LuzFaltex.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LuzFaltex.Utilities.Tests
{
    public class GenericExtensionsTests
    {
        public string JohnDoe
            => "John Doe";

        

        [Fact]
        public void Deconstruct_One()
        {
            string firstName = JohnDoe.Split(' ')[0];

            Assert.Equal("John", $"{firstName}");
        }

        [Fact]
        public void Deconstruct_Two()
        {
            (string firstName, string lastName) = JohnDoe.Split(' ');

            Assert.Equal("John Doe", $"{firstName} {lastName}");
        }
    }
}
