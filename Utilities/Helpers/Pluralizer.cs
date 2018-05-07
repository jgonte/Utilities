using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Utilities
{
    public static class Pluralizer
    {
        public static string Pluralize(string word)
        {
            if (_unpluralizables.Contains(word.ToLowerInvariant()))
            {
                return word;
            }

            // Sometimes adjectives can be used as a noun
            if (word.EndsWith("ed") || word.EndsWith("en"))
            {
                return word;
            }

            string plural = "";

            foreach (var pluralization in _pluralizations)
            {
                if (Regex.IsMatch(word, pluralization.Key))
                {
                    plural = Regex.Replace(word, pluralization.Key, pluralization.Value);
                    break;
                }
            }

            return plural;
        }

        private static readonly HashSet<string> _unpluralizables = new HashSet<string>
        {
             "equipment", 
             "information", 
             "rice", 
             "money", 
             "species", 
             "series", 
             "fish", 
             "sheep", 
             "deer"
        };

        private static readonly IDictionary<string, string> _pluralizations = new Dictionary<string, string>
        {
            // Start with the rarest cases, and move to the most common
            { "(P|p)erson$", "$1eople" },
            { "(O|o)x$", "$1xen" },
            { "(C|c)hild$", "$1hildren" },
            { "(F|f)oot$", "$1eet" },
            { "(T|t)ooth$", "$1eeth" },
            { "(G|g)oose$", "$1eese" },
            { "(Octop|octop|Vir|vir)us$", "$1i"},
            { "([M|m|L|l])ouse$", "$1ice" },
            // General rules.
            { "(.*)fe$", "$1ves" },         //  wife
            { "(.*)f$", "$1ves" },         // ie, wolf, elf
            { "(.*)man$", "$1men" },
            { "(.+[aeiou]y)$", "$1s" },
            { "(.+[^aeiou])y$", "$1ies" },
            { "(.+z)$", "$1zes" },     
            { "(.+)(e|i)x$", @"$1ices"},    // ie, Matrix, Index            
            { "(.+(s|x|sh|ch))$", @"$1es"},
            { "(.+)", @"$1s" }
        };
    }
}
