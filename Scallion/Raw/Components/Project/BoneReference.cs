using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;

namespace Scallion.Raw.Components.Project
{
    internal struct BoneReference
    {
        public int ModelIndex { get; set; }
        public int BoneIndex { get; set; }

        public BoneReference(int modelIndex, int boneIndex)
            : this()
        {
            ModelIndex = modelIndex;
            BoneIndex = boneIndex;
        }
    }
}
