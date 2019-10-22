using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;
using Scallion.DomainModels.Components;

namespace Scallion.Raw.Components
{
    internal abstract class CameraInterpolation : MMDObject
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


        public static implicit operator DomainModels.Components.CameraInterpolation(CameraInterpolation c)
        {
            return new DomainModels.Components.CameraInterpolation()
            {
                X = c.X,
                Y = c.Y,
                Z = c.Z,
                R = c.R,
                D = c.D,
                V = c.V
            };
        }
    }
}
