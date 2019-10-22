using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;

namespace Scallion.Raw.Components.Project
{
    internal class MorphKeyFrame : LinkableKeyFrame<MorphState>
    {
    }

    internal class MorphState : MMDObject
    {
        public float Weight { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.WriteSingle(Weight);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Weight = archive.ReadSingle();
        }
    }
}
