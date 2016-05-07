using System;
using System.Collections.Generic;
using System.Linq;

using System.Numerics;
using Scallion.Core;
using Scallion.Internal;

namespace Scallion.Raw.Components.Motion
{
    internal class CameraKeyFrame : KeyFrame
    {
        public float Distance { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public CameraInterpolationImpl Interpolation { get; set; }
        public int AngleOfView { get; set; }
        public bool IsPerspectiveEnabled { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteInt32(KeyFrameIndex);
            archive.WriteSingle(-Distance);
            archive.Serialize(new Vector3Wrapper(Position));
            archive.Serialize(new Vector3Wrapper(new Vector3(-Rotation.X, Rotation.Y, Rotation.Z)));
            archive.Serialize(Interpolation);
            archive.WriteInt32(AngleOfView);
            archive.WriteByte((byte)(IsPerspectiveEnabled ? 0 : 1));
        }

        public override void Deserialize(MoDeserializer archive)
        {
            KeyFrameIndex = archive.ReadInt32();
            Distance = -archive.ReadSingle();
            var pos = archive.Deserialize<Vector3Wrapper>().Value;
            Position = new Vector3(pos.X, pos.Y, pos.Z);
            var r = archive.Deserialize<Vector3Wrapper>().Value;
            Rotation = new Vector3(-r.X, r.Y, r.Z);
            Interpolation = archive.Deserialize<CameraInterpolationImpl>();
            AngleOfView = archive.ReadInt32();
            IsPerspectiveEnabled = archive.ReadByte() == 0;
        }
    }
}
