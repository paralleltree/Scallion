using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;
using Scallion.DomainModels.Components;
using Scallion.Internal.Converters.Motion;

namespace Scallion.DomainModels
{
    /// <summary>
    /// Represents MMD Motion File(.vmd).
    /// </summary>
    public class Motion : IMMDFile<Motion>
    {
        /// <summary>
        /// Gets or sets the name of a model which the motion made for.
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// Gets or sets a collection of the <see cref="Bone"/> class.
        /// </summary>
        public List<Bone> Bones { get; set; }

        /// <summary>
        /// Gets a collection of the <see cref="IKBone"/> class in <see cref="Bones"/> property.
        /// </summary>
        public IEnumerable<IKBone> IKBones
        {
            get { return Bones.Where(p => p is IKBone).Cast<IKBone>(); }
        }

        /// <summary>
        /// Gets or sets a collection of the <see cref="Morph"/> class.
        /// </summary>
        public List<Morph> Morphs { get; set; }

        /// <summary>
        /// Gets or sets a instance of the <see cref="Components.Camera"/> class for this motion.
        /// </summary>
        public Camera Camera { get; set; }

        /// <summary>
        /// Gets or sets a instance of the <see cref="Components.Light"/> class for this motion.
        /// </summary>
        public Light Light { get; set; }

        /// <summary>
        /// Gets or sets a instance of the <see cref="Components.SelfShadow"/> class for this motion.
        /// </summary>
        public SelfShadow SelfShadow { get; set; }

        /// <summary>
        /// Gets or sets a collection of the <see cref="VisibilityKeyFrame"/> class for this motion.
        /// </summary>
        public List<VisibilityKeyFrame> VisibilityKeyFrames { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Motion"/> class.
        /// </summary>
        public Motion()
        {
            VisibilityKeyFrames = new List<VisibilityKeyFrame>();
        }

        /// <summary>
        /// Loads a motion from the specified file.
        /// </summary>
        /// <param name="path">The file path to load a motion</param>
        /// <returns>The self instance assigned values from file</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the specified file is invalid or unsupported file.
        /// </exception>
        public Motion Load(string path)
        {
            return new MotionConverter().Convert(new Raw.Motion().Load(path), this);
        }

        /// <summary>
        /// Saves this motion to the specified file.
        /// </summary>
        /// <param name="path">The file path to save this motion</param>
        public void Save(string path)
        {
            new MotionConverter().ConvertBack(this, new Raw.Motion()).Save(path);
        }
    }
}
