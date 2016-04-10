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
        {
            First = new InterpolationParameter(20, 20);
            Second = new InterpolationParameter(107, 107);
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
