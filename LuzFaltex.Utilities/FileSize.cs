using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using LuzFaltex.Utilities.Extensions;

namespace LuzFaltex.Utilities
{
    /// <summary>
    /// Represents the space a file occupies on a file system or in a stream
    /// </summary>
    public class FileSize : IEquatable<FileSize>
    {
        /// <summary>
        /// The size of the file in bytes
        /// </summary>
        public long Bytes { get; }

        public const long Kilobyte = 1024L;
        public const long Megabyte = 1048576L;
        public const long Gigabyte = 1073741824L;
        public const long Terabyte = 1099511627776L;
        public const long Petabyte = 1125899906842624L;
        public const long Exabyte = 1152921504606846976L;

        /// <summary>
        ///  Using the default cluster size of 4 KB, the maximum NTFS volume size is 16 TB minus 4 KB
        /// </summary>
        public const long MaxImplementedBytes = (Terabyte * 16) - (4 * Kilobyte);

        /// <summary>
        /// The NTFS maximum theoretical limit on the size of individual files is 16 EiB (16 × 1024^6 or 2^64 bytes) minus 1 KB
        /// </summary>
        public static readonly BigInteger MaxTheoreticalBytes = new BigInteger(Exabyte) * 16 - Kilobyte;

        private static readonly KeyValuePair<long, string>[] ByteThresholds =
        {
            new KeyValuePair<long, string>(1, " Byte"),
            new KeyValuePair<long, string>(2, " Bytes"),
            new KeyValuePair<long, string>(Kilobyte, " KB"),
            new KeyValuePair<long, string>(Megabyte, " MB"), // Note: 1024 ^ 2 = 1026 (xor operator)
            new KeyValuePair<long, string>(Gigabyte, " GB"),
            new KeyValuePair<long, string>(Terabyte, " TB"),
            new KeyValuePair<long, string>(Petabyte, " PB"),
            new KeyValuePair<long, string>(Exabyte, " EB"),
        };
        
        #region Constructors

        /// <summary>
        /// Initializes a FileSize instance from the provided size.
        /// </summary>
        /// <param name="bytes">The file size, in bytes</param>
        private FileSize(long bytes)
        {
            Ensure(bytes);
            Bytes = bytes;
        }

        /// <summary>
        /// Ensures that the specified number of bytes is within the NTFS file limit
        /// </summary>
        /// <param name="bytes">Number of bytes to check</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the provided byte count is greater than <see cref="MaxTheoreticalBytes"/> (16 Exabytes - 1 Kilobyte)</exception>
        public static void Ensure(long bytes)
        {
            if (bytes > MaxTheoreticalBytes)
                throw new ArgumentOutOfRangeException(nameof(bytes), bytes, $"The provided byte count must be less than or equal to {MaxTheoreticalBytes} Bytes (16 Exabytes - 1 Kilobyte).");
        }

        /// <summary>
        /// Initializes a new FileSize instance from the provided size
        /// </summary>
        /// <param name="bytes">The file size, in bytes</param>
        public static FileSize FromBytes(long bytes)
            => new FileSize(bytes);
            

        /// <summary>
        /// Initializes a new FileSize instance from the provided size
        /// </summary>
        /// <param name="kilobytes">The file size, in bytes</param>
        public static FileSize FromKilobytes(int kilobytes)
            => new FileSize(kilobytes << 10);

        /// <summary>
        /// Initializes a new FileSize instance from the provided size
        /// </summary>
        /// <param name="megabytes">The file size, in megabytes</param>
        public static FileSize FromMegabytes(int megabytes)
            => new FileSize(megabytes << 20);

        /// <summary>
        /// Initializes a new FileSize instance from the provided size
        /// </summary>
        /// <param name="gigabytes">The file size, in gigabytes</param>
        public static FileSize FromGigabytes(int gigabytes)
            => new FileSize(gigabytes << 30);

        /// <summary>
        /// Initializes a new FileSize instance from the provided size
        /// </summary>
        /// <param name="terabytes">The file size, in terabytes. Must be a maximum of 134217728 terabytes due to file system restrictions.</param>
        public static FileSize FromTerabytes(int terabytes)
            => new FileSize(terabytes << 40);

        /// <summary>
        /// Initializes a new FileSize instance from the provided size
        /// </summary>
        /// <param name="petabytes">The file size, in petabytes. Must be a maximum of 16384 petabytes due to file system restrictions.</param>
        public static FileSize FromPetabytes(int petabytes)
            => new FileSize(petabytes << 50);

        /// <summary>
        /// Initializes a new FileSize instance from the provided size
        /// </summary>
        /// <param name="exabytes">The file size, in exabytes. Must be a maximum of 16 exabytes due to file system restrictions.</param>
        /// <remarks>Max NTFS file system size on individual files is 16 EiB (16 × 10246 or 264 bytes) minus 1 KB</remarks>
        /// <exception cref="ArgumentOutOfRangeException">The provided file size must be a maximum of 16 exabytes.</exception>
        public static FileSize FromExabytes(int exabytes)
            => new FileSize(exabytes <<60);

