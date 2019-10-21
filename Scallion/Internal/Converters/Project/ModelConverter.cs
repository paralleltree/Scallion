using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Project
{
    internal class ModelConverter : IConverter<Raw.Components.Project.Model, Model>
    {
        public DomainModels.Components.Model Convert(Raw.Components.Project.Model src)
        {
            var obj = new Model()
            {
                Index = src.Index,
                Name = src.Name,
                NameEn = src.NameEn,
                Path = src.Path,
                IsVisible = src.IsVisible,
                IsAdditiveBlending = src.IsAdditiveBlending,
                IsSelfShadowEnabled = src.IsSelfShadowEnabled,
                EdgeWidth = src.EdgeWidth,
                RenderingOrder = src.RenderingOrder,
                CalculationOrder = src.CalculationOrder,
                LastFrameIndex = src.LastFrameIndex,
                SelectedBoneIndex = src.SelectedBoneIndex,
                TimelinePanel = new ModelTimelinePanel()
                {
                    RowsCount = src.TimelinePanelRowsCount,
                    TopRowIndex = src.TimelinePanelTopRowIndex,
                    // RangeSelectorSelectedIndex: will be assigned in top level(ProjectConverter).
                },
                BoneGroupsExpansion = src.BoneGroupsExpansion
            };

            new BoneConverter().Convert(src, obj);
            new MorphConverter().Convert(src, obj);
            // Model keyframes will be converted in top-level.

            return obj;
        }

        public Raw.Components.Project.Model ConvertBack(DomainModels.Components.Model src)
        {
            var obj = new Raw.Components.Project.Model()
            {
                Index = (byte)src.Index,
                Name = src.Name,
                NameEn = src.NameEn,
                Path = src.Path,
                IsVisible = src.IsVisible,
                IsAdditiveBlending = src.IsAdditiveBlending,
                IsSelfShadowEnabled = src.IsSelfShadowEnabled,
                EdgeWidth = src.EdgeWidth,
                RenderingOrder = (byte)src.RenderingOrder,
                CalculationOrder = (byte)src.CalculationOrder,
                LastFrameIndex = src.LastFrameIndex,
                SelectedBoneIndex = src.SelectedBoneIndex,
                TimelinePanelRowsCount = (byte)src.TimelinePanel.RowsCount,
                TimelinePanelTopRowIndex = src.TimelinePanel.TopRowIndex,
                BoneGroupsExpansion = src.BoneGroupsExpansion
            };

            new BoneConverter().ConvertBack(src, obj);
            new MorphConverter().ConvertBack(src, obj);
            // Model keyframes will be converted in top-level.

            obj.MorphPanel = new Raw.Components.Project.MorphSelection()
            {
                EyebrowIndex = -1,
                EyeIndex = -1,
                LipIndex = -1,
                OtherIndex = -1
            };

            return obj;
        }
    }
}
