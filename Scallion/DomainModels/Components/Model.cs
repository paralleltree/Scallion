using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Represents MMD Model object.
    /// </summary>
    public class Model
    {
        /// <summary>
        /// Gets or sets the index of the model.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the name of the model.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the model in English.
        /// </summary>
        public string NameEn { get; set; }

        /// <summary>
        /// Gets or sets the path of the model file.
        /// </summary>
        public string Path { get; set; }

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
        /// Gets or sets a collection of the <see cref="VisibilityKeyFrame"/> class for this model.
        /// </summary>
        public List<VisibilityKeyFrame> VisibilityKeyFrames { get; set; }

        /// <summary>
        /// Gets or sets a collection of the <see cref="Morph"/> class.
        /// </summary>
        public List<Morph> Morphs { get; set; }

        /// <summary>
        /// Gets or sets the index indicating the order of rendering starting from 1.
        /// </summary>
        public int RenderingOrder { get; set; }

        /// <summary>
        /// Gets or sets the index indicating the order of calculation.
        /// </summary>
        public int CalculationOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the model is visible.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the index of the selected bone.
        /// </summary>
        public int SelectedBoneIndex { get; set; }

        /// <summary>
        /// Gets or sets the index of last frame.
        /// </summary>
        public int LastFrameIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the additive blending is enabled.
        /// </summary>
        public bool IsAdditiveBlending { get; set; }

        /// <summary>
        /// Gets or sets the width of edges.
        /// </summary>
        public float EdgeWidth { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether self-shadowing is enabled.
        /// </summary>
        public bool IsSelfShadowEnabled { get; set; }

        /// <summary>
        /// Gets or sets an instance of the <see cref="ModelTimelinePanel"/> class.
        /// </summary>
        public ModelTimelinePanel TimelinePanel { get; set; }

        /// <summary>
        /// Gets or sets the collection of bone groups expansion status.
        /// </summary>
        public List<bool> BoneGroupsExpansion { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        public Model()
        {
            Bones = new List<Bone>();
            Morphs = new List<Morph>();
            VisibilityKeyFrames = new List<VisibilityKeyFrame>();
            TimelinePanel = new ModelTimelinePanel();
        }
    }
}
