using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Represents an interpolation used for a camera key frame.
    /// </summary>
    public class CameraInterpolation
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
        /// Gets or sets an interpolation value for a camera distance.
        /// </summary>
        public Interpolation D { get; set; }

        /// <summary>
        /// Gets or sets an interpolation value for a angle of view.
        /// </summary>
        public Interpolation V { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraInterpolation"/> class.
        /// </summary>
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