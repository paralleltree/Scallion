using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.Internal
{
    internal static class IOExtension
    {
        public static string TrimNull(this string str)
        {
            return new string(str.TakeWhile(p => p != '\0').ToArray());
        }
    }
}
