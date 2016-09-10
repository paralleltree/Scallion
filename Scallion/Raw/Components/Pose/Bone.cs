using System;
using System.Collections.Generic;
using System.Linq;

using System.Numerics;
using Scallion.Core;

namespace Scallion.Raw.Components.Pose
{
    internal class Bone
    {
        public string Name { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Quaternion { get; set; }
    }
}
