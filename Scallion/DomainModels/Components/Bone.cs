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
        /// Gets or sets a instance of <see cref="BoneState"/> indicating current bone status.
        /// </summary>
        public BoneState CurrentStatus { get; set; }

        /// <summary>
        /// Gets or sets a collection of the <see cref="ExternalParentKeyFrame"/> class.
        /// </summary>
        public List<ExternalParentKeyFrame> ExternalParentKeyFrames { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bone"/> class.
        /// </summary>
        public Bone()
        {
            CurrentStatus = new BoneState();
            KeyFrames = new List<BoneKeyFrame>();
            ExternalParentKeyFrames = new List<ExternalParentKeyFrame>();
        }
    }

    /// <summary>
    /// Represents a bone implemented Inverse Kinematics.
    /// </summary>
    public class IKBone : Bone
    {
        /// <summary>
        /// Gets or sets the current IK status.
        /// </summary>
        public IKBoneState CurrentIKStatus { get; set; }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="IKBone"/> from the specified <see cref="Bone"/>.
        /// </summary>
        /// <param name="bone">The source bone</param>
        public IKBone(Bone bone) : this()
        {
            Name = bone.Name;
            KeyFrames = bone.KeyFrames;
            CurrentStatus = bone.CurrentStatus;
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
        /// Gets or sets a value indicating whether the physics calculation for the bone is enabled.
        /// </summary>
        public bool IsPhysicsEnabled { get; set; }

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

    /// <summary>
    /// Represents the current bone status.
    /// </summary>
    public class BoneState
    {
        /// <summary>
        /// Gets or sets the position of the accessory.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the quaternion of the accessory.
        /// </summary>
        public Quaternion Quaternion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bone state is saved.
        /// </summary>
        public bool IsSaved { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the physics calculation for the bone is enabled.
        /// </summary>
        public bool IsPhysicsEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bone is selected on the timeline panel.
        /// </summary>
        public bool IsRowSelected { get; set; }

        /// <summary>
        /// Gets or sets the reference for external parent bone.
        /// </summary>
        public BoneReference ExternalParent { get; set; }
    }

    /// <summary>
    /// Represents the current IK bone status.
    /// </summary>
    public class IKBoneState
    {
        /// <summary>
        /// Gets or sets a value indicating whether IK of the bone is enabled.
        /// </summary>
        public bool IsIKEnabled { get; set; }
    }
}
