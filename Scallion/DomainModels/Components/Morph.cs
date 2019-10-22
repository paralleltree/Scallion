using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Represents a morph of a model used in MMD.
    /// </summary>
    public class Morph
    {
        /// <summary>
        /// Gets or sets the name of this morph.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a collection of the <see cref="MorphKeyFrame"/> class.
        /// </summary>
        public List<MorphKeyFrame> KeyFrames { get; set; }

        public MorphState CurrentStatus { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Morph"/> class.
        /// </summary>
        public Morph()
        {
            KeyFrames = new List<MorphKeyFrame>();
        }
    }

    public class MorphKeyFrame : KeyFrame<MorphState>
    {
    }

    /// <summary>
    /// Represents a state for a morph.
    /// </summary>
    public class MorphState
    {
        /// <summary>
        /// Gets or sets a value of the morph in this key frame.
        /// </summary>
        public float Weight { get; set; }
    }
}
