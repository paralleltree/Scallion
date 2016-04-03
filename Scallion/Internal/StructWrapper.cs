using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;

namespace Scallion.Internal
{
    internal abstract class StructWrapper<T> : MMDObject where T : struct
    {
        public T Value { get; set; }

        public StructWrapper()
        {
        }

        public StructWrapper(T value)
        {
            Value = value;
        }
    }
}
