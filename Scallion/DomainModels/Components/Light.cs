using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using System.Numerics;

namespace Scallion.DomainModels.Components
{
    public class Light
    {
        public LinkedList<LightKeyFrame> KeyFrames { get; set; }

        public Light()
        {
            KeyFrames = new LinkedList<LightKeyFrame>();
        }
    }

    public class LightKeyFrame : KeyFrame
    {
        public Vector3 Position { get; set; }
        public Color Color { get; set; }
    }
}
