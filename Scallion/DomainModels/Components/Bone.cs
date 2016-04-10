using System;
using System.Collections.Generic;
using System.Linq;

using System.Numerics;

namespace Scallion.DomainModels.Components
{
    public class Bone
    {
        public string Name { get; set; }
        public List<BoneKeyFrame> KeyFrames { get; set; }

        public Bone()
        {
            KeyFrames = new List<BoneKeyFrame>();
        }
    }

    public class IKBone : Bone
    {
        public List<IKStateKeyFrame> IKStateKeyFrames { get; set; }
        public IKBone()
        {
            IKStateKeyFrames = new List<IKStateKeyFrame>();
        }
    }

    public class BoneKeyFrame : KeyFrame
    {
        public Vector3 Position { get; set; }
        public Quaternion Quaternion { get; set; }
        public BoneInterpolation Interpolation { get; set; }

        public BoneKeyFrame()
        {
            Interpolation = new BoneInterpolation();
        }
    }

    public class IKStateKeyFrame : KeyFrame
    {
        public bool IsIKEnabled { get; set; }
    }
}
