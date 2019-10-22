using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;
using Scallion.Internal;

namespace Scallion.Raw.Components.Project
{
    internal class Model : MMDObject
    {
        public byte Index { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Path { get; set; }
        public byte TimelinePanelRowsCount { get; set; }
        public int TimelinePanelTopRowIndex { get; set; }

        public int BonesCount { get { return BoneNames.Count; } }
        public List<string> BoneNames { get; set; }

        public int MorphsCount { get { return MorphNames.Count; } }
        public List<string> MorphNames { get; set; }

        public int IKBonesCount { get { return IKBoneIndices.Count; } }
        public List<int> IKBoneIndices { get; set; }

        public int ExternalParentBonesCount { get { return ExternalParentBoneIndices.Count; } }
        public List<int> ExternalParentBoneIndices { get; set; }

        public byte RenderingOrder { get; set; }
        public bool IsVisible { get; set; }
        public int SelectedBoneIndex { get; set; }
        public MorphSelection MorphPanel { get; set; }
        public List<bool> BoneGroupsExpansion { get; set; }
        public int LastFrameIndex { get; set; }

        public List<BoneKeyFrame> InitialBoneKeyFrames { get; set; }
        public List<BoneKeyFrame> BoneKeyFrames { get; set; }

        public List<MorphKeyFrame> InitialMorphKeyFrames { get; set; }
        public List<MorphKeyFrame> MorphKeyFrames { get; set; }

        public ModelKeyFrame InitialModelKeyFrame { get; set; }
        public List<ModelKeyFrame> ModelKeyFrames { get; set; }

        public List<CurrentBoneState> CurrentBoneStatuses { get; set; }
        public List<MorphState> CurrentMorphStatuses { get; set; }

        public List<bool> IKBonesEnabled { get; set; }
        public List<ExternalParentState> ExternalParentStatuses { get; set; }

        public bool IsAdditiveBlending { get; set; }
        public float EdgeWidth { get; set; }
        public bool IsSelfShadowEnabled { get; set; }
        public byte CalculationOrder { get; set; }


        public override void Serialize(MoSerializer archive)
        {
            archive.WriteByte(Index);
            archive.WriteVariableString(Name);
            archive.WriteVariableString(NameEn);
            archive.WriteByteString(Path, 256);
            archive.WriteByte(TimelinePanelRowsCount);

            archive.WriteInt32(BoneNames.Count);
            foreach (string name in BoneNames) archive.WriteVariableString(name);
            archive.WriteInt32(MorphNames.Count);
            foreach (string name in MorphNames) archive.WriteVariableString(name);
            archive.WriteInt32(IKBoneIndices.Count);
            foreach (int idx in IKBoneIndices) archive.WriteInt32(idx);
            archive.WriteInt32(ExternalParentBoneIndices.Count);
            foreach (int idx in ExternalParentBoneIndices) archive.WriteInt32(idx);

            archive.WriteByte(RenderingOrder);
            archive.WriteByte((byte)(IsVisible ? 1 : 0));
            archive.WriteInt32(SelectedBoneIndex);
            archive.Serialize(MorphPanel);
            archive.WriteByte((byte)BoneGroupsExpansion.Count);
            foreach (bool item in BoneGroupsExpansion) archive.WriteByte((byte)(item ? 1 : 0));
            archive.WriteInt32(TimelinePanelTopRowIndex);
            archive.WriteInt32(LastFrameIndex);

            archive.SerializeListWithoutCount(InitialBoneKeyFrames);
            archive.SerializeList(BoneKeyFrames);
            archive.SerializeListWithoutCount(InitialMorphKeyFrames);
            archive.SerializeList(MorphKeyFrames);
            archive.Serialize(InitialModelKeyFrame);
            archive.SerializeList(ModelKeyFrames);

            archive.SerializeListWithoutCount(CurrentBoneStatuses);
            archive.SerializeListWithoutCount(CurrentMorphStatuses);
            foreach (bool status in IKBonesEnabled) archive.WriteByte((byte)(status ? 1 : 0));
            archive.SerializeListWithoutCount(ExternalParentStatuses);

            archive.WriteByte((byte)(IsAdditiveBlending ? 1 : 0));
            archive.WriteSingle(EdgeWidth);
            archive.WriteByte((byte)(IsSelfShadowEnabled ? 1 : 0));
            archive.WriteByte(CalculationOrder);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Index = archive.ReadByte();
            Name = archive.ReadVariableString();
            NameEn = archive.ReadVariableString();
            Path = archive.ReadByteString(256).TrimNull();
            TimelinePanelRowsCount = archive.ReadByte();
            BoneNames = archive.DeserializeList(archive.ReadInt32(), () => archive.ReadVariableString());
            MorphNames = archive.DeserializeList(archive.ReadInt32(), () => archive.ReadVariableString());
            IKBoneIndices = archive.DeserializeList(archive.ReadInt32(), () => archive.ReadInt32());
            ExternalParentBoneIndices = archive.DeserializeList(archive.ReadInt32(), () => archive.ReadInt32());
            RenderingOrder = archive.ReadByte();
            IsVisible = archive.ReadByte() == 1;
            SelectedBoneIndex = archive.ReadInt32();
            MorphPanel = archive.Deserialize<MorphSelection>();
            BoneGroupsExpansion = archive.DeserializeList((int)archive.ReadByte(), () => archive.ReadByte() == 1);
            TimelinePanelTopRowIndex = archive.ReadInt32();
            LastFrameIndex = archive.ReadInt32();

            InitialBoneKeyFrames = archive.DeserializeList(BonesCount, () => archive.Deserialize(new BoneKeyFrame() { IsInitialKeyFrame = true }));
            BoneKeyFrames = archive.DeserializeList<BoneKeyFrame>(archive.ReadInt32());

            InitialMorphKeyFrames = archive.DeserializeList(MorphsCount, () => archive.Deserialize(new MorphKeyFrame() { IsInitialKeyFrame = true }));
            MorphKeyFrames = archive.DeserializeList<MorphKeyFrame>(archive.ReadInt32());

            InitialModelKeyFrame = archive.Deserialize(new ModelKeyFrame()
            {
                IsInitialKeyFrame = true,
                IKBonesCount = IKBonesCount,
                ExternalParentBonesCount = ExternalParentBonesCount
            });
            ModelKeyFrames = archive.DeserializeList(archive.ReadInt32(),
                () => archive.Deserialize(new ModelKeyFrame()
                {
                    IKBonesCount = IKBonesCount,
                    ExternalParentBonesCount = ExternalParentBonesCount
                })
            );

            CurrentBoneStatuses = archive.DeserializeList<CurrentBoneState>(BonesCount);
            CurrentMorphStatuses = archive.DeserializeList<MorphState>(MorphsCount);
            IKBonesEnabled = archive.DeserializeList(IKBonesCount, () => archive.ReadByte() == 1);
            ExternalParentStatuses = archive.DeserializeList<ExternalParentState>(ExternalParentBonesCount);

            IsAdditiveBlending = archive.ReadByte() == 1;
            EdgeWidth = archive.ReadSingle();
            IsSelfShadowEnabled = archive.ReadByte() == 1;
            CalculationOrder = archive.ReadByte();
        }
    }

