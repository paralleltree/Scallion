using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Project
{
    internal class PhysicsConverter : IInstanceConverter<Raw.Project, DomainModels.Project>
    {
        public DomainModels.Project Convert(Raw.Project src, DomainModels.Project obj)
        {
            obj.Physics = new Physics()
            {
                PhysicsMode = src.PhysicsMode,
                IsGroundPhysicsEnabled = src.IsGroundPhysicsEnabled,
                Gravity = new GravityConverter().Convert(src.Gravity)
            };
            return obj;
        }

        public Raw.Project ConvertBack(DomainModels.Project src, Raw.Project obj)
        {
            obj.PhysicsMode = src.Physics.PhysicsMode;
            obj.IsGroundPhysicsEnabled = src.Physics.IsGroundPhysicsEnabled;
            obj.Gravity = new GravityConverter().ConvertBack(src.Physics.Gravity);

            return obj;
        }
    }
}
