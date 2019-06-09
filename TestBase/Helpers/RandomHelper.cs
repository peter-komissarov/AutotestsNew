using System;
using System.Text;
using System.Threading;

namespace TestBase.Helpers
{
    /// <summary>
    /// Provides help methods for random data generation
    /// </summary>
    public static class RandomHelper
    {
        private const string DefaultStringChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789_";
        private static int _seed = Environment.TickCount;

        private static readonly ThreadLocal<Random> RandomWrapper = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));

        private static Random GetThreadRandom()
        {
            return RandomWrapper.Value;
        }

        /// <summary>
        /// Returns a random bool.
        /// </summary>
        public static bool NextBool()
        {
            return GetThreadRandom().NextDouble() < 0.5;
        }

        /// <summary>
        /// Returns a random Byte within a specified range.
        /// </summary>
        public static byte NextByte(byte min, byte max)
        {
            return (byte)GetThreadRandom().Next(min, max);
        }

        /// <summary>
        /// Returns a random decimal within a specified range.
        /// </summary>
        public static decimal NextDecimal(decimal min, decimal max)
        {
            if (min > max)
            {
                throw new ArgumentException("Maximum value must be greater than or equal to minimum.", nameof(max));
            }

            if ((max < 0M == min < 0M || min + decimal.MaxValue >= max) == false)
            {
                throw new ArgumentException("Range too great for decimal data, use double range", nameof(max));
            }

            if (min == max)
            {
                return min;
            }

            var range = max - min;

            var limit = decimal.MaxValue - decimal.MaxValue % range;
            decimal raw;
            do
            {
                var low = GetThreadRandom().Next(0, int.MaxValue);
                var mid = GetThreadRandom().Next(0, int.MaxValue);
                var high = GetThreadRandom().Next(0, int.MaxValue);
                raw = new decimal(low, mid, high, false, 0);
            }
            while (raw > limit);

            return raw % range + min;
        }

        /// <summary>
        /// Returns a random double within a specified range.
        /// </summary>
        public static double NextDouble(double min, double max)
        {
            if (max < min)
            {
                throw new ArgumentException("Maximum value must be greater than or equal to minimum.", nameof(max));
            }

            if (min.Equals(max))
            {
                return min;
            }

            var range = max - min;
            return GetThreadRandom().NextDouble() * range + min;
        }

        /// <summary>
        /// Returns a random float within a specified range.
        /// </summary>
        public static float NextFloat(float min, float max)
        {
            return (float)NextDouble(min, max);
        }

        /// <summary>
        /// Returns a random Guid.
        /// </summary>
        public static Guid NextGuid()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Returns a random int within a specified range.
        /// </summary>
        public static int NextInt(int min, int max)
        {
            return GetThreadRandom().Next(min, max);
        }

        /// <summary>
        /// Returns a random long within a specified range.
        /// </summary>
        public static long NextLong(long min = 0L, long max = long.MaxValue)
        {
            if (min > max)
            {
                throw new ArgumentException("Maximum value must be greater than or equal to minimum.", nameof(max));
            }

            if (min == max)
            {
                return min;
            }

            var range = (ulong)(max - min);

            var limit = ulong.MaxValue - ulong.MaxValue % range;
            ulong raw;
            do
            {
                var buffer = new byte[sizeof(ulong)];
                GetThreadRandom().NextBytes(buffer);
                raw = BitConverter.ToUInt64(buffer, 0);
            }
            while (raw > limit);

            return (long)(raw % range + (ulong)min);
        }

        /// <summary>
        /// Returns a random short within a specified range.
        /// </summary>
        public static short NextShort(short min, short max)
        {
            return (short)GetThreadRandom().Next(min, max);
        }

        /// <summary>
        /// Generate a random string based on the characters from the input string.
        /// </summary>
        /// <returns>A random string of the default length</returns>
        /// <remarks>Uses <see cref="DefaultStringChars">DefaultStringChars</see> as the input character set </remarks>
        public static string NextString(int length)
        {
            var sb = new StringBuilder(length);

            for (var i = 0; i < length; i++)
            {
                sb.Append(DefaultStringChars[GetThreadRandom().Next(0, DefaultStringChars.Length)]);
            }

            return sb.ToString();
        }
    }
}