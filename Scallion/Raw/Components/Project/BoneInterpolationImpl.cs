using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;
using Scallion.DomainModels.Components;

namespace Scallion.Raw.Components.Project
{
    internal class BoneInterpolationImpl : Components.BoneInterpolation
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

        }

        public override void Deserialize(MoDeserializer archive)
        {
            X = new Interpolation(new InterpolationParameter(archive.ReadByte(), archive.ReadByte()), new InterpolationParameter(archive.ReadByte(), archive.ReadByte()));
            Y = new Interpolation(new InterpolationParameter(archive.ReadByte(), archive.ReadByte()), new InterpolationParameter(archive.ReadByte(), archive.ReadByte()));
            Z = new Interpolation(new InterpolationParameter(archive.ReadByte(), archive.ReadByte()), new InterpolationParameter(archive.ReadByte(), archive.ReadByte()));
            R = new Interpolation(new InterpolationParameter(archive.ReadByte(), archive.ReadByte()), new InterpolationParameter(archive.ReadByte(), archive.ReadByte()));
        }
    }
}
