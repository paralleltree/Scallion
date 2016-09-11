using System;
using System.Collections.Generic;
using System.Linq;

using System.Numerics;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Represents a set of MMD Physics Engine parameters.
    /// </summary>
    public class Physics
    {
        /// <summary>
        /// Gets or sets a instance of the <see cref="Components.Gravity"/> class.
        /// </summary>
        public Gravity Gravity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the type of physics calculation.
        /// </summary>
        public PhysicsMode PhysicsMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the floor for physics is enabled.
        /// </summary>
        public bool IsGroundPhysicsEnabled { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Physics"/> class.
        /// </summary>
        public Physics()
        {
            Gravity = new Gravity();
        }
    }

    /// <summary>
    /// Represents the attraction of gravity.
    /// </summary>
    public class Gravity
    {
        /// <summary>
        /// Gets or sets a collection of the <see cref="GravityKeyFrame"/> class.
        /// </summary>
        public List<GravityKeyFrame> KeyFrames { get; set; }

        /// <summary>
        /// Gets or sets a instance of <see cref="GravityState"/> indicating current gravity status.
        /// </summary>
        public GravityState CurrentStatus { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Gravity"/> class.
        /// </summary>
        public Gravity()
        {
            KeyFrames = new List<GravityKeyFrame>();
            CurrentStatus = new GravityState();
        }
    }

    /// <summary>
    /// Represents a key frame for gravity.
    /// </summary>
    public class GravityKeyFrame : KeyFrame
    {
        /// <summary>
        /// Gets or sets a value indicating gravitational acceleration.
        /// </summary>
        public float Acceleration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the amount of noise for physics calculation.
        /// </summary>
        public int NoiseAmount { get; set; }

        /// <summary>
        /// Gets or sets a direction of gravitational acceleration.
        /// </summary>
        public Vector3 Direction { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the noise is applied.
        /// </summary>
        public bool IsNoiseEnabled { get; set; }


        public static implicit operator GravityState(GravityKeyFrame keyframe)
        {
            return new GravityState()
            {
                Acceleration = keyframe.Acceleration,
                NoiseAmount = keyframe.NoiseAmount,
                Direction = keyframe.Direction,
                IsNoiseEnabled = keyframe.IsNoiseEnabled
            };
        }
    }

    /// <summary>
    /// Represents the current gravity status.
    /// </summary>
    public class GravityState
    {
        /// <summary>
        /// Gets or sets a value indicating gravitational acceleration.
        /// </summary>
        public float Acceleration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the amount of noise for physics calculation.
        /// </summary>
        public int NoiseAmount { get; set; }

        /// <summary>
        /// Gets or sets a direction of gravitational acceleration.
        /// </summary>
        public Vector3 Direction { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the noise is applied.
        /// </summary>
        public bool IsNoiseEnabled { get; set; }
    }
}
