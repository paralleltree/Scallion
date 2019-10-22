using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Project
{
    internal class ViewConverter : IInstanceConverter<Raw.Project, DomainModels.Project>
    {
        public DomainModels.Project Convert(Raw.Project src, DomainModels.Project obj)
        {
            obj.View = new View()
            {
                IsInformationVisible = src.IsInformationVisible,
                IsAxesVisible = src.IsAxesVisible,
                EdgeColor = src.EdgeColor,
                IsBackgroundBlack = src.IsBackgroundBlack,
                FpsLimit = src.FpsLimit == 60f ? FpsLimit.Sixty : src.FpsLimit == 30f ? FpsLimit.Thirty : FpsLimit.None,
                IsSurfaceShadowEnabled = src.IsSurfaceShadowEnabled,
                SurfaceShadowBrightness = src.SurfaceShadowBrightness,
                IsSurfaceShadowTransparent = src.IsSurfaceShadowTransparent,
                AccessoryRenderedAfterModelIndex = src.AccessoryRenderedAfterModelIndex,
                ScreenCapturingMode = src.ScreenCapturingMode
            };

            return obj;
        }

        public Raw.Project ConvertBack(DomainModels.Project src, Raw.Project obj)
        {
            obj.IsInformationVisible = src.View.IsInformationVisible;
            obj.IsAxesVisible = src.View.IsAxesVisible;
            obj.EdgeColor = src.View.EdgeColor;
            obj.IsBackgroundBlack = src.View.IsBackgroundBlack;
            obj.FpsLimit = src.View.FpsLimit == FpsLimit.Sixty ? 60f : src.View.FpsLimit == FpsLimit.Thirty ? 30f : 1000f;
            obj.IsSurfaceShadowEnabled = src.View.IsSurfaceShadowEnabled;
            obj.SurfaceShadowBrightness = src.View.SurfaceShadowBrightness;
            obj.IsSurfaceShadowTransparent = src.View.IsSurfaceShadowTransparent;
            obj.AccessoryRenderedAfterModelIndex = src.View.AccessoryRenderedAfterModelIndex;
            obj.ScreenCapturingMode = src.View.ScreenCapturingMode;

            return obj;
        }
    }
}
