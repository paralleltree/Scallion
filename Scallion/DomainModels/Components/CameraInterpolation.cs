using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    public class CameraInterpolation
    {
        public Interpolation X { get; set; }
        public Interpolation Y { get; set; }
        public Interpolation Z { get; set; }
        public Interpolation R { get; set; }
        public Interpolation D { get; set; }
        public Interpolation V { get; set; }

        public CameraInterpolation()
        {
            X = new Interpolation();
            Y = new Interpolation();
            Z = new Interpolation();
            R = new Interpolation();
            D = new Interpolation();
            V = new Interpolation();
        }
    }
}