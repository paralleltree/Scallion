using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;
using Scallion.DomainModels.Components;

namespace Scallion.Raw.Components.Project
{
    internal class CameraInterpolationImpl : CameraInterpolation
    {
        public override void Serialize(MoSerializer archive)
        {
            archive.WriteByte(X.First.X);
            archive.WriteByte(X.First.Y);
            archive.WriteByte(X.Second.X);
            archive.WriteByte(X.Second.Y);

            archive.WriteByte(Y.First.X);
            archive.WriteByte(Y.First.Y);
            archive.WriteByte(Y.Second.X);
            archive.WriteByte(Y.Second.Y);

            archive.WriteByte(Z.First.X);
            archive.WriteByte(Z.First.Y);
            archive.WriteByte(Z.Second.X);
            archive.WriteByte(Z.Second.Y);

            archive.WriteByte(R.First.X);
            archive.WriteByte(R.First.Y);
            archive.WriteByte(R.Second.X);
            archive.WriteByte(R.Second.Y);

            archive.WriteByte(D.First.X);
            archive.WriteByte(D.First.Y);
            archive.WriteByte(D.Second.X);
            archive.WriteByte(D.Second.Y);

            archive.WriteByte(V.First.X);
            archive.WriteByte(V.First.Y);
            archive.WriteByte(V.Second.X);
            archive.WriteByte(V.Second.Y);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            X.First = new InterpolationParameter(archive.ReadByte(), archive.ReadByte());
            X.Second = new InterpolationParameter(archive.ReadByte(), archive.ReadByte());

            Y.First = new InterpolationParameter(archive.ReadByte(), archive.ReadByte());
            Y.Second = new InterpolationParameter(archive.ReadByte(), archive.ReadByte());

            Z.First = new InterpolationParameter(archive.ReadByte(), archive.ReadByte());
            Z.Second = new InterpolationParameter(archive.ReadByte(), archive.ReadByte());

            R.First = new InterpolationParameter(archive.ReadByte(), archive.ReadByte());
            R.Second = new InterpolationParameter(archive.ReadByte(), archive.ReadByte());

            D.First = new InterpolationParameter(archive.ReadByte(), archive.ReadByte());
            D.Second = new InterpolationParameter(archive.ReadByte(), archive.ReadByte());

            V.First = new InterpolationParameter(archive.ReadByte(), archive.ReadByte());
            V.Second = new InterpolationParameter(archive.ReadByte(), archive.ReadByte());
        }

    }
}
