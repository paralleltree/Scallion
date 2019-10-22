using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Represents a key frame used for transition.
    /// This class is abstract.
    /// </summary>
    public class KeyFrame<T> where T : new()
    {
        /// <summary>
        /// Gets or sets the index of this key frame.
        /// </summary>
        public int KeyFrameIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the key frame is selected.
        /// </summary>
        public bool IsSelected { get; set; }

        public T Value { get; set; }

        public KeyFrame()
        {
            Value = new T();
        }
    }

    public class VisibilityKeyFrame : KeyFrame<VisibilityState>
    {
    }

    /// <summary>
    /// Represents a key frame that indicates whether a object is visible.
    /// </summary>
    public class VisibilityState
    {
        /// <summary>
        /// Gets or sets a value indicating whether the object is visible.
        /// </summary>
        public bool IsVisible { get; set; }
    }

    public class ExternalParentKeyFrame : KeyFrame<ExternalParentState>
    {
    }

    /// <summary>
    /// Represents a key frame that indicates the external parent bone.
    /// </summary>
    public class ExternalParentState
    {
        /// <summary>
        /// Gets or sets the reference to the external parent bone.
        /// </summary>
        public BoneReference Reference { get; set; }
    }
}
