using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace Scallion.Tests.Internal
{
    internal static class NumericsExtensions
    {
        public static Vector3 ToRad(this Vector3 deg)
        {
            return new Vector3((float)Math.PI * deg.X / 180, (float)Math.PI * deg.Y / 180, (float)Math.PI * deg.Z / 180);
        }

        public static Vector3 ToDeg(this Vector3 rad)
        {
            return new Vector3(rad.X * 180 / (float)Math.PI, rad.Y * 180 / (float)Math.PI, rad.Z * 180 / (float)Math.PI);
        }
    }
}
