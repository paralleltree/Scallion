using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Scallion.Core
{
    /// <summary>
    /// Represents a serializer for <see cref="MMDObject"/>.
    /// </summary>
    internal class MoSerializer : MoIO, IDisposable
    {
        private BinaryWriter _stream;


        /// <summary>
        /// Initializes a new instance of the <see cref="MoSerializer"/> class for the specified stream.
        /// </summary>
        /// <param name="output">The stream to write</param>
        public MoSerializer(Stream output)
        {
            _stream = new BinaryWriter(output, FileEncoding);
        }


        /// <summary>
        /// Serializes the specified object of the <see cref="MMDObject"/> class to the stream.
        /// </summary>
        /// <typeparam name="T">The type of a object to be serialized</typeparam>
        /// <param name="item">The object to be serialized</param>
        public void Serialize<T>(T item) where T : MMDObject
        {
            item.Serialize(this);
        }

        /// <summary>
        /// Serializes a collection of the <typeparamref name="T"/> class without the number of its elements to the stream.
        /// </summary>
        /// <typeparam name="T">The type of objects to be serialized</typeparam>
        /// <param name="list">The collection to be serialized</param>
        public void SerializeListWithoutCount<T>(List<T> list) where T : MMDObject
        {
            foreach (var item in list) item.Serialize(this);
        }

        /// <summary>
        /// Serializes a collection of the <typeparamref name="T"/> class with the number of its elements to the stream.
        /// </summary>
        /// <typeparam name="T">The type of objects to be serialized</typeparam>
        /// <param name="list">The collection to be serialized</param>
        public void SerializeList<T>(List<T> list) where T : MMDObject
        {
            WriteInt32(list.Count);
            SerializeListWithoutCount(list);
        }


        /// <summary>
        /// Writes a four-byte signed integer to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteInt32(int value)
        {
            _stream.Write(value);
        }

        /// <summary>
        /// Writes a four-bytes floating point value to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteSingle(float value)
        {
            _stream.Write(value);
        }

        /// <summary>
        /// Writes an unsigned byte to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteByte(byte value)
        {
            _stream.Write(value);
        }

        /// <summary>
        /// Writes a string as a byte array encoded in <see cref="MoIO.FileEncoding"/> with specified bytes to the stream.
        /// </summary>
        /// <param name="value">The string to write</param>
        /// <param name="bytesCount">The length of a byte array to write</param>
        public void WriteByteString(string value, int bytesCount)
        {
            var data = new byte[bytesCount];
            var arr = FileEncoding.GetBytes(value);
            Array.Copy(arr, data, Math.Min(bytesCount, arr.Length));
            _stream.Write(data);
        }

        /// <summary>
        /// Writes a string as a byte array encoded in <see cref="MoIO.FileEncoding"/> with its length.
        /// </summary>
        /// <param name="value">The string to write</param>
        public void WriteVariableString(string value)
        {
            byte[] data = FileEncoding.GetBytes(value);
            _stream.Write((byte)data.Length);
            _stream.Write(data);
        }

        public void Dispose()
        {
            _stream.Dispose();
        }
    }
}
