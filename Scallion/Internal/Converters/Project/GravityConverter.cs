using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Project
{
    internal class GravityConverter : IConverter<Raw.Components.Project.Gravity, Gravity>
    {
        public DomainModels.Components.Gravity Convert(Raw.Components.Project.Gravity src)
        {
            return new Gravity()
            {
                CurrentStatus = new GravityState()
                {
                    Acceleration = src.CurrentStatus.Acceleration,
                    NoiseAmount = src.CurrentStatus.NoiseAmount,
                    Direction = src.CurrentStatus.Direction,
                    IsNoiseEnabled = src.CurrentStatus.IsNoiseEnabled
                },
                KeyFrames = src.KeyFrames.Extract(src.InitialKeyFrame).Select(p => new GravityKeyFrame()
                {
                    KeyFrameIndex = p.KeyFrameIndex,
                    Acceleration = p.Value.Acceleration,
                    NoiseAmount = p.Value.NoiseAmount,
                    Direction = p.Value.Direction,
                    IsNoiseEnabled = p.Value.IsNoiseEnabled,
                    IsSelected = p.IsSelected
                }).ToList()
            };
        }

        public Raw.Components.Project.Gravity ConvertBack(DomainModels.Components.Gravity src)
        {
            var frames = src.KeyFrames.Select(p => new Raw.Components.Project.GravityKeyFrame()
            {
                KeyFrameIndex = p.KeyFrameIndex,
                Value = new Raw.Components.Project.GravityState()
                {
                    Acceleration = p.Acceleration,
                    NoiseAmount = p.NoiseAmount,
                    Direction = p.Direction,
                    IsNoiseEnabled = p.IsNoiseEnabled
                },
                IsSelected = p.IsSelected
            }).ToList();
            var obj = new Raw.Components.Project.Gravity()
            {
                CurrentStatus = new Raw.Components.Project.GravityState()
                {
                    Acceleration = src.CurrentStatus.Acceleration,
                    NoiseAmount = src.CurrentStatus.NoiseAmount,
                    Direction = src.CurrentStatus.Direction,
                    IsNoiseEnabled = src.CurrentStatus.IsNoiseEnabled
                },
                InitialKeyFrame = frames.Single(p => p.KeyFrameIndex == 0),
                KeyFrames = frames.Where(p => p.KeyFrameIndex > 0).ToList().Pack(1)
            };
            obj.InitialKeyFrame.IsInitialKeyFrame = true;
            obj.InitialKeyFrame.NextDataIndex = frames.Count > 1 ? 1 : 0;

            return obj;
        }
    }
}
