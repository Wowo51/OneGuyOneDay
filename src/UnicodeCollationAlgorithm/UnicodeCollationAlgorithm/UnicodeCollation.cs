using System;
using System.Globalization;

namespace UnicodeCollationAlgorithm
{
    public static class UnicodeCollation
    {
        public static int Compare(string left, string right)
        {
            string notNullLeft = left ?? string.Empty;
            string notNullRight = right ?? string.Empty;
            CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
            return compareInfo.Compare(notNullLeft, notNullRight, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase);
        }
    }
}