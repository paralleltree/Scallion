using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using Scallion.Core;
using Scallion.Internal;
using Scallion.Raw.Components.Project;

namespace Scallion.Raw
{
    internal class Project : MMDFile<Project>
    {
        public readonly string Signature = "Polygon Movie maker 0002";

        public Size OutputSize { get; set; }
        public int TimelinePanelWidth { get; set; }
        public float AngleOfView { get; set; }
        public bool IsModelSelected { get; set; }
        public PanelExpansion PanelExpansion { get; set; }
        public byte SelectedModelIndex { get; set; }

        public List<Model> Models { get; set; }
        public Camera Camera { get; set; }
        public Light Light { get; set; }

        public byte SelectedAccessoryIndex { get; set; }
        public int TimelinePanelTopAccessoryIndex { get; set; }
        public byte AccessoriesCount { get; set; }
        public List<Accessory> Accessories { get; set; }

        public TimelinePanelState TimelinePanelStatus { get; set; }
        public DomainModels.Components.BoneSelectionType BoneSelectionType { get; set; }
        public DomainModels.Components.CameraFollowingType CameraFollowingType { get; set; }
        public PreviewPanel PreviewPanel { get; set; }

        public Media Media { get; set; }

        public bool IsInformationVisible { get; set; }
        public bool IsAxesVisible { get; set; }
        public bool IsSurfaceShadowEnabled { get; set; }
        public float FpsLimit { get; set; }
        public DomainModels.Components.ScreenCaptureMode ScreenCapturingMode { get; set; }

        public int AccessoryRenderedAfterModelIndex { get; set; }
        public float SurfaceShadowBrightness { get; set; }
        public bool IsSurfaceShadowTransparent { get; set; }
        public DomainModels.Components.PhysicsMode PhysicsMode { get; set; }

        public Gravity Gravity { get; set; }
        public SelfShadow SelfShadow { get; set; }
        public Color EdgeColor { get; set; }
        public bool IsBackgroundBlack { get; set; }

        public BoneReference CameraFollowingBone { get; set; }
        public bool IsFollowingViewEnabled { get; set; }
        public bool IsGroundPhysicsEnabled { get; set; }

        public int FrameJumpingBoxValue { get; set; }
        public List<RangeSelection> RangeSelections { get; set; }


