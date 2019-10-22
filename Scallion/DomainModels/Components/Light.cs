using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using System.Numerics;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Represents a light in MMD.
    /// </summary>
    public class Light
    {
        /// <summary>
        /// Gets or sets a instance of <see cref="LightState"/> indicating current lighting status.
        /// </summary>
        public LightState CurrentStatus { get; set; }

        /// <summary>
        /// Gets or sets a collection of the <see cref="LightKeyFrame"/> class.
        /// </summary>
        public List<LightKeyFrame> KeyFrames { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Light"/> class.
        /// </summary>
        public Light()
        {
            CurrentStatus = new LightState();
            KeyFrames = new List<LightKeyFrame>();
        }
    }

    public class LightKeyFrame : KeyFrame<LightState>
    {
    }

    /// <summary>
    /// Represents a state for light configuration.
    /// </summary>
    public class LightState
    {
        /// <summary>
        /// Gets or sets a position of the light.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets a color of the light.
        /// </summary>
        public Color Color { get; set; }
    }
}
