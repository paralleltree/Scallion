using System;
using System.Collections.Generic;
using System.Linq;

using System.Numerics;

namespace Scallion.DomainModels.Components
{
    public class Camera
    {
        public List<CameraKeyFrame> KeyFrames { get; set; }

        public Camera()
        {
            KeyFrames = new List<CameraKeyFrame>();
        }
    }

    public class CameraKeyFrame : KeyFrame
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public CameraInterpolation Interpolation { get; set; }
        public float Distance { get; set; }
        public int AngleOfView { get; set; }
        public bool IsPerspectiveEnabled { get; set; }

        public CameraKeyFrame()
        {
            Interpolation = new CameraInterpolation();
        }
    }
}
