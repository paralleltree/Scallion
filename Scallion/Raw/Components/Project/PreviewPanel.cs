using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;
using Scallion.DomainModels.Components;

namespace Scallion.Raw.Components.Project
{
    internal class PreviewPanel : MMDObject
    {
        public CameraFollowingType CameraFollowingType { get; set; }
        public bool IsRepeating { get; set; }
        public bool IsStartingFromCurrentFrame { get; set; }
        public bool IsStayingAtStoppedFrame { get; set; }
        public int StartFrameIndex { get; set; }
        public int EndFrameIndex { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteByte((byte)(CameraFollowingType));
            archive.WriteByte((byte)(IsRepeating ? 1 : 0));
            archive.WriteByte((byte)(IsStayingAtStoppedFrame ? 1 : 0));
            archive.WriteByte((byte)(IsStartingFromCurrentFrame ? 1 : 0));
            archive.WriteInt32(StartFrameIndex);
            archive.WriteInt32(EndFrameIndex);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            CameraFollowingType = (CameraFollowingType)archive.ReadByte();
            IsRepeating = archive.ReadByte() == 1;
            IsStayingAtStoppedFrame = archive.ReadByte() == 1;
            IsStartingFromCurrentFrame = archive.ReadByte() == 1;
            StartFrameIndex = archive.ReadInt32();
            EndFrameIndex = archive.ReadInt32();
        }
    }
}