    internal class MorphSelection : MMDObject
    {
        public int EyebrowIndex { get; set; }
        public int EyeIndex { get; set; }
        public int LipIndex { get; set; }
        public int OtherIndex { get; set; }


        public override void Serialize(MoSerializer archive)
        {
            archive.WriteInt32(EyebrowIndex);
            archive.WriteInt32(EyeIndex);
            archive.WriteInt32(LipIndex);
            archive.WriteInt32(OtherIndex);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            EyebrowIndex = archive.ReadInt32();
            EyeIndex = archive.ReadInt32();
            LipIndex = archive.ReadInt32();
            OtherIndex = archive.ReadInt32();
        }
    }

    internal class ExternalParentState : MMDObject
    {
        public int CurrentKeyFrameIndex { get; set; }
        public int NextKeyFrameIndex { get; set; }
        public BoneReference ExternalParent { get; set; }


        public override void Serialize(MoSerializer archive)
        {
            archive.WriteInt32(CurrentKeyFrameIndex);
            archive.WriteInt32(NextKeyFrameIndex);
            archive.WriteInt32(ExternalParent.ModelIndex);
            archive.WriteInt32(ExternalParent.BoneIndex);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            CurrentKeyFrameIndex = archive.ReadInt32();
            NextKeyFrameIndex = archive.ReadInt32();
            ExternalParent = new BoneReference(archive.ReadInt32(), archive.ReadInt32());
        }
    }
}
