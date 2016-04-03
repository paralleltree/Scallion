using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;
using Scallion.Internal;

namespace Scallion.Raw.Components.Motion
{
    internal class MorphKeyFrame : KeyFrame
    {
        public string MorphName { get; set; }
        public float Weight { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteByteString(MorphName, 15);
            archive.WriteInt32(KeyFrameIndex);
            archive.WriteSingle(Weight);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            MorphName = archive.ReadByteString(15).TrimNull();
            KeyFrameIndex = archive.ReadInt32();
            Weight = archive.ReadSingle();
        }
    }
}
