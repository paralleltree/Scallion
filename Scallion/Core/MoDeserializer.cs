using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Scallion.Core
{
    /// <summary>
    /// Represents a deserializer for <see cref="MMDObject"/>.
    /// </summary>
    internal class MoDeserializer : MoIO
    {
        private BinaryReader _stream;


        /// <summary>
        /// Initializes a new instance of the <see cref="MoDeserializer"/> class for the specified stream.
        /// </summary>
        /// <param name="input">The stream to be read</param>
        public MoDeserializer(Stream input)
        {
            _stream = new BinaryReader(input, FileEncoding);
        }


        /// <summary>
        /// Deserializes an object of the <typeparamref name="T"/> class from the stream.
        /// </summary>
        /// <typeparam name="T">The type of a object to be deserialized</typeparam>
        /// <returns>The deserialized object of the <typeparamref name="T"/> class</returns>
        public T Deserialize<T>() where T : MMDObject, new()
        {
            return Deserialize(new T());
        }

        /// <summary>
        /// Deserializes an object of the <typeparamref name="T"/> class from the stream.
        /// </summary>
        /// <typeparam name="T">The type of a object to be deserialized</typeparam>
        /// <param name="obj">The object which deserialized value is assigned to</param>
        /// <returns>The object specified as <paramref name="obj"/></returns>
        public T Deserialize<T>(T obj) where T : MMDObject
        {
            obj.Deserialize(this);
            return obj;
        }

        /// <summary>
        /// Deserializes a collection of the <typeparamref name="T"/> class from the stream.
        /// </summary>
        /// <typeparam name="T">The type of a object to be deserialized</typeparam>
        /// <returns>The deserialized collection of the <typeparamref name="T"/> class</returns>
        public List<T> DeserializeList<T>() where T : MMDObject, new()
        {
            int count = ReadInt32();
            var list = new List<T>(count);
            for (int i = 0; i < count; i++) list.Add(Deserialize<T>());
            return list;
        }


        /// <summary>
        /// Reads a four-byte signed integer from the stream.
        /// </summary>
        /// <returns>A four-byte signed integer read from the stream.</returns>
        public int ReadInt32()
        {
            return _stream.ReadInt32();
        }

        /// <summary>
        /// Reads a four-byte floating point value from the stream.
        /// </summary>
        /// <returns>A four-byte floating point value read from the stream.</returns>
        public float ReadSingle()
        {
            return _stream.ReadSingle();
        }

        /// <summary>
        /// Reads an unsigned byte from the stream.
        /// </summary>
        /// <returns>An unsigned byte read from the stream</returns>
        public byte ReadByte()
        {
            return _stream.ReadByte();
        }

        /// <summary>
        /// Reads a byte array as a string encoded in <see cref="MoIO.FileEncoding"/>.
        /// </summary>
        /// <param name="bytesCount">The byte length to read</param>
        /// <returns>A string read and encoded in <see cref="MoIO.FileEncoding"/> from the stream</returns>
        public string ReadByteString(int bytesCount)
        {
            var b = _stream.ReadBytes(bytesCount);
            return FileEncoding.GetString(b);
        }

        /// <summary>
        /// Reads a byte array leading its length as a string encoded in <see cref="MoIO.FileEncoding"/>.
        /// </summary>
        /// <returns>A string read and encoded in <see cref="MoIO.FileEncoding"/> from the stream.</returns>
        public string ReadVariableString()
        {
            byte length = _stream.ReadByte();
            return ReadByteString(length);
        }
    }
}
