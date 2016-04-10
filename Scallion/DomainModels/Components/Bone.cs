using System;
using System.Collections.Generic;
using System.Linq;

using System.Numerics;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Represents a bone of a model used in MMD.
    /// </summary>
    public class Bone
    {
        /// <summary>
        /// Gets or sets the name of this bone.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a collection of the <see cref="BoneKeyFrame"/> class.
        /// </summary>
        public List<BoneKeyFrame> KeyFrames { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bone"/> class.
        /// </summary>
        public Bone()
        {
            KeyFrames = new List<BoneKeyFrame>();
        }
    }

    /// <summary>
    /// Represents a bone implemented Inverse Kinematics.
    /// </summary>
    public class IKBone : Bone
    {
        /// <summary>
        /// Gets or sets a collection of the <see cref="IKStateKeyFrame"/> class.
        /// </summary>
        public List<IKStateKeyFrame> IKStateKeyFrames { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IKBone"/> class.
        /// </summary>
        public IKBone()
        {
            IKStateKeyFrames = new List<IKStateKeyFrame>();
        }
    }

    /// <summary>
    /// Represents a key frame for a bone.
    /// </summary>
    public class BoneKeyFrame : KeyFrame
    {
        /// <summary>
        /// Gets or sets a position of the bone in this key frame.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets a quaternion of the bone in this key frame.
        /// </summary>
        public Quaternion Quaternion { get; set; }

        /// <summary>
        /// Gets or sets the interpolation in this key frame.
        /// </summary>
        public BoneInterpolation Interpolation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoneKeyFrame"/> class.
        /// </summary>
        public BoneKeyFrame()
        {
            Interpolation = new BoneInterpolation();
        }
    }

    /// <summary>
    /// Represents a key frame indicating whether IK of the bone is enabled.
    /// </summary>
    public class IKStateKeyFrame : KeyFrame
    {
        /// <summary>
        /// Gets or sets a value indicating whether IK of the bone is enabled.
        /// </summary>
        public bool IsIKEnabled { get; set; }
    }
}
