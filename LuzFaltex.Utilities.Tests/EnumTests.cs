using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace LuzFaltex.Utilities.Tests
{
    public class EnumTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        public void Enum_ParseFromValue_Known(long value)
        {
            var result = EnumTools.ParseFromValue<ExampleEnum>(value);

            Assert.NotEqual(default, result);
            Assert.True(Enum.IsDefined(typeof(ExampleEnum), result));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(24)]
        public void Enum_ParseFromValue_Unknown(long value)
        {
            var result = EnumTools.ParseFromValue<ExampleEnum>(value);

            Assert.NotEqual(default, result);
            Assert.False(Enum.IsDefined(typeof(ExampleEnum), result));
        }

    }
}
