using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Scallion.Core
{
    /// <summary>
    /// Provides I/O functionality for MMD file.
    /// </summary>
    /// <typeparam name="T">The actual type implemented this interface</typeparam>
    public interface IMMDFile<T>
    {
        /// <summary>
        /// Loads a file from the specified path.
        /// </summary>
        /// <param name="path">The file path to load</param>
        /// <returns>The self instance</returns>
        T Load(string path);

        /// <summary>
        /// Saves the instance to the specified path.
        /// </summary>
        /// <param name="path">The file path to save</param>
        void Save(string path);
    }

    /// <summary>
    /// Represents a MMD file and provides internal implementations of <see cref="IMMDFile{T}"/>.
    /// This class is abstract.
    /// </summary>
    /// <typeparam name="T">The actual type inheriting this class</typeparam>
    internal abstract class MMDFile<T> : MMDObject, IMMDFile<T> where T : MMDFile<T>
    {
        /// <summary>
        /// Loads a file from the specified path.
        /// </summary>
        /// <param name="path">The file path to load</param>
        /// <returns>The self instance</returns>
        public T Load(string path)
        {
            using (var input = new FileStream(path, FileMode.Open))
            {
                this.Deserialize(new MoDeserializer(input));
            }
            return (T)this;
        }

        /// <summary>
        /// Saves the instance to specified path.
        /// </summary>
        /// <param name="path">The file path to save</param>
        public void Save(string path)
        {
            using (var output = new FileStream(path, FileMode.Create))
            {
                this.Serialize(new MoSerializer(output));
            }
        }
    }
}
