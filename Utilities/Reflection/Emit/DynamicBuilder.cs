using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Utilities
{
    /// <summary>
    /// Helper class to create dynamic code
    /// </summary>
    public static class DynamicBuilder
    {
        /// <summary>
        /// Creates an assembly builder
        /// </summary>
        /// <param name="name">The name of the assembly</param>
        /// <returns>The assembly builder</returns>
        public static AssemblyBuilder CreateAssembly(string name)
        {
            AssemblyName assemblyName = new AssemblyName(name);

            return  AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        }

        /// <summary>
        /// Creates a module builder
        /// </summary>
        /// <param name="assemblyBuilder">The assembly builder to create the module from</param>
        /// <param name="name">(Optional) the name of the module. If not provided the name of the assembly will be used</param>
        /// <returns>The module builder</returns>
        public static ModuleBuilder CreateModule(this AssemblyBuilder assemblyBuilder, string name = null)
        {
            string moduleName = string.IsNullOrEmpty(name) ? assemblyBuilder.GetName().Name : name;

            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(moduleName);

            return moduleBuilder;
        }

        /// <summary>
        /// Creates a type builder
        /// </summary>
        /// <param name="moduleBuilder">The module builder to build the type from</param>
        /// <param name="name"></param>
        /// <param name="attributes"></param>
        /// <param name="baseType"></param>
        /// <returns>The type builder</returns>
        public static TypeBuilder CreateType(this ModuleBuilder moduleBuilder, string name, TypeAttributes attributes = TypeAttributes.Class | TypeAttributes.Public, Type parent = null)
        {
            TypeBuilder typeBuilder = null;

            if (parent != null) // Derive from the base type
            {
                typeBuilder = moduleBuilder.DefineType(name, attributes, parent);
            }
            else
            {
                typeBuilder = moduleBuilder.DefineType(name, attributes);
            }

            return typeBuilder;
        }

        /// <summary>
        /// Creates an empty constructor
        /// </summary>
        /// <param name="typeBuilder"></param>
        /// <param name="attributes"></param>
        /// <param name="callingConvention"></param>
        /// <param name="parameterTypes"></param>
        /// <returns></returns>
        public static ConstructorBuilder CreateConstructor(this TypeBuilder typeBuilder, 
                                                           MethodAttributes attributes = MethodAttributes.Public,
                                                           CallingConventions callingConvention = CallingConventions.Standard,
                                                           Type[] parameterTypes = null)
        {
            if (parameterTypes == null)
            {
                parameterTypes = new Type[] { };
            }

            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(attributes, callingConvention, parameterTypes);

            return constructorBuilder;
        }

        /// <summary>
        /// Creates a method builder from an existing method info
        /// </summary>
        /// <param name="typeBuilder"></param>
        /// <param name="methodInfo"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static MethodBuilder CreateMethod(this TypeBuilder typeBuilder, MethodInfo methodInfo, MethodAttributes attributes = MethodAttributes.Public)
        {
            Type[] parameterTypes = methodInfo.GetParameters().SelectToArray(p => p.ParameterType);

            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, attributes, methodInfo.ReturnType, parameterTypes);

            return methodBuilder;
        }
    }
}
