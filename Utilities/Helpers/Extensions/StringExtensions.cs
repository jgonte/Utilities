using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Utilities
{
    /// <summary>
    /// Extension methods to extend the functionality of the string class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns the right part of the string after the separator.
        /// If the separator is not found then the original string is returned
        /// </summary>
        /// <param name="s">The string to extract the right part from</param>
        /// <param name="separator">The string that delimits the right part to extract</param>
        /// <returns></returns>
        public static string Right(this string s, string separator)
        {
            int rightmostSeparator = s.LastIndexOf(separator);

            if (rightmostSeparator < 0) // Not found
            {
                return s;
            }

            return s.Substring(rightmostSeparator + separator.Length);
        }

        /// <summary>
        /// Returns the left part of the string before the separator.
        /// If the separator is not found then the original string is returned
        /// </summary>
        /// <param name="s">The string to extract the left part from</param>
        /// <param name="separator">The string that delimits the left part to extract</param>
        /// <returns></returns>
        public static string Left(this string s, string separator)
        {
            int leftmostSeparator = s.IndexOf(separator);

            if (leftmostSeparator < 0) // Not found
            {
                return s;
            }

            return s.Substring(0, leftmostSeparator);
        }

        /// <summary>
        /// Returns an empty string if the string is null
        /// </summary>
        /// <param name="s">The string to test</param>
        /// <returns>Returns an empty string if the string is null otherwise returns the same string</returns>
        public static string EmptyIfNull(this string s)
        {
            return s == null ? string.Empty : s;
        }

        /// <summary>
        /// Converts a string to camel case
        /// </summary>
        /// <param name="s">The string to convert to camel case</param>
        /// <returns></returns>
        public static string ToCamelCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            return Char.ToLower(s[0]) + s.Substring(1);
        }

        public static string CamelCaseToWords(this string s)
        {
            var words = Regex.Matches(s, @"(^\p{Ll}+|\p{Lu}+(?!\p{Ll})|\p{Lu}\p{Ll}+)")
                .OfType<Match>()
                .Select(m => m.Value)
                .ToArray();

            return string.Join(" ", words);
        }

        /// <summary>
        /// Converts a string to pascal case
        /// </summary>
        /// <param name="s">The string to convert to pascal case</param>
        /// <returns></returns>
        public static string ToPascalCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            return Char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string RemoveEnd(this string s, string toRemove)
        {
            if (s.EndsWith(toRemove))
            {
                return s.Substring(0, s.LastIndexOf(toRemove));
            }

            return s;
        }

        public static string Singularize(this string s)
        {
            if (s.EndsWith("ies")) { return s.Replace("ies", "y"); }
            if (s.EndsWith("s")) { return s.Substring(0, s.Length - 1); }

            return s;
        }

        public static string Pluralize(this string s)
        {
            return Pluralizer.Pluralize(s);
        }

        public static void SaveToFile(this string s, string path, bool createDir = true)
        {
            string dir = Path.GetDirectoryName(path);

            if (createDir
                && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText(path, s);
        }
    }
}
