using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;

namespace Scallion.DomainModels.Components
{
    /// <summary>
    /// Represents MMD 3D View.
    /// </summary>
    public class View
    {
        /// <summary>
        /// Gets or sets a value indicating whether the information is visible.
        /// </summary>
        public bool IsInformationVisible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the axes are visible.
        /// </summary>
        public bool IsAxesVisible { get; set; }

        /// <summary>
        /// Gets or sets the color of edges.
        /// </summary>
        public Color EdgeColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the background color is black.
        /// </summary>
        public bool IsBackgroundBlack { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the limited fps.
        /// </summary>
        public FpsLimit FpsLimit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether surface shadows are enabled.
        /// </summary>
        public bool IsSurfaceShadowEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the brightness of surface shadows.
        /// </summary>
        public float SurfaceShadowBrightness { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether surface shadows are transparent.
        /// </summary>
        public bool IsSurfaceShadowTransparent { get; set; }

        /// <summary>
        /// Gets or sets the first index of the accessories being rendered after models.
        /// </summary>
        public int AccessoryRenderedAfterModelIndex { get; set; }
    }
}
