using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scallion.Core
{
    /// <summary>
    /// Provides functionality for a serializable object with <see cref="MoSerializer"/> and <see cref="MoDeserializer"/>.
    /// </summary>
    internal interface IMoSerializable
    {
        /// <summary>
        /// Serializes a object with the specified <see cref="MoSerializer"/>.
        /// </summary>
        /// <param name="archive">The instance of <see cref="MoSerializer"/> to serialize</param>
        void Serialize(MoSerializer archive);

        /// <summary>
        /// Deserializes this instance with the specified <see cref="MoDeserializer"/>.
        /// </summary>
        /// <param name="archive">The instance of <see cref="MoDeserializer"/> to deserialize</param>
        void Deserialize(MoDeserializer archive);
    }

    /// <summary>
    /// Represents an object used in a MMD file.
    /// This class is abstract.
    /// </summary>
    internal abstract class MMDObject : IMoSerializable
    {
        /// <summary>
        /// Serializes a object with the specified <see cref="MoSerializer"/>.
        /// </summary>
        /// <param name="archive">The instance of <see cref="MoSerializer"/> to serialize</param>
        public abstract void Serialize(MoSerializer archive);

        /// <summary>
        /// Deserializes this instance with the specified <see cref="MoDeserializer"/>.
        /// </summary>
        /// <param name="archive">The instance of <see cref="MoDeserializer"/> to deserialize</param>
        public abstract void Deserialize(MoDeserializer archive);
    }

    /// <summary>
    /// Provides properties used for I/O.
    /// This class is abstract.
    /// </summary>
    internal abstract class MoIO
    {
        /// <summary>
        /// Gets or sets a character encoding to be used for I/O.
        /// </summary>
        public Encoding FileEncoding { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MoIO"/> class with Shift-JIS.
        /// </summary>
        public MoIO()
            : this(Encoding.GetEncoding("shift_jis"))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MoIO"/> class with the specified character encoding.
        /// </summary>
        /// <param name="enc">The character encoding to be used for I/O</param>
        public MoIO(Encoding enc)
        {
            FileEncoding = enc;
        }
    }
}
