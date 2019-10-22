using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;

namespace Scallion.Raw.Components.Project
{
    internal class RangeSelection : MMDObject
    {
        public byte ModelIndex { get; set; }
        public int SelectedIndex { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteByte(ModelIndex);
            archive.WriteInt32(SelectedIndex);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            ModelIndex = archive.ReadByte();
            SelectedIndex = archive.ReadInt32();
        }
    }
}
