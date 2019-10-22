using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Project
{
    internal class AccessoryConverter : IConverter<Raw.Components.Project.Accessory, DomainModels.Components.Accessory>
    {
        public DomainModels.Components.Accessory Convert(Raw.Components.Project.Accessory src)
        {
            var obj = new DomainModels.Components.Accessory()
            {
                Index = src.Index,
                Name = src.Name,
                Path = src.Path,
                RenderingOrder = src.RenderingOrder,
                IsAdditiveBlending = src.IsAdditiveBlending,
                CurrentStatus = new AccessoryState()
                {
                    Opacity = src.CurrentStatus.Opacity,
                    IsVisible = src.CurrentStatus.IsVisible,
                    IsShadowEnabled = src.CurrentStatus.IsShadowEnabled,
                    // ExternalParent: will be assigned in top level.
                    Position = src.CurrentStatus.Position,
                    Rotation = src.CurrentStatus.Rotation,
                    Scale = src.CurrentStatus.Scale,
                }
            };
            new AccessoryKeyFrameConverter().Convert(src, obj);
            return obj;
        }

        public Raw.Components.Project.Accessory ConvertBack(DomainModels.Components.Accessory src)
        {
            var obj = new Raw.Components.Project.Accessory()
            {
                Index = (byte)src.Index,
                Name = src.Name,
                Path = src.Path,
                RenderingOrder = src.RenderingOrder,
                IsAdditiveBlending = src.IsAdditiveBlending,
                CurrentStatus = new Raw.Components.Project.AccessoryState()
                {
                    Opacity = src.CurrentStatus.Opacity,
                    IsVisible = src.CurrentStatus.IsVisible,
                    IsShadowEnabled = src.CurrentStatus.IsShadowEnabled,
                    // ExternalParent: will be assigned in top level.
                    Position = src.CurrentStatus.Position,
                    Rotation = src.CurrentStatus.Rotation,
                    Scale = src.CurrentStatus.Scale,
                }
            };
            new AccessoryKeyFrameConverter().ConvertBack(src, obj);
            return obj;
        }
    }
}
