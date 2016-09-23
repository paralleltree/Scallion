using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using Scallion.Core;
using Scallion.DomainModels.Components;
using Scallion.Internal.Converters.Project;

namespace Scallion.DomainModels
{
    /// <summary>
    /// Represents a MMD Project File(.pmm).
    /// </summary>
    public class Project : IMMDFile<Project>
    {
        /// <summary>
        /// Gets or sets the size of the ouput image.
        /// </summary>
        public Size OutputSize { get; set; }

        /// <summary>
        /// Gets or sets a collection of the <see cref="Model"/> class.
        /// </summary>
        public List<Model> Models { get; set; }

        /// <summary>
        /// Gets or sets a collection of the <see cref="Accessory"/> class.
        /// </summary>
        public List<Accessory> Accessories { get; set; }

        /// <summary>
        /// Gets or sets an instance of the <see cref="Camera"/> class.
        /// </summary>
        public Camera Camera { get; set; }

        /// <summary>
        /// Gets or sets an instance of the <see cref="Components.Light"/> class.
        /// </summary>
        public Light Light { get; set; }

        /// <summary>
        /// Gets or sets an instance of the <see cref="Components.SelfShadow"/> class.
        /// </summary>
        public SelfShadow SelfShadow { get; set; }

        /// <summary>
        /// Gets or sets an instance of the <see cref="Components.Physics"/> class.
        /// </summary>
        public Physics Physics { get; set; }

        /// <summary>
        /// Gets or sets an instance of the <see cref="Components.Media"/> class.
        /// </summary>
        public Media Media { get; set; }

        /// <summary>
        /// Gets or sets an instance of the <see cref="Components.Panel"/> class.
        /// </summary>
        public Panel Panel { get; set; }

        /// <summary>
        /// Gets or sets an instance of the <see cref="Components.View"/> class.
        /// </summary>
        public View View { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        public Project()
        {
            OutputSize = new Size(640, 360);
            Models = new List<Model>();
            Accessories = new List<Accessory>();
            Camera = new Camera();
            Light = new Light();
            SelfShadow = new SelfShadow();
            Physics = new Physics();
            Media = new Media();
            Panel = new Panel();
            View = new View();
        }

        /// <summary>
        /// Loads a project from the specified file.
        /// </summary>
        /// <param name="path">The file path to load a project</param>
        /// <returns>The self instance assigned values from file</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the specified file is invalid or unsupported file.
        /// </exception>
        public Project Load(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves this project to the specified file.
        /// </summary>
        /// <param name="path">The file path to save this project</param>
        public void Save(string path)
        {
            throw new NotImplementedException();
        }
    }
}
