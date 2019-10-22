using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using System.Numerics;
using Scallion.Core;
using Scallion.Internal;

namespace Scallion.Raw.Components.Project
{
    internal class Light : MMDObject
    {
        public LightKeyFrame InitialKeyFrame { get; set; }
        public List<LightKeyFrame> KeyFrames { get; set; }
        public LightState CurrentStatus { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            if (!InitialKeyFrame.IsInitialKeyFrame) throw new InvalidOperationException("IsInitial property of InitialKeyFrame must be true.");
            archive.Serialize(InitialKeyFrame);
            archive.SerializeList(KeyFrames);
            archive.Serialize(CurrentStatus);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            InitialKeyFrame = archive.Deserialize(new LightKeyFrame() { IsInitialKeyFrame = true });
            KeyFrames = archive.DeserializeList<LightKeyFrame>(archive.ReadInt32());
            CurrentStatus = archive.Deserialize<LightState>();
        }
    }

    internal class LightKeyFrame : LinkableKeyFrame<LightState>
    {
    }

    internal class LightState : MMDObject
    {
        public Color Color { get; set; }
        public Vector3 Position { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.Serialize(new ColorWrapper(Color));
            archive.Serialize(new Vector3Wrapper(Position));
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Color = archive.Deserialize<ColorWrapper>().Value;
            Position = archive.Deserialize<Vector3Wrapper>().Value;
        }
    }
}
