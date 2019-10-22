using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;

namespace Scallion.Raw.Components.Project
{
    internal class PanelExpansion : MMDObject
    {
        public bool Camera { get; set; }
        public bool Light { get; set; }
        public bool Accessory { get; set; }
        public bool Bone { get; set; }
        public bool Morph { get; set; }
        public bool SelfShadow { get; set; }


        public override void Serialize(MoSerializer archive)
        {
            archive.WriteByte((byte)(Camera ? 1 : 0));
            archive.WriteByte((byte)(Light ? 1 : 0));
            archive.WriteByte((byte)(Accessory ? 1 : 0));
            archive.WriteByte((byte)(Bone ? 1 : 0));
            archive.WriteByte((byte)(Morph ? 1 : 0));
            archive.WriteByte((byte)(SelfShadow ? 1 : 0));
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Camera = archive.ReadByte() == 1;
            Light = archive.ReadByte() == 1;
            Accessory = archive.ReadByte() == 1;
            Bone = archive.ReadByte() == 1;
            Morph = archive.ReadByte() == 1;
            SelfShadow = archive.ReadByte() == 1;
        }
    }
}
