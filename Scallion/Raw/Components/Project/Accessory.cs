using System;
using System.Collections.Generic;
using System.Linq;

using System.Numerics;
using Scallion.Core;
using Scallion.Internal;

namespace Scallion.Raw.Components.Project
{
    internal class Accessory : MMDObject
    {
        public byte Index { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public byte RenderingOrder { get; set; }
        public bool IsAdditiveBlending { get; set; }

        public AccessoryKeyFrame InitialKeyFrame { get; set; }
        public List<AccessoryKeyFrame> KeyFrames { get; set; }
        public AccessoryState CurrentStatus { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteByte(Index);
            archive.WriteByteString(Name, 100);
            archive.WriteByteString(Path, 256);
            archive.WriteByte(RenderingOrder);

            if (!InitialKeyFrame.IsInitialKeyFrame) throw new InvalidOperationException("IsInitial property of InitialKeyFrame must be true.");
            archive.Serialize(InitialKeyFrame);
            archive.SerializeList(KeyFrames);
            archive.Serialize(CurrentStatus);
            archive.WriteByte((byte)(IsAdditiveBlending ? 1 : 0));
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Index = archive.ReadByte();
            Name = archive.ReadByteString(100).TrimNull();
            Path = archive.ReadByteString(256).TrimNull();
            RenderingOrder = archive.ReadByte();
            InitialKeyFrame = archive.Deserialize(new AccessoryKeyFrame() { IsInitialKeyFrame = true });
            KeyFrames = archive.DeserializeList<AccessoryKeyFrame>(archive.ReadInt32());
            CurrentStatus = archive.Deserialize<AccessoryState>();
            IsAdditiveBlending = archive.ReadByte() == 1;
        }
    }

    internal class AccessoryKeyFrame : LinkableKeyFrame<AccessoryState>
    {
    }

    internal class AccessoryState : MMDObject
    {
        public float Opacity { get; set; }
        public bool IsVisible { get; set; }
        public BoneReference ExternalParent { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public float Scale { get; set; }
        public bool IsShadowEnabled { get; set; }


        public override void Serialize(MoSerializer archive)
        {
            byte data = (byte)((int)(100 - 100 * Opacity) << 1);
            data |= (byte)(IsVisible ? 1 : 0);
            archive.WriteByte(data);
            archive.WriteInt32(ExternalParent.ModelIndex);
            archive.WriteInt32(ExternalParent.BoneIndex);
            archive.Serialize(new Vector3Wrapper(Position));
            archive.Serialize(new Vector3Wrapper(Rotation));
            archive.WriteSingle(Scale);
            archive.WriteByte((byte)(IsShadowEnabled ? 1 : 0));
        }

        public override void Deserialize(MoDeserializer archive)
        {
            byte data = archive.ReadByte();
            IsVisible = (data & 1) == 1;
            Opacity = (float)((100 - (data >> 1)) / 100.0);
            ExternalParent = new BoneReference(archive.ReadInt32(), archive.ReadInt32());
            Position = archive.Deserialize<Vector3Wrapper>().Value;
            Rotation = archive.Deserialize<Vector3Wrapper>().Value;
            Scale = archive.ReadSingle();
            IsShadowEnabled = archive.ReadByte() == 1;
        }
    }
}
