using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    public class Interpolation
    {
        public InterpolationParameter First { get; set; }
        public InterpolationParameter Second { get; set; }

        public Interpolation()
        {
            First = new InterpolationParameter(20, 20);
            Second = new InterpolationParameter(107, 107);
        }
    }

    public struct InterpolationParameter
    {
        public byte X { get; set; }
        public byte Y { get; set; }

        public InterpolationParameter(byte x, byte y)
            : this()
        {
            X = x;
            Y = y;
        }
    }
}
