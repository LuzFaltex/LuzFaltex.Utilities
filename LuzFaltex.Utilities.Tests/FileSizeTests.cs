using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace LuzFaltex.Utilities.Tests
{
    public class FileSizeTests
    {
        [Theory]
        [InlineData("100b")]
        [InlineData("1000kb")]
        [InlineData("20GB")]
        [InlineData("200 gigabytes")]
        public void FileSize_ParseString(string bytes)
        {
            Assert.True(FileSize.TryParse(bytes, out _));
        }

        [Theory]
        [InlineData(FileSize.Kilobyte, 1)]
        [InlineData(FileSize.Kilobyte * 5, 5)]
        public void FileSize_Kilobytes(long bytes, double expectedKb)
        {
            var size = FileSize.FromBytes(bytes);
            double actualKb = size.ToKilobytes();

            Assert.Equal(expectedKb, actualKb);
        }

        [Theory]
        [InlineData(FileSize.Megabyte, 1)]
        [InlineData(FileSize.Megabyte * 5, 5)]
        public void FileSize_Megabytes(long bytes, double expectedMb)
        {
            var size = FileSize.FromBytes(bytes);
            double actualMb = size.ToMegabytes();

            Assert.Equal(expectedMb, actualMb);
        }

        [Theory]
        [InlineData(FileSize.Gigabyte, 1)]
        [InlineData(FileSize.Gigabyte * 5, 5)]
        public void FileSize_Gigabytes(long bytes, double expectedGb)
        {
            var size = FileSize.FromBytes(bytes);
            double actualGb = size.ToGigabytes();

            Assert.Equal(expectedGb, actualGb);
        }

        [Theory]
        [InlineData(FileSize.Terabyte, 1)]
        [InlineData(FileSize.Terabyte * 5, 5)]
        public void FileSize_Terabytes(long bytes, double expectedTb)
        {
            var size = FileSize.FromBytes(bytes);
            double actualTb = size.ToTerabytes();

            Assert.Equal(expectedTb, actualTb);
        }

        [Theory]
        [InlineData(FileSize.Petabyte, 1)]
        [InlineData(FileSize.Petabyte * 5, 5)]
        public void FileSize_Petabytes(long bytes, double expectedPb)
        {
            var size = FileSize.FromBytes(bytes);
            double actualPb = size.ToPetabytes();

            Assert.Equal(actualPb, expectedPb);
        }

        [Theory]
        [InlineData(FileSize.Exabyte, 1)]
        [InlineData(FileSize.Exabyte * 5, 5)]
        public void FileSize_Exabytes(long bytes, double expectedEb)
        {
            var size = FileSize.FromBytes(bytes);
            double actualPb = size.ToExabytes();

            Assert.Equal(actualPb, expectedEb);
        }

        [Theory]
        [InlineData(FileSize.Megabyte, "1 MB")]
        [InlineData(2 * FileSize.Megabyte, "2 MB")]
        [InlineData(226497536, "216.0048828125 MB")]
        [InlineData(342360064, "326.5 MB")]
        [InlineData(138244259840, "128.75 GB")]
        public void FileSize_ToString(long bytes, string expected)
        {
            var size = FileSize.FromBytes(bytes);
            string actual = size.ToString();

            Assert.Equal(expected, actual, StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
