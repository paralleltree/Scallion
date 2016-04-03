using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using System.Numerics;
using Scallion.Core;
using Scallion.Internal;

namespace Scallion.Raw.Components.Motion
{
    internal class LightKeyFrame : KeyFrame
    {
        public Vector3 Position { get; set; }
        public Color Color { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteInt32(KeyFrameIndex);
            archive.Serialize(new ColorWrapper(Color));
            archive.Serialize(new Vector3Wrapper(Position));
        }

        public override void Deserialize(MoDeserializer archive)
        {
            KeyFrameIndex = archive.ReadInt32();
            Color = archive.Deserialize<ColorWrapper>().Value;
            Position = archive.Deserialize<Vector3Wrapper>().Value;
        }
    }
}
