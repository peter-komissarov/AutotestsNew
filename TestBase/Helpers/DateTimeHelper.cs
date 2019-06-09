using System;
using System.Globalization;

namespace TestBase.Helpers
{
    /// <summary>
    /// Provides help methods for DateTime type
    /// </summary>
    public static class DateTimeHelper
    {
        private const string dateTimeFormat = "dddd, dd MMMM yyyy HH:mm:ss.fff";

        /// <summary>
        /// Returns current DateTime as formatted string 
        /// </summary>
        public static string GetCurrentDateTime() => DateTime.UtcNow.ToString(dateTimeFormat, new CultureInfo("en-US"));
    }
}
