using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;
using Scallion.DomainModels.Components;

namespace Scallion.Raw.Components
{
    internal class CameraInterpolation : MMDObject
    {
        public Interpolation X { get; set; }
        public Interpolation Y { get; set; }
        public Interpolation Z { get; set; }
        public Interpolation R { get; set; }
        public Interpolation D { get; set; }
        public Interpolation V { get; set; }

        public CameraInterpolation()
        {
            X = new Interpolation();
            Y = new Interpolation();
            Z = new Interpolation();
            R = new Interpolation();
            D = new Interpolation();
            V = new Interpolation();
        }


        public override void Serialize(MoSerializer archive)
        {
            archive.WriteByte(X.First.X);
            archive.WriteByte(X.Second.X);
            archive.WriteByte(X.First.Y);
            archive.WriteByte(X.Second.Y);

            archive.WriteByte(Y.First.X);
            archive.WriteByte(Y.Second.X);
            archive.WriteByte(Y.First.Y);
            archive.WriteByte(Y.Second.Y);

            archive.WriteByte(Z.First.X);
            archive.WriteByte(Z.Second.X);
            archive.WriteByte(Z.First.Y);
            archive.WriteByte(Z.Second.Y);

            archive.WriteByte(R.First.X);
            archive.WriteByte(R.Second.X);
            archive.WriteByte(R.First.Y);
            archive.WriteByte(R.Second.Y);

            archive.WriteByte(D.First.X);
            archive.WriteByte(D.Second.X);
            archive.WriteByte(D.First.Y);
            archive.WriteByte(D.Second.Y);

            archive.WriteByte(V.First.X);
            archive.WriteByte(V.Second.X);
            archive.WriteByte(V.First.Y);
            archive.WriteByte(V.Second.Y);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            var first = new InterpolationParameter();
            var second = new InterpolationParameter();

            first.X = archive.ReadByte();
            second.X = archive.ReadByte();
            first.Y = archive.ReadByte();
            second.Y = archive.ReadByte();
            X.First = first;
            X.Second = second;

            first.X = archive.ReadByte();
            second.X = archive.ReadByte();
            first.Y = archive.ReadByte();
            second.Y = archive.ReadByte();
            Y.First = first;
            Y.Second = second;

            first.X = archive.ReadByte();
            second.X = archive.ReadByte();
            first.Y = archive.ReadByte();
            second.Y = archive.ReadByte();
            Z.First = first;
            Z.Second = second;

            first.X = archive.ReadByte();
            second.X = archive.ReadByte();
            first.Y = archive.ReadByte();
            second.Y = archive.ReadByte();
            R.First = first;
            R.Second = second;

            first.X = archive.ReadByte();
            second.X = archive.ReadByte();
            first.Y = archive.ReadByte();
            second.Y = archive.ReadByte();
            D.First = first;
            D.Second = second;

            first.X = archive.ReadByte();
            second.X = archive.ReadByte();
            first.Y = archive.ReadByte();
            second.Y = archive.ReadByte();
            V.First = first;
            V.Second = second;
        }
    }
}
