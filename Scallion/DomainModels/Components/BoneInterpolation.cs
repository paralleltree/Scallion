using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    public class BoneInterpolation
    {
        public Interpolation X { get; set; }
        public Interpolation Y { get; set; }
        public Interpolation Z { get; set; }
        public Interpolation R { get; set; }

        public BoneInterpolation()
            : this(new Interpolation(), new Interpolation(), new Interpolation(), new Interpolation())
        {
        }

        public BoneInterpolation(Interpolation x, Interpolation y, Interpolation z, Interpolation r)
        {
            X = x;
            Y = y;
            Z = z;
            R = r;
        }
    }
}
