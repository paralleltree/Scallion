using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;
using Scallion.Internal;

namespace Scallion.Raw.Components.Project
{
    internal class ModelKeyFrame : LinkableKeyFrame<ModelState>
    {
        public int IKBonesCount { get; set; }
        public int ExternalParentBonesCount { get; set; }
        public bool IsVisible { get; set; }

        internal override void SerializeKeyFrameValue(MoSerializer archive)
        {
            archive.WriteByte((byte)(IsVisible ? 1 : 0));
            archive.Serialize(Value);
        }

        internal override void DeserializeKeyFrameValue(MoDeserializer archive)
        {
            IsVisible = archive.ReadByte() == 1;
            Value = archive.Deserialize(new ModelState()
            {
                IKBonesCount = IKBonesCount,
                ExternalParentBonesCount = ExternalParentBonesCount
            });
        }
    }

    internal class ModelState : MMDObject
    {
        public int IKBonesCount { get; set; }
        public int ExternalParentBonesCount { get; set; }
        public List<bool> IKEnabled { get; set; }
        public List<BoneReference> ExternalParentBoneStatuses { get; set; }


        public override void Serialize(MoSerializer archive)
        {
            foreach (bool status in IKEnabled)
                archive.WriteByte((byte)(status ? 1 : 0));

            foreach (var item in ExternalParentBoneStatuses)
                new BoneReferenceWrapper(item).Serialize(archive);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            IKEnabled = archive.DeserializeList(IKBonesCount, () => archive.ReadByte() == 1);
            ExternalParentBoneStatuses = archive.DeserializeList(ExternalParentBonesCount, () => new BoneReference(archive.ReadInt32(), archive.ReadInt32()));
        }
    }
}
