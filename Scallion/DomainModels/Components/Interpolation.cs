using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Represents an interpolating curve that used in a interpolation.
    /// </summary>
    /// <remarks>
    /// In MMD, a interpolation is implemented as a cubic bezier curve that first and last points are fixed as (0, 0), (127, 127).
    /// This class has the second and third points as <see cref="First"/> and <see cref="Second"/> properties for the curve.
    /// </remarks>
    public class Interpolation
    {
        /// <summary>
        /// Gets or sets the first interpolation parameter.
        /// </summary>
        public InterpolationParameter First { get; set; }

        /// <summary>
        /// Gets or sets the second interpolation parameter.
        /// </summary>
        public InterpolationParameter Second { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Interpolation"/> class that defined as linear interpolation.
        /// </summary>
        public Interpolation()
            : this(new InterpolationParameter(20, 20), new InterpolationParameter(107, 107))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Interpolation"/> class with the specified <see cref="InterpolationParameter"/> objects.
        /// </summary>
        /// <param name="first">The instance of the <see cref="InterpolationParameter"/> class for <see cref="First"/> property of this interpolation.</param>
        /// <param name="second">The instance of the <see cref="InterpolationParameter"/> class for <see cref="Second"/> property of this interpolation.</param>
        public Interpolation(InterpolationParameter first, InterpolationParameter second)
        {
            First = first;
            Second = second;
        }

        /// <summary>
        /// Get interpolated value using this interpolation.
        /// </summary>
        /// <param name="pos">The parameter moving from 0 to 1</param>
        /// <returns>The interpolated value moving from 0 to 1</returns>
        public float GetInterpolatedValue(float pos)
        {
            if (pos < 0 || pos > 1) throw new ArgumentOutOfRangeException("pos");
            float f(float t, float p2, float p3) => 3 * (1 - t) * (1 - t) * t * p2 + 3 * (1 - t) * t * t * p3 + t * t * t;
            float x2 = First.X / 127f;
            float x3 = Second.X / 127f;
            float upper = 1f;
            float lower = 0;
            for (int i = 0; i < 16; i++)
            {
                float x = (upper + lower) / 2;
                float d = f(x, x2, x3) - pos;
                if (Math.Abs(d) < 1e-6) break;
                if (d < 0) lower = x;
                else upper = x;
            }
            return f((upper + lower) / 2, First.Y / 127f, Second.Y / 127f);
        }
    }

    /// <summary>
    /// Represents a point used for interpolation.
    /// </summary>
    public struct InterpolationParameter
    {
        /// <summary>
        /// Gets or sets a value of the x-coordinate(time axis) of this parameter.
        /// </summary>
        public byte X { get; set; }

        /// <summary>
        /// Gets or sets a value of the y-coordinate(value axis) of this parameter.
        /// </summary>
        public byte Y { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="InterpolationParameter"/> class with the specified coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate(time axis) of this parameter</param>
        /// <param name="y">The y-coordinate(value axis) of this parameter</param>
        public InterpolationParameter(byte x, byte y)
            : this()
        {
            X = x;
            Y = y;
        }
    }
}
