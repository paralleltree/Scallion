using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;

namespace Scallion.Raw.Components.Motion
{
    internal class SelfShadowKeyFrame : KeyFrame
    {
        public SelfShadowType Type { get; set; }
        public int Distance { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteInt32(KeyFrameIndex);
            archive.WriteByte(((byte)Type));
            archive.WriteSingle((float)((10000 - Distance) / 100000.0));
        }

        public override void Deserialize(MoDeserializer archive)
        {
            KeyFrameIndex = archive.ReadInt32();
            Type = (SelfShadowType)archive.ReadByte();
            Distance = (int)Math.Round(10000 - archive.ReadSingle() * 100000);
        }
    }

    public enum SelfShadowType
    {
        Off,
        Mode1,
        Mode2
    }
}
