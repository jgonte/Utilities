using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.Builders
{
    public class BuildersConfigurator<T> where T : INamed, new()
    {
        public List<T> Builders { get; private set; }

        public BuildersConfigurator(List<T> builders)
        {
            Builders = builders;
        }

        public BuildersConfigurator<T> Add(params T[] builders)
        {
            foreach (var builder in builders)
            {
                if (Builders.SingleOrDefault(b => b.Name == builder.Name) != null)
                {
                    throw new InvalidOperationException($"Builder of name: '{builder.Name}' already exists.");
                }

                Builders.Add(builder);
            }

            return this;
        }

        /// <summary>
        /// Adds builders to the configurator
        /// </summary>
        /// <param name="configures"></param>
        /// <returns></returns>
        public BuildersConfigurator<T> Add(params Action<T>[] configures)
        {
            return Add(
                configures.Select(
                    configure => 
                    {
                        var b = new T();

                        configure(b);

                        return b;
                    }
                )
                .ToArray()
            );
        }

        public IEnumerable<T> Configure(params string[] names)
        {
            if (names.Length == 1 && names[0] == "*")
            {
                return Builders; // Return all
            }

            return Builders.Where(b => names.Contains(b.Name));
        }

        /// <summary>
        /// Removes builders from the configurator by their names
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public BuildersConfigurator<T> Remove(params string[] names)
        {
            if (names.Length == 1 && names[0] == "*")
            {
                Builders.Clear();
            }

            Builders = Builders.Where(b => !names.Contains(b.Name)).ToList();

            return this;
        }
    }
}
