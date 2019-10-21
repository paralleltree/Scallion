using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Project
{
    internal class SelfShadowConverter : IConverter<Raw.Components.Project.SelfShadow, SelfShadow>
    {
        public DomainModels.Components.SelfShadow Convert(Raw.Components.Project.SelfShadow src)
        {
            return new SelfShadow()
            {
                IsEnabled = src.IsEnabled,
                CurrentStatus = new SelfShadowState()
                {
                    Distance = src.CurrentStatus.Distance
                },
                KeyFrames = src.KeyFrames.Extract(src.InitialKeyFrame).Select(p => new SelfShadowKeyFrame()
                {
                    KeyFrameIndex = p.KeyFrameIndex,
                    Type = p.SelfShadowType,
                    Distance = p.Value.Distance
                }).ToList()
            };
        }

        public Raw.Components.Project.SelfShadow ConvertBack(DomainModels.Components.SelfShadow src)
        {
            var init = src.KeyFrames.Single(p => p.KeyFrameIndex == 0);
            var frames = src.KeyFrames.Where(p => p.KeyFrameIndex > 0).Select(p => new Raw.Components.Project.SelfShadowKeyFrame()
            {
                SelfShadowType = p.Type,
                Value = new Raw.Components.Project.SelfShadowState()
                {
                    Distance = p.Distance
                },
                IsSelected = p.IsSelected
            }).ToList().Pack(1);

            return new Raw.Components.Project.SelfShadow()
            {
                IsEnabled = src.IsEnabled,
                CurrentStatus = new Raw.Components.Project.SelfShadowState()
                {
                    Distance = src.CurrentStatus.Distance
                },
                InitialKeyFrame = new Raw.Components.Project.SelfShadowKeyFrame()
                {
                    IsInitialKeyFrame = true,
                    NextDataIndex = frames.Count > 0 ? 1 : 0,
                    SelfShadowType = init.Type,
                    Value = new Raw.Components.Project.SelfShadowState()
                    {
                        Distance = init.Distance
                    },
                    IsSelected = init.IsSelected
                },
                KeyFrames = frames
            };
        }
    }
}
