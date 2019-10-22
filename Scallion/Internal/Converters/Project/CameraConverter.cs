using System;
using System.Collections.Generic;
using System.Linq;

using System.Numerics;
using Scallion.DomainModels.Components;
using Scallion.Raw.Components.Project;

namespace Scallion.Internal.Converters.Project
{
    internal class CameraConverter : IConverter<Raw.Components.Project.Camera, DomainModels.Components.Camera>
    {
        private bool IsModelSelected { get; }

        public CameraConverter(bool isModelSelected)
        {
            IsModelSelected = isModelSelected;
        }

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
                    Distance = IsModelSelected ? GetCameraViewDistance(src.CurrentStatus) : -src.CurrentStatus.OffsetPosition.Z
                },
                KeyFrames = src.KeyFrames.Extract(src.InitialKeyFrame).Select(p => new DomainModels.Components.CameraKeyFrame()
                {
                    KeyFrameIndex = p.KeyFrameIndex,
                    Value = new DomainModels.Components.CameraState()
                    {
                        Position = p.Value.CenterPosition,
                        Rotation = p.Value.Rotation,
                        Interpolation = p.Interpolation,
                        Distance = p.Distance,
                        IsPerspectiveEnabled = p.Value.IsPerspectiveEnabled,
                        AngleOfView = p.AngleOfView,
                    },
                    IsSelected = p.IsSelected
                }).ToList()
            };
        }

        public Raw.Components.Project.Camera ConvertBack(DomainModels.Components.Camera src)
        {
            var frames = src.KeyFrames.Select(p => new Raw.Components.Project.CameraKeyFrame()
            {
                KeyFrameIndex = p.KeyFrameIndex,
                AngleOfView = p.Value.AngleOfView,
                Distance = p.Value.Distance,
                Value = new Raw.Components.Project.CameraState()
                {
                    CenterPosition = p.Value.Position,
                    Rotation = p.Value.Rotation,
                    // FollowingBone: will be assigned in top-level.
                    IsPerspectiveEnabled = p.Value.IsPerspectiveEnabled
                },
                Interpolation = p.Value.Interpolation,
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
                    OffsetPosition = IsModelSelected ? GetLocalCameraPosition(src.CurrentStatus) : new Vector3(0, 0, -src.CurrentStatus.Distance)
                },
                InitialKeyFrame = frames.Single(p => p.KeyFrameIndex == 0),
                KeyFrames = frames.Where(p => p.KeyFrameIndex > 0).ToList().Pack(1)
            };
            obj.InitialKeyFrame.IsInitialKeyFrame = true;
            obj.InitialKeyFrame.NextDataIndex = frames.Count > 1 ? 1 : 0;
            return obj;
        }

        private Vector3 GetLocalCameraPosition(DomainModels.Components.CameraState state)
        {
            // カメラのローカル座標系において、ワールド原点を基準にした際の相対位置
            var rotVec = new Vector3(0, 0, 1);
            var q = Quaternion.CreateFromYawPitchRoll(-state.Rotation.Y, state.Rotation.X, -state.Rotation.Z);

            var absPosition = state.Position - Vector3.Transform(rotVec, q) * state.Distance;
            var trans = Matrix4x4.CreateFromQuaternion(q) * Matrix4x4.CreateTranslation(absPosition);
            Matrix4x4.Invert(trans, out Matrix4x4 inv);
            return -Vector3.Transform(Vector3.Zero, inv);
        }

        private float GetCameraViewDistance(Raw.Components.Project.CurrentCameraState state)
        {
            var localPos = state.OffsetPosition;
            var q = Quaternion.CreateFromYawPitchRoll(-state.Rotation.Y, state.Rotation.X, -state.Rotation.Z);
            var trans = Matrix4x4.CreateTranslation(localPos) * Matrix4x4.CreateFromQuaternion(q);
            Matrix4x4.Invert(trans, out Matrix4x4 inv);
            var relativeCenter = Vector3.Transform(state.CenterPosition, inv);
            return relativeCenter.Z;
        }
    }
}
