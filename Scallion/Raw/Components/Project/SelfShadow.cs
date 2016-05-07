using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;
using Scallion.DomainModels.Components;

namespace Scallion.Raw.Components.Project
{
    internal class SelfShadow : MMDObject
    {
        public bool IsEnabled { get; set; }
        public SelfShadowKeyFrame InitialKeyFrame { get; set; }
        public List<SelfShadowKeyFrame> KeyFrames { get; set; }
        public SelfShadowState CurrentStatus { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteByte((byte)(IsEnabled ? 1 : 0));
            archive.Serialize(CurrentStatus);
            if (!InitialKeyFrame.IsInitialKeyFrame) throw new InvalidOperationException("IsInitial property of InitialKeyFrame must be true.");
            archive.Serialize(InitialKeyFrame);
            archive.SerializeList(KeyFrames);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            IsEnabled = archive.ReadByte() == 1;
            CurrentStatus = archive.Deserialize<SelfShadowState>();
            InitialKeyFrame = archive.Deserialize(new SelfShadowKeyFrame() { IsInitialKeyFrame = true });
            KeyFrames = archive.DeserializeList<SelfShadowKeyFrame>(archive.ReadInt32());
        }
    }

    internal class SelfShadowState : MMDObject
    {
        public int Distance { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteSingle((float)((10000 - Distance) / 100000.0));
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Distance = (int)Math.Round(10000 - archive.ReadSingle() * 100000);
        }
    }

    internal class SelfShadowKeyFrame : LinkableKeyFrame<SelfShadowState>
    {
        public SelfShadowType SelfShadowType { get; set; }

        internal override void SerializeKeyFrameValue(MoSerializer archive)
        {
            archive.WriteByte((byte)SelfShadowType);
            base.SerializeKeyFrameValue(archive);
        }

        internal override void DeserializeKeyFrameValue(MoDeserializer archive)
        {
            SelfShadowType = (SelfShadowType)archive.ReadByte();
            base.DeserializeKeyFrameValue(archive);
        }
    }
}
