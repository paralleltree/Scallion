using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Motion
{
    internal class MotionConverter : IInstanceConverter<Raw.Motion, DomainModels.Motion>
    {
        public DomainModels.Motion Convert(Raw.Motion src, DomainModels.Motion obj)
        {
            obj.ModelName = src.ModelName;
            obj.Bones = new BoneConverter().Convert(src.BoneKeyFrames).ToList();
            obj.Morphs = new MorphConverter().Convert(src.MorphKeyFrames).ToList();
            obj.Camera = new Camera()
            {
                KeyFrames = new LinkedList<CameraKeyFrame>(src.CameraKeyFrames.Select(p => new CameraKeyFrame()
                {
                    Distance = p.Distance,
                    Position = p.Position,
                    Rotation = p.Rotation,
                    Interpolation = p.Interpolation,
                    AngleOfView = p.AngleOfView,
                    IsPerspectiveEnabled = p.IsPerspectiveEnabled,
                    KeyFrameIndex = p.KeyFrameIndex
                }))
            };
            obj.Light = new Light()
            {
                KeyFrames = new LinkedList<LightKeyFrame>(src.LightKeyFrames.Select(p => new LightKeyFrame()
                {
                    KeyFrameIndex = p.KeyFrameIndex,
                    Position = p.Position,
                    Color = p.Color
                }))
            };
            obj.SelfShadow = new SelfShadow()
            {
                KeyFrames = new LinkedList<SelfShadowKeyFrame>(src.SelfShadowKeyFrames.Select(p => new SelfShadowKeyFrame()
                {
                    KeyFrameIndex = p.KeyFrameIndex,
                    Type = p.Type,
                    Distance = p.Distance
                }))
            };
            new ModelKeyFrameConverter().Convert(src, obj);
            return obj;
        }

        public Raw.Motion ConvertBack(DomainModels.Motion src, Raw.Motion obj)
        {
            obj.ModelName = src.ModelName;
            obj.BoneKeyFrames = new BoneConverter().ConvertBack(src.Bones).ToList();
            obj.MorphKeyFrames = new MorphConverter().ConvertBack(src.Morphs).ToList();
            obj.CameraKeyFrames = src.Camera.KeyFrames.Select(p => new Raw.Components.Motion.CameraKeyFrame()
            {
                Distance = p.Distance,
                Position = p.Position,
                Rotation = p.Rotation,
                Interpolation = new Raw.Components.CameraInterpolation()
                {
                    X = p.Interpolation.X,
                    Y = p.Interpolation.Y,
                    Z = p.Interpolation.Z,
                    R = p.Interpolation.R,
                    D = p.Interpolation.D,
                    V = p.Interpolation.V
                },
                AngleOfView = p.AngleOfView,
                IsPerspectiveEnabled = p.IsPerspectiveEnabled,
                KeyFrameIndex = p.KeyFrameIndex
            }).ToList();
            obj.LightKeyFrames = src.Light.KeyFrames.Select(p => new Raw.Components.Motion.LightKeyFrame()
            {
                KeyFrameIndex = p.KeyFrameIndex,
                Position = p.Position,
                Color = p.Color
            }).ToList();
            obj.SelfShadowKeyFrames = src.SelfShadow.KeyFrames.Select(p => new Raw.Components.Motion.SelfShadowKeyFrame()
            {
                KeyFrameIndex = p.KeyFrameIndex,
                Type = p.Type,
                Distance = p.Distance
            }).ToList();
            new ModelKeyFrameConverter().ConvertBack(src, obj);
            return obj;
        }
    }
}
