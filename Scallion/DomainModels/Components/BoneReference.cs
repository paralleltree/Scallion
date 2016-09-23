using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Represents a reference for the bone of a external model.
    /// </summary>
    public class BoneReference
    {
        /// <summary>
        /// Gets the parent model.
        /// </summary>
        public Model TargetModel { get; }

        /// <summary>
        /// Gets the parent bone of <see cref="TargetModel"/>.
        /// The null value represents the root of model.
        /// </summary>
        public Bone TargetBone { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoneReference"/> class.
        /// </summary>
        public BoneReference()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoneReference"/> class from the model and bone.
        /// </summary>
        /// <param name="model">The parent model</param>
        /// <param name="bone">The parent bone being a part of <paramref name="model"/></param>
        public BoneReference(Model model, Bone bone)
        {
            if (!model.Bones.Contains(bone))
                throw new ArgumentException("The collection of bones of model must contain the bone.");
            TargetModel = model;
            TargetBone = bone;
        }
    }
}
