using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Project
{
    internal class AccessoryKeyFrameConverter : IInstanceConverter<Raw.Components.Project.Accessory, Accessory>
    {
        public DomainModels.Components.Accessory Convert(Raw.Components.Project.Accessory src, DomainModels.Components.Accessory obj)
        {
            obj.KeyFrames = src.KeyFrames.Extract(src.InitialKeyFrame).Select(p => new AccessoryKeyFrame()
            {
                KeyFrameIndex = p.KeyFrameIndex,
                Opacity = p.Value.Opacity,
                IsVisible = p.Value.IsVisible,
                IsShadowEnabled = p.Value.IsShadowEnabled,
                // ExternalParent: will be assgined in top level.
                Position = p.Value.Position,
                Rotation = p.Value.Rotation,
                Scale = p.Value.Scale
            }).ToList();

            return obj;
        }

        public Raw.Components.Project.Accessory ConvertBack(DomainModels.Components.Accessory src, Raw.Components.Project.Accessory obj)
        {
            var frames = src.KeyFrames.Select(p => new Raw.Components.Project.AccessoryKeyFrame()
            {
                KeyFrameIndex = p.KeyFrameIndex,
                Value = new Raw.Components.Project.AccessoryState()
                {
                    Opacity = p.Opacity,
                    IsVisible = p.IsVisible,
                    IsShadowEnabled = p.IsShadowEnabled,
                    // ExternalParent: will be assign in top level.
                    Position = p.Position,
                    Rotation = p.Rotation,
                    Scale = p.Scale
                },
                IsSelected = p.IsSelected
            }).ToList();
            obj.InitialKeyFrame = frames.Single(p => p.KeyFrameIndex == 0);
            obj.InitialKeyFrame.IsInitialKeyFrame = true;
            obj.InitialKeyFrame.NextDataIndex = frames.Count > 1 ? 1 : 0;
            obj.KeyFrames = frames.Where(p => p.KeyFrameIndex > 0).ToList().Pack(1);

            return obj;
        }
    }
}
