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
        /// Gets or sets a collection of the <see cref="LightKeyFrame"/> class.
        /// </summary>
        public List<LightKeyFrame> KeyFrames { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Light"/> class.
        /// </summary>
        public Light()
        {
            KeyFrames = new List<LightKeyFrame>();
        }
    }

    /// <summary>
    /// Represents a key frame for a light configuration.
    /// </summary>
    public class LightKeyFrame : KeyFrame
    {
        /// <summary>
        /// Gets or sets a position of the light in this key frame.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets a color of the light in this key frame.
        /// </summary>
        public Color Color { get; set; }
    }
}
