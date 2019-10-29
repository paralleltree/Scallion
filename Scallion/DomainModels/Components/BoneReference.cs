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
        /// Gets the parent bone index of <see cref="TargetModel"/>.
        /// The value "-1" represents the root of model.
        /// </summary>
        public int TargetBoneIndex { get; } = -1;

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
        /// <param name="boneIndex">The parent bone index of <paramref name="model"/></param>
        public BoneReference(Model model, int boneIndex)
        {
            if (boneIndex < -1 || boneIndex >= model.Bones.Count)
                throw new ArgumentException("The collection of bones of model must contain the bone.");
            TargetModel = model;
            TargetBoneIndex = boneIndex;
        }
    }
}
