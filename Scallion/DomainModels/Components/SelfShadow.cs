using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    public class SelfShadow
    {
        public LinkedList<SelfShadowKeyFrame> KeyFrames { get; set; }

        public SelfShadow()
        {
            KeyFrames = new LinkedList<SelfShadowKeyFrame>();
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
