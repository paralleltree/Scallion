using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    public class SelfShadow
    {
        public List<SelfShadowKeyFrame> KeyFrames { get; set; }

        public SelfShadow()
        {
            KeyFrames = new List<SelfShadowKeyFrame>();
        }
    }

    public class SelfShadowKeyFrame : KeyFrame
    {
        public SelfShadowType Type { get; set; }
        public int Distance { get; set; }
    }

    public enum SelfShadowType
    {
        Off,
        Mode1,
        Mode2
    }
}
