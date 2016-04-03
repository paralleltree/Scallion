using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    public abstract class KeyFrame
    {
        public int KeyFrameIndex { get; set; }
    }

    public class VisibilityKeyFrame : KeyFrame
    {
        public bool IsVisible { get; set; }
    }
}
