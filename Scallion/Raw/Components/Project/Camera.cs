using System;
using System.Collections.Generic;
using System.Linq;

using System.Numerics;
using Scallion.Core;
using Scallion.Internal;

namespace Scallion.Raw.Components.Project
{
    internal class Camera : MMDObject
    {
        public CameraKeyFrame InitialKeyFrame { get; set; }
        public List<CameraKeyFrame> KeyFrames { get; set; }
        public CurrentCameraState CurrentStatus { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            if (!InitialKeyFrame.IsInitialKeyFrame) throw new InvalidOperationException("IsInitial property of InitialKeyFrame must be true.");
            archive.Serialize(InitialKeyFrame);
            archive.SerializeList(KeyFrames);
            archive.Serialize(CurrentStatus);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            InitialKeyFrame = archive.Deserialize(new CameraKeyFrame() { IsInitialKeyFrame = true });
            KeyFrames = archive.DeserializeList<CameraKeyFrame>(archive.ReadInt32());
            CurrentStatus = archive.Deserialize<CurrentCameraState>();
        }
    }

    internal class CameraKeyFrame : LinkableKeyFrame<CameraState>
    {
        public float Distance { get; set; }
        public BoneReference FollowingBone { get; set; }
        public CameraInterpolationImpl Interpolation { get; set; }
        public int AngleOfView { get; set; }


        internal override void SerializeKeyFrameValue(MoSerializer archive)
        {
            archive.WriteSingle(-Distance);
            archive.Serialize(new Vector3Wrapper(Value.CenterPosition));
            archive.Serialize(new Vector3Wrapper(new Vector3(-Value.Rotation.X, Value.Rotation.Y, Value.Rotation.Z)));
            archive.WriteInt32(FollowingBone.ModelIndex);
            archive.WriteInt32(FollowingBone.BoneIndex);
            archive.Serialize(Interpolation);
            archive.WriteByte((byte)(Value.IsPerspectiveEnabled ? 0 : 1));
            archive.WriteInt32(AngleOfView);
        }

        internal override void DeserializeKeyFrameValue(MoDeserializer archive)
        {
            Distance = -archive.ReadSingle();
            Value = new CameraState();
            Value.CenterPosition = archive.Deserialize<Vector3Wrapper>().Value;
            var r = archive.Deserialize<Vector3Wrapper>().Value;
            Value.Rotation = new Vector3(-r.X, r.Y, r.Z);
            FollowingBone = new BoneReference(archive.ReadInt32(), archive.ReadInt32());
            Interpolation = archive.Deserialize<CameraInterpolationImpl>();
            Value.IsPerspectiveEnabled = archive.ReadByte() == 0;
            AngleOfView = archive.ReadInt32();
        }
    }

    internal class CameraState : MMDObject
    {
        public Vector3 CenterPosition { get; set; }
        public Vector3 Rotation { get; set; }
        public bool IsPerspectiveEnabled { get; set; }


        public override void Serialize(MoSerializer archive)
        {
            archive.Serialize(new Vector3Wrapper(CenterPosition));
            archive.Serialize(new Vector3Wrapper(Rotation));
            archive.WriteByte((byte)(IsPerspectiveEnabled ? 1 : 0));
        }

        public override void Deserialize(MoDeserializer archive)
        {
            CenterPosition = archive.Deserialize<Vector3Wrapper>().Value;
            Rotation = archive.Deserialize<Vector3Wrapper>().Value;
            IsPerspectiveEnabled = archive.ReadByte() == 1;
        }
    }

    internal class CurrentCameraState : CameraState
    {
        public Vector3 OffsetPosition { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.Serialize(new Vector3Wrapper(CenterPosition));
            archive.Serialize(new Vector3Wrapper(OffsetPosition));
            archive.Serialize(new Vector3Wrapper(Rotation));
            archive.WriteByte((byte)(IsPerspectiveEnabled ? 0 : 1));
        }

        public override void Deserialize(MoDeserializer archive)
        {
            CenterPosition = archive.Deserialize<Vector3Wrapper>().Value;
            OffsetPosition = archive.Deserialize<Vector3Wrapper>().Value;
            Rotation = archive.Deserialize<Vector3Wrapper>().Value;
            IsPerspectiveEnabled = archive.ReadByte() == 0;
        }
    }
}
