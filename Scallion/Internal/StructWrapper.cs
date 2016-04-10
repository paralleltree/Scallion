using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;

namespace Scallion.Internal
{
    /// <summary>
    /// Implements abstract methods of the <see cref="MMDObject"/> class for structures.
    /// </summary>
    /// <typeparam name="T">The type of struct to serialize and deserialize</typeparam>
    internal abstract class StructWrapper<T> : MMDObject where T : struct
    {
        /// <summary>
        /// Gets or sets a value of the <typeparamref name="T"/> structure that is serialized or deserialized.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StructWrapper{T}"/> class.
        /// </summary>
        public StructWrapper()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StructWrapper{T}"/> class with the specified value.
        /// </summary>
        /// <param name="value"></param>
        public StructWrapper(T value)
        {
            Value = value;
        }
    }
}
