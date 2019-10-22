using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Project
{
    internal class MediaConverter : IConverter<Raw.Components.Project.Media, Media>
    {
        public DomainModels.Components.Media Convert(Raw.Components.Project.Media src)
        {
            return new Media()
            {
                Audio = new Audio()
                {
                    IsEnabled = src.Audio.IsEnabled,
                    Path = src.Audio.Path
                },
                Video = new Video()
                {
                    IsVisible = src.Video.IsVisible,
                    Path = src.Video.Path,
                    Position = src.Video.Position,
                    Scale = src.Video.Scale
                },
                BackgroundImage = new BackgroundImage()
                {
                    IsVisible = src.BackgroundImage.IsVisible,
                    Path = src.BackgroundImage.Path,
                    Position = src.BackgroundImage.Position,
                    Scale = src.BackgroundImage.Scale
                }
            };
        }

        public Raw.Components.Project.Media ConvertBack(DomainModels.Components.Media src)
        {
            return new Raw.Components.Project.Media()
            {
                Audio = new Raw.Components.Project.Audio()
                {
                    IsEnabled = src.Audio.IsEnabled,
                    Path = src.Audio.Path
                },
                Video = new Raw.Components.Project.Video()
                {
                    IsVisible = src.Video.IsVisible,
                    Path = src.Video.Path,
                    Position = src.Video.Position,
                    Scale = src.Video.Scale
                },
                BackgroundImage = new Raw.Components.Project.BackgroundImage()
                {
                    IsVisible = src.BackgroundImage.IsVisible,
                    Path = src.BackgroundImage.Path,
                    Position = src.BackgroundImage.Position,
                    Scale = src.BackgroundImage.Scale
                }
            };
        }
    }
}
