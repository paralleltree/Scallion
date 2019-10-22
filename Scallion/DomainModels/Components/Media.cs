using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Represents a set of media in MMD.
    /// </summary>
    public class Media
    {
        /// <summary>
        /// Gets or sets a instance of the <see cref="Components.Audio"/> class.
        /// </summary>
        public Audio Audio { get; set; }

        /// <summary>
        /// Gets or sets a instance of the <see cref="Components.Video"/> class.
        /// </summary>
        public Video Video { get; set; }

        /// <summary>
        /// Gets or sets a instance of the <see cref="Components.BackgroundImage"/> class.
        /// </summary>
        public BackgroundImage BackgroundImage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Media"/> class.
        /// </summary>
        public Media()
        {
            Audio = new Audio();
            Video = new Video();
            BackgroundImage = new BackgroundImage();
        }
    }

    /// <summary>
    /// Represents a Audio in MMD.
    /// </summary>
    public class Audio
    {
        /// <summary>
        /// Gets or sets a value indicating whether this audio is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the path of the audio file.
        /// </summary>
        public string Path { get; set; }
    }

    /// <summary>
    /// Represents a Video in MMD.
    /// </summary>
    public class Video
    {
        /// <summary>
        /// Gets or sets the position of the video.
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Gets or sets the scale factor applied to the video.
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this video is visible.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the path of the video file.
        /// </summary>
        public string Path { get; set; }
    }

    /// <summary>
    /// Represents a BackgroundImage in MMD.
    /// </summary>
    public class BackgroundImage
    {
        /// <summary>
        /// Gets or sets the position of the image.
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Gets or sets the scale factor applied to the image.
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this image is visible.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the path of the background image file.
        /// </summary>
        public string Path { get; set; }
    }
}
