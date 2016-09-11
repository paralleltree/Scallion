using System;
using System.Collections.Generic;
using System.Linq;

using System.Numerics;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Represents a MMD Accessory object.
    /// </summary>
    public class Accessory
    {
        /// <summary>
        /// Gets or sets the index of the accessory.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the name of the accessory.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the path of the accessory file.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the index indicating the order of rendering.
        /// </summary>
        public byte RenderingOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the additive blending is enabled.
        /// </summary>
        public bool IsAdditiveBlending { get; set; }

        /// <summary>
        /// Gets or sets a collection of the <see cref="AccessoryKeyFrame"/> class.
        /// </summary>
        public List<AccessoryKeyFrame> KeyFrames { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Accessory"/> class.
        /// </summary>
        public Accessory()
        {
            KeyFrames = new List<AccessoryKeyFrame>();
        }
    }

    /// <summary>
    /// Represents a key frame for <see cref="Accessory"/>.
    /// </summary>
    public class AccessoryKeyFrame : KeyFrame
    {
        /// <summary>
        /// Gets or sets the opacity factor applied to the accessory.
        /// </summary>
        public float Opacity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the accessory is visible.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the reference for external parent bone.
        /// </summary>
        public BoneReference ExternalParent { get; set; }

        /// <summary>
        /// Gets or sets the position of the accessory.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the angle of rotation.
        /// </summary>
        public Vector3 Rotation { get; set; }

        /// <summary>
        /// Gets or sets the scale factor applied to the accessory.
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the shadowing is enabled.
        /// </summary>
        public bool IsShadowEnabled { get; set; }
    }
}
