using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;
using Scallion.Raw.Components.Project;

namespace Scallion.Internal.Converters.Project
{
    internal class CameraConverter : IConverter<Raw.Components.Project.Camera, DomainModels.Components.Camera>
    {
        public DomainModels.Components.Camera Convert(Raw.Components.Project.Camera src)
        {
            return new DomainModels.Components.Camera()
            {
                CurrentStatus = new DomainModels.Components.CameraState()
                {
                    Position = src.CurrentStatus.CenterPosition,
                    Rotation = src.CurrentStatus.Rotation,
                    // AngleOfView = Project.AngleOfView, // will be assigned in top-level.
                    IsPerspectiveEnabled = src.CurrentStatus.IsPerspectiveEnabled,
                    OffsetPosition = src.CurrentStatus.OffsetPosition
                },
                KeyFrames = src.KeyFrames.Extract(src.InitialKeyFrame).Select(p => new DomainModels.Components.CameraKeyFrame()
                {
                    KeyFrameIndex = p.KeyFrameIndex,
                    Position = p.Value.CenterPosition,
                    Rotation = p.Value.Rotation,
                    Interpolation = p.Interpolation,
                    Distance = p.Distance,
                    IsPerspectiveEnabled = p.Value.IsPerspectiveEnabled,
                    AngleOfView = p.AngleOfView,
                    IsSelected = p.IsSelected
                }).ToList()
            };
        }

        public Raw.Components.Project.Camera ConvertBack(DomainModels.Components.Camera src)
        {
            var frames = src.KeyFrames.Select(p => new Raw.Components.Project.CameraKeyFrame()
            {
                KeyFrameIndex = p.KeyFrameIndex,
                AngleOfView = p.AngleOfView,
                Distance = p.Distance,
                Value = new Raw.Components.Project.CameraState()
                {
                    CenterPosition = p.Position,
                    Rotation = p.Rotation,
                    // FollowingBone: will be assigned in top-level.
                    IsPerspectiveEnabled = p.IsPerspectiveEnabled
                },
                Interpolation = p.Interpolation,
                IsSelected = p.IsSelected
            }).ToList();
            var obj = new Raw.Components.Project.Camera()
            {
                CurrentStatus = new Raw.Components.Project.CurrentCameraState()
                {
                    CenterPosition = src.CurrentStatus.Position,
                    Rotation = src.CurrentStatus.Rotation,
                    // AngleOfView: will be assigned in top-level.
                    IsPerspectiveEnabled = src.CurrentStatus.IsPerspectiveEnabled,
                    OffsetPosition = src.CurrentStatus.OffsetPosition
                },
                InitialKeyFrame = frames.Single(p => p.KeyFrameIndex == 0),
                KeyFrames = frames.Where(p => p.KeyFrameIndex > 0).ToList().Pack(1)
            };
            obj.InitialKeyFrame.IsInitialKeyFrame = true;
            obj.InitialKeyFrame.NextDataIndex = frames.Count > 1 ? 1 : 0;
            return obj;
        }
    }

    static class CameraConverterExtensions
    {
        internal static Raw.Components.Project.CameraKeyFrame ToRaw(this DomainModels.Components.CameraKeyFrame src)
        {
            return new Raw.Components.Project.CameraKeyFrame()
            {
                KeyFrameIndex = src.KeyFrameIndex,
                Value = new Raw.Components.Project.CameraState()
                {
                    CenterPosition = src.Position,
                    Rotation = src.Rotation,
                    IsPerspectiveEnabled = src.IsPerspectiveEnabled
                },
                Interpolation = src.Interpolation,
                Distance = src.Distance,
                AngleOfView = src.AngleOfView,
                IsSelected = src.IsSelected
            };
        }
    }
}
