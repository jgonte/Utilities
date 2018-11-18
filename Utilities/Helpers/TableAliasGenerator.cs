using System.Collections.Generic;

namespace Utilities
{
    /// <summary>
    /// Generates an alias for the table using the first letter of the name of the table converted to lower case
    /// and a count to make it unique in case of tables that start with the same letter
    /// </summary>
    public class TableAliasGenerator
    {
        /// <summary>
        /// Generates a unique alias for a table
        /// </summary>
        /// <param propertyName="tableName"></param>
        /// <returns></returns>
        public string GenerateAlias(string tableName)
        {
            if (_aliases.ContainsKey(tableName))
            {
                return _aliases[tableName];
            }

            // Create an alias for that table name
            string letter = tableName[0].ToString().ToLower(); // Get the first character

            if (_lettersCount.ContainsKey(letter))
            {
                int count = _lettersCount[letter]; // Get the current count

                ++count;

                _lettersCount[letter] = count; // Update the count for that letter

                var alias = $"{letter}{count}";

                _aliases.Add(tableName, alias); // Register the alias for the name of the table

                return alias;

            }
            else
            {
                _lettersCount.Add(letter, 0); // Register a count for that letter

                _aliases.Add(tableName, letter); // Register the alias for the name of the table

                return letter;
            }
        }

        /// <summary>
        /// Caches the aliases to be reused for the same name, the key is the name of the table
        /// and the value is the created alias for that table
        /// </summary>
        private Dictionary<string, string> _aliases = new Dictionary<string, string>();

        /// <summary>
        /// Keeps track of the count per letters, the key is the letter and the value is the current count for that letter
        /// </summary>
        private Dictionary<string, int> _lettersCount = new Dictionary<string, int>();

    }
}