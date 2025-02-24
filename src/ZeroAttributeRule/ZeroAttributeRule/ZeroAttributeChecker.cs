using System;

namespace ZeroAttributeRule
{
    // A simple helper to check for attribute usage in C# source code.
    public static class ZeroAttributeChecker
    {
        // Returns true if the source code contains any attribute-like syntax.
        public static bool HasAttributes(string sourceCode)
        {
            if (sourceCode == null)
            {
                return false;
            }

            // This basic check searches for an opening and closing bracket.
            return sourceCode.Contains("[") && sourceCode.Contains("]");
        }
    }
}