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
        DateTimeOffset,
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

                // Binary
                typeof(byte[]),

                // Special
                typeof(Guid),
                typeof(DateTime),
                typeof(DateTimeOffset),
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

        #endregion

        #region Properties

        public static Type[] PrimitiveTypes { get; private set; }

        #endregion
    }
}
