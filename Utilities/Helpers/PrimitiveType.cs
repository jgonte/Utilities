using System;
using System.Linq;

namespace Utilities
{
    public enum PrimitiveTypes
    {
        // Characters
        String = 1,
        Character, // Single character

        // Boolean
        Boolean,

        // Integers
        Byte,
        SignedByte,

        Short,
        UnsignedShort,

        Int,
        UnsignedInt,

        Long,
        UnsignedLong,

        // Decimals
        Float,
        Double,
        Decimal,

        // Special
        Guid,
        DateTime,
        Object
    };

    /// <summary>
    /// Contains the primitive types (in terms of business logic)
    /// </summary>
    public static class PrimitiveType
    {
        #region Constructors

        static PrimitiveType()
        {
            PrimitiveTypes = new Type[]
            {
                // Characters
                typeof(string),
                typeof(char),

                // Boolean
                typeof(bool),

                // Integers
                typeof(sbyte),
                typeof(byte),
               
                typeof(short),
                typeof(ushort),

                typeof(int),
                typeof(uint),

                typeof(long),
                typeof(ulong),

                // Decimals
                typeof(float),
                typeof(double),
                typeof(decimal),

                // Special
                typeof(Guid),
                typeof(DateTime),
                typeof(object)
                
                // Add primitive types here
            };
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tests whether the type is a primitive one from the business logic view.
        /// (Business logic view sees structures such as DateTime, Guid or enumerations as primitives)
        /// </summary>
        /// <param name="type">The type to be tested</param>
        /// <returns>true if the type is a primitive according to business logic view, false otherwise</returns>
        public static bool IsPrimitive(this Type type)
        {
            if (type.IsNullable()) // Unbox the nullable type
            {
                type = type.GetNullableType();
            }

            return (type.IsEnum
                || PrimitiveTypes.Contains(type));
        }

        /// <summary>
        /// Tests whether the type is a primitive one from the business logic view.
        /// </summary>
        /// <param name="name">Full name of the type to test</param>
        /// <returns></returns>
        public static bool IsPrimitive(string name)
        {
            return PrimitiveTypes.Where(p => p.FullName == name).FirstOrDefault() != null;
        }

        /// <summary>
        /// Tests whether the object is a primitive one from the business logic view.
        /// (Business logic view sees structures such as DateTime or Guid as primitives)
        /// </summary>
        /// <param name="target">The object to be tested</param>
        /// <returns>true if the object is a primitive according to business logic view, false otherwise</returns>
        public static bool IsPrimitive(this object target)
        {
            return IsPrimitive(target.GetType());
        }

        public static PrimitiveTypes GetPrimitive(Type type)
        {
            if (type == typeof(string))
            {
                return Utilities.PrimitiveTypes.String;
            }

            if (type == typeof(char))
            {
                return Utilities.PrimitiveTypes.Character;
            }

            if (type == typeof(bool))
            {
                return Utilities.PrimitiveTypes.Boolean;
            }

            if (type == typeof(sbyte))
            {
                return Utilities.PrimitiveTypes.SignedByte;
            }

            if (type == typeof(byte))
            {
                return Utilities.PrimitiveTypes.Byte;
            }

            if (type == typeof(short))
            {
                return Utilities.PrimitiveTypes.Short;
            }

            if (type == typeof(ushort))
            {
                return Utilities.PrimitiveTypes.UnsignedShort;
            }

            if (type == typeof(int))
            {
                return Utilities.PrimitiveTypes.Int;
            }

            if (type == typeof(uint))
            {
                return Utilities.PrimitiveTypes.UnsignedInt;
            }

            if (type == typeof(long))
            {
                return Utilities.PrimitiveTypes.Long;
            }

            if (type == typeof(ulong))
            {
                return Utilities.PrimitiveTypes.UnsignedLong;
            }

            if (type == typeof(float))
            {
                return Utilities.PrimitiveTypes.Float;
            }

            if (type == typeof(double))
            {
                return Utilities.PrimitiveTypes.Double;
            }

            if (type == typeof(decimal))
            {
                return Utilities.PrimitiveTypes.Decimal;
            }

            if (type == typeof(Guid))
            {
                return Utilities.PrimitiveTypes.Guid;
            }

            if (type == typeof(DateTime))
            {
                return Utilities.PrimitiveTypes.DateTime;
            }

            if (type == typeof(object))
            {
                return Utilities.PrimitiveTypes.Object;
            }

            throw new NotImplementedException("No primitive type defined for type: " + type);
        }

        #endregion

        #region Properties

        public static Type[] PrimitiveTypes { get; private set; }

        #endregion
    }
}
