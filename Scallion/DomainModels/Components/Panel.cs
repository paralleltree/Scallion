using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Represents User Interface of MMD.
    /// </summary>
    public class Panel
    {
        /// <summary>
        /// Gets or sets a value indicating whether any model is selected.
        /// </summary>
        public bool IsModelSelected { get; set; }

        /// <summary>
        /// Gets or sets the index of the selected model.
        /// </summary>
        public int SelectedModelIndex { get; set; }

        /// <summary>
        /// Gets or sets the index of the selected accessory.
        /// </summary>
        public int SelectedAccessoryIndex { get; set; }

        /// <summary>
        /// Gets or sets the index of frame in the box.
        /// </summary>
        public int FrameJumpingBoxValue { get; set; }

        /// <summary>
        /// Gets or sets an instance of the <see cref="Components.PreviewPanel"/> class.
        /// </summary>
        public PreviewPanel PreviewPanel { get; set; }

        /// <summary>
        /// Gets or sets an instance of the <see cref="Components.ViewPanel"/> class.
        /// </summary>
        public ViewPanel ViewPanel { get; set; }

        /// <summary>
        /// Gets or sets an instance of the <see cref="Components.TimelinePanel"/> class.
        /// </summary>
        public TimelinePanel TimelinePanel { get; set; }

        /// <summary>
        /// Gets or sets the expansion status of each control panel.
        /// </summary>
        public PanelExpansion PanelExpansion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the way of selecting bone(s).
        /// </summary>
        public BoneSelectionType BoneSelectionType { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Panel"/> class.
        /// </summary>
        public Panel()
        {
            PreviewPanel = new PreviewPanel();
            ViewPanel = new ViewPanel();
            TimelinePanel = new TimelinePanel();
            PanelExpansion = new PanelExpansion();
        }
    }

    /// <summary>
    /// Represents a preview panel.
    /// </summary>
    public class PreviewPanel
    {
        /// <summary>
        /// Gets or sets a value indicating whether repeating is enabled on previewing.
        /// </summary>
        public bool IsRepeating { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether previewing starts from current frame.
        /// </summary>
        public bool IsStartingFromCurrentFrame { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the key frame position stays at end of the range after previewing finished.
        /// </summary>
        public bool IsStayingAtStoppedFrame { get; set; }

        /// <summary>
        /// Gets or sets the index of the frame beginning of previewing range.
        /// </summary>
        public int StartFrameIndex { get; set; }

        /// <summary>
        /// Gets or sets the index of the frame ending of previewing range.
        /// </summary>
        public int EndFrameIndex { get; set; }
    }

    /// <summary>
    /// Represents a view panel.
    /// </summary>
    public class ViewPanel
    {
        /// <summary>
        /// Gets or sets a value indicating whether the camera follows any target.
        /// </summary>
        public bool IsFollowingViewEnabled { get; set; }

        /// <summary>
        /// Gets or sets the way of following bone.
        /// </summary>
        public CameraFollowingType CameraFollowingType { get; set; }
    }

    /// <summary>
    /// Represents a timeline panel.
    /// </summary>
    public class TimelinePanel
    {
        /// <summary>
        /// Gets or sets the width of the timeline panel.
        /// </summary>
        public int PanelWidth { get; set; }

        /// <summary>
        /// Gets or sets the index of the accessory that is on the top of timeline panel.
        /// </summary>
        public int TopAccessoryIndex { get; set; }

        /// <summary>
        /// Gets or sets the index of the current frame.
        /// </summary>
        public int CurrentFrameIndex { get; set; }

        /// <summary>
        /// Gets or sets the index of the head frame on timeline panel.
        /// </summary>
        public int HorizontalHeadFrameIndex { get; set; }

        /// <summary>
        /// Gets or sets the magnification of the horizontal scroll bar.
        /// </summary>
        public int HorizontalScale { get; set; }
    }

    /// <summary>
    /// Represents a timeline panel for each model.
    /// </summary>
    public class ModelTimelinePanel
    {
        /// <summary>
        /// Gets or sets the number of rows on the model timeline panel.
        /// </summary>
        public int RowsCount { get; set; }

        /// <summary>
        /// Gets or sets the index of the row being on the top of the panel.
        /// </summary>
        public int TopRowIndex { get; set; }

        /// <summary>
        /// Gets or sets the index of the item selected range-selector combobox.
        /// </summary>
        public int RangeSelectorSelectedIndex { get; set; }
    }

    /// <summary>
    /// Represents the expansion status of each control panel.
    /// </summary>
    public class PanelExpansion
    {
        /// <summary>
        /// Gets or sets a value indicating whether the camera panel is expanded.
        /// </summary>
        public bool Camera { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the light panel is expanded.
        /// </summary>
        public bool Light { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the accessory panel is expanded.
        /// </summary>
        public bool Accessory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bone panel is expanded.
        /// </summary>
        public bool Bone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the morph panel is expanded.
        /// </summary>
        public bool Morph { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the self-shadow panel is expanded.
        /// </summary>
        public bool SelfShadow { get; set; }
    }
}
