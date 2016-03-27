using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Scallion.Core
{
    internal class MoDeserializer : MoIO
    {
        private BinaryReader _stream;


        public MoDeserializer(Stream input)
        {
            _stream = new BinaryReader(input, FileEncoding);
        }


        public T Deserialize<T>() where T : MMDObject, new()
        {
            return Deserialize(new T());
        }

        public T Deserialize<T>(T obj) where T : MMDObject
        {
            obj.Deserialize(this);
            return obj;
        }

        public List<T> DeserializeList<T>() where T : MMDObject, new()
        {
            int count = ReadInt32();
            var list = new List<T>(count);
            for (int i = 0; i < count; i++) list.Add(Deserialize<T>());
            return list;
        }


        public int ReadInt32()
        {
            return _stream.ReadInt32();
        }

        public float ReadSingle()
        {
            return _stream.ReadSingle();
        }

        public byte ReadByte()
        {
            return _stream.ReadByte();
        }

        public string ReadByteString(int bytesCount)
        {
            var b = _stream.ReadBytes(bytesCount);
            return FileEncoding.GetString(b);
        }

        public string ReadVariableString()
        {
            byte length = _stream.ReadByte();
            return ReadByteString(length);
        }
    }
}
