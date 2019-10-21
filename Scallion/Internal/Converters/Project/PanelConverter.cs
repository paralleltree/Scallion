using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Project
{
    class PanelConverter : IInstanceConverter<Raw.Project, DomainModels.Project>
    {
        public DomainModels.Project Convert(Raw.Project src, DomainModels.Project obj)
        {
            obj.Panel = new Panel()
            {
                IsModelSelected = src.IsModelSelected,
                SelectedModelIndex = src.SelectedModelIndex,
                SelectedAccessoryIndex = src.SelectedAccessoryIndex,
                FrameJumpingBoxValue = src.FrameJumpingBoxValue,
                BoneSelectionType = src.BoneSelectionType,
                PreviewPanel = new PreviewPanel()
                {
                    StartFrameIndex = src.PreviewPanel.StartFrameIndex,
                    EndFrameIndex = src.PreviewPanel.EndFrameIndex,
                    IsRepeating = src.PreviewPanel.IsRepeating,
                    IsStartingFromCurrentFrame = src.PreviewPanel.IsStartingFromCurrentFrame,
                    IsStayingAtStoppedFrame = src.PreviewPanel.IsStayingAtStoppedFrame
                },
                ViewPanel = new ViewPanel()
                {
                    IsFollowingViewEnabled = src.IsFollowingViewEnabled,
                    CameraFollowingType = src.CameraFollowingType
                },
                TimelinePanel = new TimelinePanel()
                {
                    PanelWidth = src.TimelinePanelWidth,
                    CurrentFrameIndex = src.TimelinePanelStatus.CurrentFrameIndex,
                    HorizontalHeadFrameIndex = src.TimelinePanelStatus.HorizontalHeadFrameIndex,
                    HorizontalScale = src.TimelinePanelStatus.HorizontalScale,
                    TopAccessoryIndex = src.TimelinePanelTopAccessoryIndex
                },
                PanelExpansion = new PanelExpansion()
                {
                    Camera = src.PanelExpansion.Camera,
                    Light = src.PanelExpansion.Light,
                    Accessory = src.PanelExpansion.Accessory,
                    Bone = src.PanelExpansion.Bone,
                    Morph = src.PanelExpansion.Morph,
                    SelfShadow = src.PanelExpansion.SelfShadow
                }
            };

            return obj;
        }

        public Raw.Project ConvertBack(DomainModels.Project src, Raw.Project obj)
        {
            obj.IsModelSelected = src.Panel.IsModelSelected;
            obj.SelectedModelIndex = (byte)src.Panel.SelectedModelIndex;
            obj.SelectedAccessoryIndex = (byte)src.Panel.SelectedAccessoryIndex;
            obj.FrameJumpingBoxValue = src.Panel.FrameJumpingBoxValue;
            obj.BoneSelectionType = src.Panel.BoneSelectionType;

            obj.PreviewPanel = new Raw.Components.Project.PreviewPanel()
            {
                StartFrameIndex = src.Panel.PreviewPanel.StartFrameIndex,
                EndFrameIndex = src.Panel.PreviewPanel.EndFrameIndex,
                IsRepeating = src.Panel.PreviewPanel.IsRepeating,
                IsStartingFromCurrentFrame = src.Panel.PreviewPanel.IsStartingFromCurrentFrame,
                IsStayingAtStoppedFrame = src.Panel.PreviewPanel.IsStayingAtStoppedFrame
            };

            obj.IsFollowingViewEnabled = src.Panel.ViewPanel.IsFollowingViewEnabled;
            obj.CameraFollowingType = src.Panel.ViewPanel.CameraFollowingType;

            obj.TimelinePanelWidth = src.Panel.TimelinePanel.PanelWidth;
            obj.TimelinePanelStatus = new Raw.Components.Project.TimelinePanelState()
            {
                CurrentFrameIndex = src.Panel.TimelinePanel.CurrentFrameIndex,
                HorizontalHeadFrameIndex = src.Panel.TimelinePanel.HorizontalHeadFrameIndex,
                HorizontalScale = src.Panel.TimelinePanel.HorizontalScale
            };
            obj.TimelinePanelTopAccessoryIndex = src.Panel.TimelinePanel.TopAccessoryIndex;

            obj.PanelExpansion = new Raw.Components.Project.PanelExpansion()
            {
                Camera = src.Panel.PanelExpansion.Camera,
                Light = src.Panel.PanelExpansion.Light,
                Accessory = src.Panel.PanelExpansion.Accessory,
                Bone = src.Panel.PanelExpansion.Bone,
                Morph = src.Panel.PanelExpansion.Morph,
                SelfShadow = src.Panel.PanelExpansion.SelfShadow
            };

            return obj;
        }
    }
}
