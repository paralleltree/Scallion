using System;
using System.Collections.Generic;
using System.Linq;

using System.Numerics;
using Scallion.Core;
using Scallion.Internal;

namespace Scallion.Raw.Components.Project
{
    internal class Gravity : MMDObject
    {
        public GravityKeyFrame InitialKeyFrame { get; set; }
        public List<GravityKeyFrame> KeyFrames { get; set; }
        public GravityState CurrentStatus { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.Serialize(CurrentStatus);
            if (!InitialKeyFrame.IsInitialKeyFrame) throw new InvalidOperationException("IsInitial property of InitialKeyFrame must be true.");
            archive.Serialize(InitialKeyFrame);
            archive.SerializeList(KeyFrames);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            CurrentStatus = archive.Deserialize<GravityState>();
            InitialKeyFrame = archive.Deserialize(new GravityKeyFrame() { IsInitialKeyFrame = true });
            KeyFrames = archive.DeserializeList<GravityKeyFrame>(archive.ReadInt32());
        }
    }

    internal class GravityState : MMDObject
    {
        public float Acceleration { get; set; }
        public int NoiseAmount { get; set; }
        public Vector3 Direction { get; set; }
        public bool IsNoiseEnabled { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteSingle(Acceleration);
            archive.WriteInt32(NoiseAmount);
            archive.Serialize(new Vector3Wrapper(Direction));
            archive.WriteByte((byte)(IsNoiseEnabled ? 1 : 0));
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Acceleration = archive.ReadSingle();
            NoiseAmount = archive.ReadInt32();
            Direction = archive.Deserialize<Vector3Wrapper>().Value;
            IsNoiseEnabled = archive.ReadByte() == 1;
        }
    }

    internal class GravityKeyFrame : LinkableKeyFrame<GravityState>
    {
        internal override void SerializeKeyFrameValue(MoSerializer archive)
        {
            archive.WriteByte((byte)(Value.IsNoiseEnabled ? 1 : 0));
            archive.WriteInt32(Value.NoiseAmount);
            archive.WriteSingle(Value.Acceleration);
            archive.Serialize(new Vector3Wrapper(Value.Direction));
        }

        internal override void DeserializeKeyFrameValue(MoDeserializer archive)
        {
            Value = new GravityState()
            {
                IsNoiseEnabled = archive.ReadByte() == 1,
                NoiseAmount = archive.ReadInt32(),
                Acceleration = archive.ReadSingle(),
                Direction = archive.Deserialize<Vector3Wrapper>().Value
            };
        }
    }
}
