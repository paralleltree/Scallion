using System;
using System.Collections.Generic;
using System.Linq;

using System.Numerics;
using Scallion.Core;
using Scallion.Internal;

namespace Scallion.Raw.Components.Motion
{
    internal class BoneKeyFrame : KeyFrame
    {
        public string BoneName { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Quaternion { get; set; }
        public BoneInterpolationImpl Interpolation { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteByteString(BoneName, 15);
            archive.WriteInt32(KeyFrameIndex);
            archive.Serialize(new Vector3Wrapper(Position));
            archive.Serialize(new QuaternionWrapper(Quaternion));
            archive.Serialize(Interpolation);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            BoneName = archive.ReadByteString(15).TrimNull();
            KeyFrameIndex = archive.ReadInt32();
            Position = archive.Deserialize<Vector3Wrapper>().Value;
            Quaternion = archive.Deserialize<QuaternionWrapper>().Value;
            Interpolation = archive.Deserialize<BoneInterpolationImpl>();
        }
    }
}
