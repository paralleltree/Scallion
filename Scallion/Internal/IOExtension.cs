using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.Internal
{
    /// <summary>
    /// Provides a set of static methods for I/O.
    /// </summary>
    internal static class IOExtension
    {
        /// <summary>
        /// Returns a new string that a first null character and followed characters are removed from the string.
        /// </summary>
        /// <param name="str">The string to be trimmed</param>
        /// <returns>A string that a first null character and followed characters are removed.</returns>
        public static string TrimNull(this string str)
        {
            return new string(str.TakeWhile(p => p != '\0').ToArray());
        }
    }
}