        public override void Serialize(MoSerializer archive)
        {
            archive.WriteByteString(Signature, 30);

            archive.Serialize(new SizeWrapper(OutputSize));
            archive.WriteInt32(TimelinePanelWidth);
            archive.WriteSingle(AngleOfView);
            archive.WriteByte((byte)(IsModelSelected ? 0 : 1));
            archive.Serialize(PanelExpansion);
            archive.WriteByte(SelectedModelIndex);

            archive.WriteByte((byte)Models.Count);
            foreach (var model in Models) archive.Serialize(model);
            archive.Serialize(Camera);
            archive.Serialize(Light);

            archive.WriteByte(SelectedAccessoryIndex);
            archive.WriteInt32(TimelinePanelTopAccessoryIndex);
            archive.WriteByte((byte)Accessories.Count);
            foreach (var acc in Accessories.OrderBy(p => p.RenderingOrder))
                archive.WriteByteString(acc.Name, 100);

            archive.SerializeListWithoutCount(Accessories);

            archive.Serialize(TimelinePanelStatus);
            archive.WriteInt32((int)BoneSelectionType);
            archive.WriteByte((byte)(CameraFollowingType));
            archive.Serialize(PreviewPanel);
            archive.Serialize(Media);

            archive.WriteByte((byte)(IsInformationVisible ? 1 : 0));
            archive.WriteByte((byte)(IsAxesVisible ? 1 : 0));
            archive.WriteByte((byte)(IsSurfaceShadowEnabled ? 1 : 0));
            archive.WriteSingle(FpsLimit);
            archive.WriteInt32((int)ScreenCapturingMode);
            archive.WriteInt32(AccessoryRenderedAfterModelIndex);
            archive.WriteSingle(SurfaceShadowBrightness);
            archive.WriteByte((byte)(IsSurfaceShadowTransparent ? 1 : 0));
            archive.WriteByte((byte)PhysicsMode);

            archive.Serialize(Gravity);
            archive.Serialize(SelfShadow);
            archive.Serialize(new Int32ColorWrapper(EdgeColor));
            archive.WriteByte((byte)(IsBackgroundBlack ? 1 : 0));

            archive.Serialize(new BoneReferenceWrapper(CameraFollowingBone));

            // unknown 64bits sequence(like matrix)
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    archive.WriteSingle(i == j ? 1 : 0);

            archive.WriteByte((byte)(IsFollowingViewEnabled ? 1 : 0));

            archive.WriteByte(0); // unknown

            archive.WriteByte((byte)(IsGroundPhysicsEnabled ? 1 : 0));
            archive.WriteInt32(FrameJumpingBoxValue);

            archive.WriteByte(1);
            archive.SerializeListWithoutCount(RangeSelections);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            if (archive.ReadByteString(30).TrimNull() != Signature)
                throw new ArgumentException("Unsupported or invalid .pmm file");

            OutputSize = archive.Deserialize<SizeWrapper>().Value;
            TimelinePanelWidth = archive.ReadInt32();
            AngleOfView = archive.ReadSingle();
            IsModelSelected = archive.ReadByte() == 0;
            PanelExpansion = archive.Deserialize<PanelExpansion>();
            SelectedModelIndex = archive.ReadByte();

            Models = archive.DeserializeList<Model>((int)archive.ReadByte());
            Camera = archive.Deserialize<Camera>();
            Light = archive.Deserialize<Light>();

            SelectedAccessoryIndex = archive.ReadByte();
            TimelinePanelTopAccessoryIndex = archive.ReadInt32();
            AccessoriesCount = archive.ReadByte();
            for (int i = 0; i < AccessoriesCount; i++) archive.ReadByteString(100);

            Accessories = archive.DeserializeList<Accessory>(AccessoriesCount);

            TimelinePanelStatus = archive.Deserialize<TimelinePanelState>();
            BoneSelectionType = (DomainModels.Components.BoneSelectionType)archive.ReadInt32();
            CameraFollowingType = (DomainModels.Components.CameraFollowingType)archive.ReadByte();
            PreviewPanel = archive.Deserialize<PreviewPanel>();
            Media = archive.Deserialize<Media>();

            IsInformationVisible = archive.ReadByte() == 1;
            IsAxesVisible = archive.ReadByte() == 1;
            IsSurfaceShadowEnabled = archive.ReadByte() == 1;
            FpsLimit = archive.ReadSingle();
            ScreenCapturingMode = (DomainModels.Components.ScreenCaptureMode)archive.ReadInt32();
            AccessoryRenderedAfterModelIndex = archive.ReadInt32();
            SurfaceShadowBrightness = archive.ReadSingle();
            IsSurfaceShadowTransparent = archive.ReadByte() == 1;
            PhysicsMode = (DomainModels.Components.PhysicsMode)archive.ReadByte();

            Gravity = archive.Deserialize<Gravity>();
            SelfShadow = archive.Deserialize<SelfShadow>();
            EdgeColor = archive.Deserialize<Int32ColorWrapper>().Value;
            IsBackgroundBlack = archive.ReadByte() == 1;

            // huh?
            CameraFollowingBone = new BoneReference(archive.ReadInt32(), archive.ReadInt32());

            // unknown 64bits sequence
            for (int i = 0; i < 64; i++) archive.ReadByte();
            IsFollowingViewEnabled = archive.ReadByte() == 1;
            archive.ReadByte(); // unknown
            IsGroundPhysicsEnabled = archive.ReadByte() == 1;
            FrameJumpingBoxValue = archive.ReadInt32();

            archive.ReadByte(); // after v9.24, this will be 1 that represents trailing section has valid data.
            RangeSelections = archive.DeserializeList<RangeSelection>(Models.Count);
        }
    }
}