        #endregion

        #region Converters

        /// <summary>
        /// Returns a byte representation of this file size.
        /// </summary>
        /// <returns>The file size, as bytes.</returns>
        public long AsBytes()
            => Bytes;

        /// <summary>
        /// Returns a kilobyte representation of this file size.
        /// </summary>
        /// <returns>The file size, as kilobytes.</returns>
        public double ToKilobytes()
            => Bytes >> 10;

        /// <summary>
        /// Returns a megabyte representation of this file size.
        /// </summary>
        /// <returns>The file size, as megabytes.</returns>
        public double ToMegabytes()
            => Bytes >> 20;

        /// <summary>
        /// Returns a gigabyte representation of this file size.
        /// </summary>
        /// <returns>The file size, as gigabytes.</returns>
        public double ToGigabytes()
            => Bytes >> 30;

        /// <summary>
        /// Returns a terabyte representation of this file size.
        /// </summary>
        /// <returns>The file size, as terabytes.</returns>
        public double ToTerabytes()
            => Bytes >> 40;

        /// <summary>
        /// Returns a petabyte representation of this file size.
        /// </summary>
        /// <returns>The file size, as petabytes.</returns>
        public double ToPetabytes()
            => Bytes >> 50;

        /// <summary>
        /// Returns an exabyte byte representation of this file size.
        /// </summary>
        /// <returns>The file size, as exabytes.</returns>
        public double ToExabytes()
            => Bytes >> 60;

        /// <summary>
        /// Returns the size, minimized to the largest file size
        /// </summary>
        /// <returns>The file size, as a string</returns>
        public override string ToString()
        {
            if (Bytes == 0) return "0 Bytes"; // zero is plural.

            for (int t = ByteThresholds.Length - 1; t > 0; t--)
                if (Bytes >= ByteThresholds[t].Key)
                    return ((double) Bytes / ByteThresholds[t].Key) + ByteThresholds[t].Value;
            return $"{Bytes} Bytes";
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Determines if two FileSizes are equal based on their underlying byte sizes
        /// </summary>
        /// <param name="other"></param>
        /// <returns>True if equals; otherwise false.</returns>
        /// <exception cref="NullReferenceException">Thrown if <param name="other"></param> is null.</exception>
        public bool Equals(FileSize other)
            => Bytes.Equals(other.Bytes);

        public override int GetHashCode()
            => Bytes.GetHashCode();

        #endregion

        #region StaticMethods

        /// <summary>
        /// Reads in a string representation of a size in bytes and converts it into a FileSize.
        /// </summary>
        /// <param name="size">The size to parse. Must have the size (kb, mb, etc.) after an integer showing count.</param>
        /// <returns>A <see cref="FileSize"/> representing the specified FileSize</returns>
        public static FileSize Parse(string size)
        {
            if (string.IsNullOrWhiteSpace(size))
                throw  new ArgumentNullException(nameof(size));

            return TryParse(size, out FileSize result) 
                ? result 
                : throw new FormatException($"Could not parse {size} into a FileSize.");
        }

        /// <summary>
        /// Reads in a string representation of a size in bytes and converts it into a FileSize
        /// </summary>
        /// <param name="size">The size to parse</param>
        /// <param name="result">The resultant FileSize</param>
        /// <returns>True if successful; otherwise false</returns>
        public static bool TryParse(string size, out FileSize result)
        {
            if (string.IsNullOrWhiteSpace(size))
            {
                result = null;
                return false;
            }

            StringBuilder intBuilder = new StringBuilder();
            for (int i = 0; i < size.Length; i++)
            {
                if (char.IsDigit(size[i]))
                    intBuilder.Append(size[i]);
                else
                    break;
            }

            if (!long.TryParse(intBuilder.ToString(), out long count))
            {
                result = null;
                return false;
            }

            string marker = size.Replace(intBuilder.ToString(), "").ToLowerInvariant().Trim();

            switch (marker)
            {
                case "b":
                case "byte":
                case "bytes":
                    result = new FileSize(count);
                    return true;

                case "kilobyte":
                case "kilobytes":
                case "kb":
                    result = FromKilobytes((int)count);
                    return true;

                case "megabyte":
                case "megabytes":
                case "mb":
                    result = FromMegabytes((int)count);
                    return true;

                case "gigabyte":
                case "gigabytes":
                case "gb":
                    result = FromGigabytes((int)count);
                    return true;

                case "terabyte":
                case "terabytes":
                case "tb":
                    result = FromTerabytes((int)count);
                    return true;

                case "petabyte":
                case "petabytes":
                case "pb":
                    result = FromPetabytes((int)count);
                    return true;

                case "exabyte":
                case "exabytes":
                case "ex":
                    result = FromExabytes((int)count);
                    return true;

                default:
                    result = null;
                    return false;
            }
        }

        #endregion

    }
}
