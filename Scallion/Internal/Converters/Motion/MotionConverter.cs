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
                KeyFrames = src.CameraKeyFrames.Select(p => new CameraKeyFrame()
                {
                    KeyFrameIndex = p.KeyFrameIndex,
                    Value = new CameraState()
                    {
                        Distance = p.Distance,
                        Position = p.Position,
                        Rotation = p.Rotation,
                        Interpolation = p.Interpolation,
                        AngleOfView = p.AngleOfView,
                        IsPerspectiveEnabled = p.IsPerspectiveEnabled,
                    },
                }).ToList()
            };
            obj.Light = new Light()
            {
                KeyFrames = src.LightKeyFrames.Select(p => new LightKeyFrame()
                {
                    KeyFrameIndex = p.KeyFrameIndex,
                    Value = new LightState()
                    {
                        Position = p.Position,
                        Color = p.Color
                    }
                }).ToList()
            };
            obj.SelfShadow = new SelfShadow()
            {
                KeyFrames = src.SelfShadowKeyFrames.Select(p => new SelfShadowKeyFrame()
                {
                    KeyFrameIndex = p.KeyFrameIndex,
                    Value = new SelfShadowState()
                    {
                        Type = p.Type,
                        Distance = p.Distance
                    }
                }).ToList()
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
                Distance = p.Value.Distance,
                Position = p.Value.Position,
                Rotation = p.Value.Rotation,
                Interpolation = new Raw.Components.Motion.CameraInterpolationImpl()
                {
                    X = p.Value.Interpolation.X,
                    Y = p.Value.Interpolation.Y,
                    Z = p.Value.Interpolation.Z,
                    R = p.Value.Interpolation.R,
                    D = p.Value.Interpolation.D,
                    V = p.Value.Interpolation.V
                },
                AngleOfView = p.Value.AngleOfView,
                IsPerspectiveEnabled = p.Value.IsPerspectiveEnabled,
                KeyFrameIndex = p.KeyFrameIndex
            }).ToList();
            obj.LightKeyFrames = src.Light.KeyFrames.Select(p => new Raw.Components.Motion.LightKeyFrame()
            {
                KeyFrameIndex = p.KeyFrameIndex,
                Position = p.Value.Position,
                Color = p.Value.Color
            }).ToList();
            obj.SelfShadowKeyFrames = src.SelfShadow.KeyFrames.Select(p => new Raw.Components.Motion.SelfShadowKeyFrame()
            {
                KeyFrameIndex = p.KeyFrameIndex,
                Type = p.Value.Type,
                Distance = p.Value.Distance
            }).ToList();
            new ModelKeyFrameConverter().ConvertBack(src, obj);
            return obj;
        }
    }
}
