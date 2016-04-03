using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;
using Scallion.Internal;

namespace Scallion.Raw.Components.Motion
{
    internal class ModelKeyFrame : KeyFrame
    {
        public bool IsVisible { get; set; }
        public List<IKBoneData> IKData { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteInt32(KeyFrameIndex);
            archive.WriteByte((byte)(IsVisible ? 1 : 0));
            archive.SerializeList(IKData);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            KeyFrameIndex = archive.ReadInt32();
            IsVisible = archive.ReadByte() == 1;
            IKData = archive.DeserializeList<IKBoneData>();
        }
    }

    internal class IKBoneData : MMDObject
    {
        public string BoneName { get; set; }
        public bool IsEnabled { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteByteString(BoneName, 20);
            archive.WriteByte((byte)(IsEnabled ? 1 : 0));
        }

        public override void Deserialize(MoDeserializer archive)
        {
            BoneName = archive.ReadByteString(20).TrimNull();
            IsEnabled = archive.ReadByte() == 1;
        }
    }
}
