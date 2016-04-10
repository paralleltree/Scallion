using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Represents an interpolation used for a bone key frame.
    /// </summary>
    public class BoneInterpolation
    {
        /// <summary>
        /// Gets or sets an interpolation value for x-direction.
        /// </summary>
        public Interpolation X { get; set; }

        /// <summary>
        /// Gets or sets an interpolation value for y-direction.
        /// </summary>
        public Interpolation Y { get; set; }

        /// <summary>
        /// Gets or sets an interpolation value for z-direction.
        /// </summary>
        public Interpolation Z { get; set; }

        /// <summary>
        /// Gets or sets an interpolation value for rotation.
        /// </summary>
        public Interpolation R { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoneInterpolation"/> class.
        /// </summary>
        public BoneInterpolation()
            : this(new Interpolation(), new Interpolation(), new Interpolation(), new Interpolation())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoneInterpolation"/> class that contains elements copied from specified value.
        /// </summary>
        /// <param name="x">The interploation value for x-direction</param>
        /// <param name="y">The interpolation value for y-direction</param>
        /// <param name="z">The interpolation value for z-direction</param>
        /// <param name="r">The interpolation value for rotation</param>
        public BoneInterpolation(Interpolation x, Interpolation y, Interpolation z, Interpolation r)
        {
            X = x;
            Y = y;
            Z = z;
            R = r;
        }
    }
}
