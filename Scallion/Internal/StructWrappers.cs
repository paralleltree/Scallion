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

    internal class Int32ColorWrapper : StructWrapper<Color>
    {
        public Int32ColorWrapper()
        {
        }

        public Int32ColorWrapper(Color value) : base(value)
        {
        }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteInt32((int)Value.R);
            archive.WriteInt32((int)Value.G);
            archive.WriteInt32((int)Value.B);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Value = Color.FromArgb(archive.ReadInt32(), archive.ReadInt32(), archive.ReadInt32());
        }
    }

    internal class PointWrapper : StructWrapper<Point>
    {
        public PointWrapper()
        {
        }

        public PointWrapper(Point value) : base(value)
        {
        }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteInt32(Value.X);
            archive.WriteInt32(Value.Y);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Value = new Point(archive.ReadInt32(), archive.ReadInt32());
        }
    }

    internal class SizeWrapper : StructWrapper<Size>
    {
        public SizeWrapper()
        {
        }

        public SizeWrapper(Size value) : base(value)
        {
        }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteInt32(Value.Width);
            archive.WriteInt32(Value.Height);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Value = new Size(archive.ReadInt32(), archive.ReadInt32());
        }
    }

    internal class BoneReferenceWrapper : StructWrapper<Raw.Components.Project.BoneReference>
    {
        public BoneReferenceWrapper()
        {
        }

        public BoneReferenceWrapper(Raw.Components.Project.BoneReference value)
            : base(value)
        {
        }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteInt32(Value.ModelIndex);
            archive.WriteInt32(Value.BoneIndex);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Value = new Raw.Components.Project.BoneReference(archive.ReadInt32(), archive.ReadInt32());
        }
    }
}
