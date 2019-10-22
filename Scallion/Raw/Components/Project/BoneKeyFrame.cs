using System;
using System.Collections.Generic;
using System.Linq;

using System.Numerics;
using Scallion.Core;
using Scallion.Internal;

namespace Scallion.Raw.Components.Project
{
    internal class BoneState : MMDObject
    {
        public BoneInterpolationImpl Interpolation { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Quaternion { get; set; }
        public bool IsPhysicsEnabled { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            throw new InvalidOperationException("This instance will not be serialized directly.");
        }

        public override void Deserialize(MoDeserializer archive)
        {
            throw new InvalidOperationException("This instance will not be deserialized directly.");
        }
    }

    internal class BoneKeyFrame : LinkableKeyFrame<BoneState>
    {
        internal override void SerializeKeyFrame(MoSerializer archive)
        {
            archive.Serialize(Value.Interpolation);
            archive.Serialize(new Vector3Wrapper(Value.Position));
            archive.Serialize(new QuaternionWrapper(Value.Quaternion));
            archive.WriteByte((byte)(IsSelected ? 1 : 0));
            archive.WriteByte((byte)(Value.IsPhysicsEnabled ? 0 : 1));
        }

        internal override void DeserializeKeyFrame(MoDeserializer archive)
        {
            Value = new BoneState();
            Value.Interpolation = archive.Deserialize<BoneInterpolationImpl>();
            Value.Position = archive.Deserialize<Vector3Wrapper>().Value;
            Value.Quaternion = archive.Deserialize<QuaternionWrapper>().Value;
            IsSelected = archive.ReadByte() == 1;
            Value.IsPhysicsEnabled = archive.ReadByte() == 0;
        }
    }

    internal class CurrentBoneState : MMDObject
    {
        public Vector3 Position { get; set; }
        public Quaternion Quaternion { get; set; }
        public bool IsSaved { get; set; }
        public bool IsPhysicsEnabled { get; set; }
        public bool IsRowSelected { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.Serialize(new Vector3Wrapper(Position));
            archive.Serialize(new QuaternionWrapper(Quaternion));
            archive.WriteByte((byte)(IsSaved ? 0 : 1));
            archive.WriteByte((byte)(IsPhysicsEnabled ? 0 : 1));
            archive.WriteByte((byte)(IsRowSelected ? 1 : 0));
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Position = archive.Deserialize<Vector3Wrapper>().Value;
            Quaternion = archive.Deserialize<QuaternionWrapper>().Value;
            IsSaved = archive.ReadByte() == 0;
            IsPhysicsEnabled = archive.ReadByte() == 0;
            IsRowSelected = archive.ReadByte() == 1;
        }
    }
}
