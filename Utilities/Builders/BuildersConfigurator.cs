using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.Builders
{
    public class BuildersConfigurator<T> where T : IBuilder, INamed, new()
    {
        public List<T> Builders { get; private set; }

        public BuildersConfigurator(List<T> builders)
        {
            Builders = builders;
        }

        public virtual BuildersConfigurator<T> Add(params T[] builders)
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
                        var builder = new T();

                        configure(builder);

                        return builder;
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
        /// Configures a single builder
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public BuildersConfigurator<T> Configure(string name, Action<T> configure)
        {
            var builder = Builders.Single(b => b.Name == name);

            configure(builder);

            return this;
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

            //TODO: Maybe throw an exception when the member is not found?
            Builders = Builders.Where(b => !names.Contains(b.Name)).ToList();

            return this;
        }
    }
}
