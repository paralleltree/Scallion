using System;
using System.Collections.Generic;
using System.Linq;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Specifies the mode of screen capturing.
    /// </summary>
    public enum ScreenCaptureMode
    {
        /// <summary>
        /// Do not capture the screen.
        /// </summary>
        Off,

        /// <summary>
        /// Captures the screen.
        /// </summary>
        FullScreen,

        /// <summary>
        /// Captures the screen as 4:3.
        /// </summary>
        FourByThree,

        /// <summary>
        /// Displays the video file.
        /// </summary>
        BackgroundVideo
    }

    /// <summary>
    /// Specifies the mode of using physics engine.
    /// </summary>
    public enum PhysicsMode
    {
        /// <summary>
        /// Do not use the physics engine.
        /// </summary>
        Off,

        /// <summary>
        /// Always use the physics engine.
        /// </summary>
        Always,

        /// <summary>
        /// Switches whether the physics calculation is enabled for each bone.
        /// </summary>
        OnOff,

        /// <summary>
        /// Locks rigid bodies while the physics calculation for the related bone is disabled.
        /// </summary>
        Tracing
    }

    /// <summary>
    /// Specifies the method of selecting bones.
    /// </summary>
    public enum BoneSelectionType
    {
        /// <summary>
        /// Selects a bone.
        /// </summary>
        SingleSelection,

        /// <summary>
        /// Selects bones in the rectangle made by dragging.
        /// </summary>
        BoxSelection,

        /// <summary>
        /// Do not select any bones.
        /// </summary>
        None,

        /// <summary>
        /// Selects a bone and edits its rotation.
        /// </summary>
        Rotate,

        /// <summary>
        /// Selects a bone and edits its location.
        /// </summary>
        Move
    }

    /// <summary>
    /// Specifies a target object that the camera follows.
    /// </summary>
    public enum CameraFollowingType
    {
        /// <summary>
        /// Do not follow any targets.
        /// </summary>
        None,

        /// <summary>
        /// Follows the selected model.
        /// </summary>
        Model,

        /// <summary>
        /// Follows the selected bone of the selected model.
        /// </summary>
        Bone
    }
}
