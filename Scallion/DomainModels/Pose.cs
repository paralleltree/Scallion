using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;
using Scallion.DomainModels.Components;

namespace Scallion.DomainModels
{
    /// <summary>
    /// Represents MMD Pose File(.vpd).
    /// </summary>
    public class Pose : IMMDFile<Pose>
    {
        /// <summary>
        /// Gets or sets the name of a model which the pose made for.
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// Gets or sets a value of <see cref="BoneKeyFrame"/> associated with the bone name.
        /// <see cref="BoneKeyFrame.Interpolation"/> and keyframe data in these values will be ignored.
        /// </summary>
        public Dictionary<string, BoneKeyFrame> Bones { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pose"/> class.
        /// </summary>
        public Pose()
        {
            Bones = new Dictionary<string, BoneKeyFrame>();
        }

        /// <summary>
        /// Loads a pose from the specified file.
        /// </summary>
        /// <param name="path">The file path to load a pose</param>
        /// <returns>The self instance assigned values from the file</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the specified file is invalid or unsupported file.
        /// </exception>
        public Pose Load(string path)
        {
            var raw = new Raw.Pose().Load(path);
            ModelName = raw.ModelName;
            Bones = raw.Bones.ToDictionary(p => p.Name, p => new BoneKeyFrame()
            {
                Position = p.Position,
                Quaternion = p.Quaternion
            });
            return this;
        }

        /// <summary>
        /// Saves this pose to the specified file.
        /// </summary>
        /// <param name="path">The file path to save this pose</param>
        public void Save(string path)
        {
            new Raw.Pose()
            {
                ModelName = this.ModelName,
                Bones = Bones.Select(p => new Raw.Components.Pose.Bone()
                {
                    Name = p.Key,
                    Position = p.Value.Position,
                    Quaternion = p.Value.Quaternion
                }).ToList()
            }.Save(path);
        }
    }
}
