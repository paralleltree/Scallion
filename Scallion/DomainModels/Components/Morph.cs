using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    public class Morph
    {
        public string Name { get; set; }
        public List<MorphKeyFrame> KeyFrames { get; set; }

        public Morph()
        {
            KeyFrames = new List<MorphKeyFrame>();
        }
    }

    public class MorphKeyFrame : KeyFrame
    {
        public float Weight { get; set; }
    }
}
