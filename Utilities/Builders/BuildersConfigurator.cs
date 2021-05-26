using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.Builders
{
    public class BuildersConfigurator<T, E>
        where T : class, INamed
        where E : T, new()
    {
        public List<T> Builders { get; private set; }

        /// <summary>
        /// The method to execute when a builder is added
        /// </summary>
        private Action<T> _onBeforeAddBuilder;

        public BuildersConfigurator(List<T> builders, Action<T> onBeforeAddBuilder = null)
        {
            Builders = builders;

            _onBeforeAddBuilder = onBeforeAddBuilder;
        }

        public virtual BuildersConfigurator<T, E> Add(params E[] builders)
        {
            foreach (var builder in builders)
            {
                if (Builders.SingleOrDefault(b => b.Name == builder.Name) != null)
                {
                    throw new InvalidOperationException($"Builder of name: '{builder.Name}' already exists.");
                }

                if (_onBeforeAddBuilder != null)
                {
                    _onBeforeAddBuilder(builder);
                }


                Builders.Add(builder);
            }

            return this;
        }

        /// <summary>
        /// Adds builders to the configurator
        /// </summary>
        /// <param name="factories"></param>
        /// <returns></returns>
        public BuildersConfigurator<T, E> Add(params Action<E>[] configures)
        {
            return Add(
                configures.Select(
                    configure =>
                    {
                        var b = new E();

                        configure(b);

                        return b;
                    }
                )
                .ToArray()
            );
        }

        /// <summary>
        /// Configures a single builder
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public BuildersConfigurator<T, E> Configure(string name, Action<T> configure)
        {
            var builder = Builders.SingleOrDefault(b => b.Name == name);

            if (builder == null)
            {
                throw new InvalidOperationException($"There is no builder with name: {name}");
            }

            configure(builder);

            return this;
        }

        /// <summary>
        /// Removes builders from the configurator by their names
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public BuildersConfigurator<T, E> Remove(params string[] names)
        {
            if (names.Length == 1 && names[0] == "*")
            {
                Builders.Clear();
            }

            //TODO: Maybe throw an exception when the member is not found?
            Builders = Builders.Where(b => !names.Contains(b.Name)).ToList();

            return this;
        }

        /// <summary>
        /// Removes all the builders from the configurator except the ones listed in the names parameter
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public BuildersConfigurator<T, E> RemoveExcept(params string[] names)
        {
            //TODO: Maybe throw an exception when the member is not found?
            Builders = Builders.Where(b => names.Contains(b.Name)).ToList();

            return this;
        }
    }
}
