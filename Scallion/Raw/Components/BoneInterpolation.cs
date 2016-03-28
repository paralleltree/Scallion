using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;
using Scallion.DomainModels.Components;

namespace Scallion.Raw.Components
{
    internal abstract class BoneInterpolation : MMDObject
    {
        public Interpolation X { get; set; }
        public Interpolation Y { get; set; }
        public Interpolation Z { get; set; }
        public Interpolation R { get; set; }

        public BoneInterpolation()
        {
            X = new Interpolation();
            Y = new Interpolation();
            Z = new Interpolation();
            R = new Interpolation();
        }


        public static implicit operator DomainModels.Components.BoneInterpolation(BoneInterpolation b)
        {
            return new DomainModels.Components.BoneInterpolation()
            {
                X = b.X,
                Y = b.Y,
                Z = b.Z,
                R = b.R
            };
        }
    }
}
