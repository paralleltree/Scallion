using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;

namespace Scallion.Raw.Components.Project
{
    internal class TimelinePanelState : MMDObject
    {
        public int CurrentFrameIndex { get; set; }
        public int HorizontalHeadFrameIndex { get; set; }
        public int HorizontalScale { get; set; }


        public override void Serialize(MoSerializer archive)
        {
            archive.WriteInt32(CurrentFrameIndex);
            archive.WriteInt32(HorizontalHeadFrameIndex);
            archive.WriteInt32(HorizontalScale);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            CurrentFrameIndex = archive.ReadInt32();
            HorizontalHeadFrameIndex = archive.ReadInt32();
            HorizontalScale = archive.ReadInt32();
        }
    }
}
