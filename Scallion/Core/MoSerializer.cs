using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Scallion.Core
{
    internal class MoSerializer : MoIO
    {
        private BinaryWriter _stream;


        public MoSerializer(Stream output)
        {
            _stream = new BinaryWriter(output, FileEncoding);
        }


        public void Serialize<T>(T item) where T : MMDObject
        {
            item.Serialize(this);
        }

        public void SerializeList<T>(List<T> list) where T : MMDObject
        {
            WriteInt32(list.Count);
            foreach (var item in list) item.Serialize(this);
        }


        public void WriteInt32(int value)
        {
            _stream.Write(value);
        }

        public void WriteSingle(float value)
        {
            _stream.Write(value);
        }

        public void WriteByte(byte value)
        {
            _stream.Write(value);
        }

        public void WriteByteString(string value, int bytesCount)
        {
            var data = new byte[bytesCount];
            FileEncoding.GetBytes(value).CopyTo(data, 0);
            _stream.Write(data);
        }

        public void WriteVariableString(string value)
        {
            byte[] data = FileEncoding.GetBytes(value);
            _stream.Write((byte)data.Length);
            _stream.Write(data);
        }
    }
}
