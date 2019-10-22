using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;
using Scallion.Internal.Converters.Project;

namespace Scallion.Internal.Converters.Project
{
    internal class LightConverter : IConverter<Raw.Components.Project.Light, Light>
    {
        public DomainModels.Components.Light Convert(Raw.Components.Project.Light src)
        {
            return new Light()
            {
                CurrentStatus = new LightState()
                {
                    Position = src.CurrentStatus.Position,
                    Color = src.CurrentStatus.Color
                },
                KeyFrames = src.KeyFrames.Extract(src.InitialKeyFrame).Select(p => new LightKeyFrame()
                {
                    KeyFrameIndex = p.KeyFrameIndex,
                    Value = new LightState()
                    {
                        Position = p.Value.Position,
                        Color = p.Value.Color,
                    },
                    IsSelected = p.IsSelected
                }).ToList()
            };
        }

        public Raw.Components.Project.Light ConvertBack(DomainModels.Components.Light src)
        {
            var init = src.KeyFrames.Single(p => p.KeyFrameIndex == 0);
            var frames = src.KeyFrames.Where(p => p.KeyFrameIndex > 0).Select(p => new Raw.Components.Project.LightKeyFrame()
            {
                Value = new Raw.Components.Project.LightState()
                {
                    Position = p.Value.Position,
                    Color = p.Value.Color
                },
                IsSelected = p.IsSelected
            }).ToList().Pack(1);

            return new Raw.Components.Project.Light()
            {
                CurrentStatus = new Raw.Components.Project.LightState()
                {
                    Position = src.CurrentStatus.Position,
                    Color = src.CurrentStatus.Color
                },
                InitialKeyFrame = new Raw.Components.Project.LightKeyFrame()
                {
                    IsInitialKeyFrame = true,
                    NextDataIndex = frames.Count > 0 ? 1 : 0,
                    Value = new Raw.Components.Project.LightState()
                    {
                        Position = init.Value.Position,
                        Color = init.Value.Color
                    },
                    IsSelected = init.IsSelected
                },
                KeyFrames = frames
            };
        }
    }
}
