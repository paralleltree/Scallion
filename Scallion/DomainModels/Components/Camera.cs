using System;
using System.Collections.Generic;
using System.Linq;

using System.Numerics;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Represents an camera used in MMD.
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// Gets or sets a collection of the <see cref="CameraKeyFrame"/> class.
        /// </summary>
        public List<CameraKeyFrame> KeyFrames { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        public Camera()
        {
            KeyFrames = new List<CameraKeyFrame>();
        }
    }

    /// <summary>
    /// Represents a key frame for a camera.
    /// </summary>
    public class CameraKeyFrame : KeyFrame
    {
        /// <summary>
        /// Gets or sets the position of the camera in this key frame.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the angle of the camera in this key frame.
        /// </summary>
        public Vector3 Rotation { get; set; }

        /// <summary>
        /// Gets or sets the interpolation in this key frame.
        /// </summary>
        public CameraInterpolation Interpolation { get; set; }

        /// <summary>
        /// Gets or sets the camera distance in this key frame.
        /// </summary>
        /// <remarks>
        /// If this value is negative, the camera is in front of the position.
        /// </remarks>
        public float Distance { get; set; }

        /// <summary>
        /// Gets or sets the angle of view in this key frame.
        /// </summary>
        public int AngleOfView { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether perspective is enabled.
        /// </summary>
        public bool IsPerspectiveEnabled { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraKeyFrame"/> class.
        /// </summary>
        public CameraKeyFrame()
        {
            Interpolation = new CameraInterpolation();
        }
    }
}
