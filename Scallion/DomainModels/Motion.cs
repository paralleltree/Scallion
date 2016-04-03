using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;
using Scallion.DomainModels.Components;
using Scallion.Internal.Converters.Motion;

namespace Scallion.DomainModels
{
    public class Motion : IMMDFile<Motion>
    {
        public string ModelName { get; set; }
        public List<Bone> Bones { get; set; }
        public IEnumerable<IKBone> IKBones
        {
            get { return Bones.Where(p => p is IKBone).Cast<IKBone>(); }
        }
        public List<Morph> Morphs { get; set; }
        public Camera Camera { get; set; }
        public Light Light { get; set; }
        public SelfShadow SelfShadow { get; set; }
        public LinkedList<VisibilityKeyFrame> VisibilityKeyFrames { get; set; }


        public Motion()
        {
            VisibilityKeyFrames = new LinkedList<VisibilityKeyFrame>();
        }

        public Motion Load(string path)
        {
            return new MotionConverter().Convert(new Raw.Motion().Load(path), this);
        }

        public void Save(string path)
        {
            new MotionConverter().ConvertBack(this, new Raw.Motion()).Save(path);
        }
    }
}
