using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Represents a self shadow configuration used in MMD.
    /// </summary>
    public class SelfShadow
    {
        /// <summary>
        /// Gets or sets a collection of the <see cref="SelfShadowKeyFrame"/> class.
        /// </summary>
        public List<SelfShadowKeyFrame> KeyFrames { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfShadow"/> class.
        /// </summary>
        public SelfShadow()
        {
            KeyFrames = new List<SelfShadowKeyFrame>();
        }
    }

    /// <summary>
    /// Represents a key frame for a self shadow configuration.
    /// </summary>
    public class SelfShadowKeyFrame : KeyFrame
    {
        /// <summary>
        /// Gets or sets a type of self-shadowing methods in <see cref="SelfShadowType"/>.
        /// </summary>
        public SelfShadowType Type { get; set; }

        /// <summary>
        /// Gets or sets the shadow distance.
        /// </summary>
        public int Distance { get; set; }
    }

    /// <summary>
    /// Specifies the method of self-shadowing.
    /// </summary>
    public enum SelfShadowType
    {
        /// <summary>
        /// Do not use self-shadowing.
        /// </summary>
        Off,

        /// <summary>
        /// Use Mode1 method that renders shadows averagely.
        /// </summary>
        Mode1,

        /// <summary>
        /// Use Mode2 method that renders shadows prioritized close-range view.
        /// </summary>
        Mode2
    }
}
