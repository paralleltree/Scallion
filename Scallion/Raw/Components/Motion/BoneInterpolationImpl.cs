using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;
using Scallion.DomainModels.Components;

namespace Scallion.Raw.Components.Motion
{
    internal class BoneInterpolationImpl : BoneInterpolation
    {
        public override void Serialize(MoSerializer archive)
        {
            // I hate this...
            archive.WriteByte(X.First.X);
            archive.WriteByte(Y.First.X);
            archive.WriteByte(0);
            archive.WriteByte(0);
            archive.WriteByte(X.First.Y);
            archive.WriteByte(Y.First.Y);
            archive.WriteByte(Z.First.Y);
            archive.WriteByte(R.First.Y);
            archive.WriteByte(X.Second.X);
            archive.WriteByte(Y.Second.X);
            archive.WriteByte(Z.Second.X);
            archive.WriteByte(R.Second.X);
            archive.WriteByte(X.Second.Y);
            archive.WriteByte(Y.Second.Y);
            archive.WriteByte(Z.Second.Y);
            archive.WriteByte(R.Second.Y);
            archive.WriteByte(Y.First.X);
            archive.WriteByte(Z.First.X);
            archive.WriteByte(R.First.X);
            archive.WriteByte(X.First.Y);
            archive.WriteByte(Y.First.Y);
            archive.WriteByte(Z.First.Y);
            archive.WriteByte(R.First.Y);
            archive.WriteByte(X.Second.X);
            archive.WriteByte(Y.Second.X);
            archive.WriteByte(Z.Second.X);
            archive.WriteByte(R.Second.X);
            archive.WriteByte(X.Second.Y);
            archive.WriteByte(Y.Second.Y);
            archive.WriteByte(Z.Second.Y);
            archive.WriteByte(R.Second.Y);
            archive.WriteByte(0);
            archive.WriteByte(Z.First.X);
            archive.WriteByte(R.First.X);
            archive.WriteByte(X.First.Y);
            archive.WriteByte(Y.First.Y);
            archive.WriteByte(Z.First.Y);
            archive.WriteByte(R.First.Y);
            archive.WriteByte(X.Second.X);
            archive.WriteByte(Y.Second.X);
            archive.WriteByte(Z.Second.X);
            archive.WriteByte(R.Second.X);
            archive.WriteByte(X.Second.Y);
            archive.WriteByte(Y.Second.Y);
            archive.WriteByte(Z.Second.Y);
            archive.WriteByte(R.Second.Y);
            archive.WriteByte(0);
            archive.WriteByte(0);
            archive.WriteByte(R.First.X);
            archive.WriteByte(X.First.Y);
            archive.WriteByte(Y.First.Y);
            archive.WriteByte(Z.First.Y);
            archive.WriteByte(R.First.Y);
            archive.WriteByte(X.Second.X);
            archive.WriteByte(Y.Second.X);
            archive.WriteByte(Z.Second.X);
            archive.WriteByte(R.Second.X);
            archive.WriteByte(X.Second.Y);
            archive.WriteByte(Y.Second.Y);
            archive.WriteByte(Z.Second.Y);
            archive.WriteByte(R.Second.Y);
            archive.WriteByte(0);
            archive.WriteByte(0);
            archive.WriteByte(0);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            X = new Interpolation();
            Y = new Interpolation();
            Z = new Interpolation();
            R = new Interpolation();
            var x = new InterpolationParameter();
            var y = new InterpolationParameter();
            var z1 = new InterpolationParameter();
            var r1 = new InterpolationParameter();
            var z2 = new InterpolationParameter();
            var r2 = new InterpolationParameter();

            x.X = archive.ReadByte();
            y.X = archive.ReadByte();
            archive.ReadByte();
            archive.ReadByte();
            x.Y = archive.ReadByte();
            y.Y = archive.ReadByte();
            X.First = x;
            Y.First = y;

            z1.Y = archive.ReadByte();
            r1.Y = archive.ReadByte();
            x.X = archive.ReadByte();
            y.X = archive.ReadByte();
            z2.X = archive.ReadByte();
            r2.X = archive.ReadByte();
            x.Y = archive.ReadByte();
            y.Y = archive.ReadByte();
            z2.Y = archive.ReadByte();
            r2.Y = archive.ReadByte();
            archive.ReadByte();
            z1.X = archive.ReadByte();
            r1.X = archive.ReadByte();
            X.Second = x;
            Y.Second = y;
            Z.First = z1;
            Z.Second = z2;
            R.First = r1;
            R.Second = r2;

            for (int i = 0; i < 64 - 19; i++) archive.ReadByte();
        }
    }
}
