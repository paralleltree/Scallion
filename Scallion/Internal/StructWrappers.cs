using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using System.Numerics;
using Scallion.Core;

namespace Scallion.Internal
{
    internal class Vector3Wrapper : StructWrapper<Vector3>
    {
        public Vector3Wrapper()
        {
        }

        public Vector3Wrapper(Vector3 value) : base(value)
        {
        }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteSingle(Value.X);
            archive.WriteSingle(Value.Y);
            archive.WriteSingle(Value.Z);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Value = new Vector3(archive.ReadSingle(), archive.ReadSingle(), archive.ReadSingle());
        }
    }

    internal class QuaternionWrapper : StructWrapper<Quaternion>
    {
        public QuaternionWrapper()
        {
        }

        public QuaternionWrapper(Quaternion value) : base(value)
        {
        }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteSingle(Value.X);
            archive.WriteSingle(Value.Y);
            archive.WriteSingle(Value.Z);
            archive.WriteSingle(Value.W);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Value = new Quaternion(archive.ReadSingle(), archive.ReadSingle(), archive.ReadSingle(), archive.ReadSingle());
        }
    }

    internal class ColorWrapper : StructWrapper<Color>
    {
        public ColorWrapper()
        {
        }

        public ColorWrapper(Color value) : base(value)
        {
        }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteSingle((float)(Value.R / 256.0));
            archive.WriteSingle((float)(Value.G / 256.0));
            archive.WriteSingle((float)(Value.B / 256.0));
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Value = Color.FromArgb((int)(archive.ReadSingle() * 256), (int)(archive.ReadSingle() * 256), (int)(archive.ReadSingle() * 256));
        }
    }
}
