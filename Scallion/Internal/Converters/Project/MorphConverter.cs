using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Project
{
    internal class MorphConverter : IInstanceConverter<Raw.Components.Project.Model, Model>
    {
        public Model Convert(Raw.Components.Project.Model src, Model obj)
        {
            var list = new List<Morph>(src.MorphsCount);
            for (int i = 0; i < src.MorphsCount; i++)
            {
                list.Add(new Morph()
                {
                    Name = src.MorphNames[i],
                    KeyFrames = src.MorphKeyFrames.Extract(src.InitialMorphKeyFrames[i]).Select(p => new MorphKeyFrame
                    {
                        KeyFrameIndex = p.KeyFrameIndex,
                        Value = new MorphState() { Weight = p.Value.Weight },
                        IsSelected = p.IsSelected
                    }).ToList(),
                    CurrentStatus = new MorphState() { Weight = src.CurrentMorphStatuses[i].Weight }
                });
            }
            obj.Morphs = list;
            return obj;
        }

        public Raw.Components.Project.Model ConvertBack(Model src, Raw.Components.Project.Model obj)
        {
            obj.MorphNames = src.Morphs.Select(p => p.Name).ToList();

            var currentList = new List<Raw.Components.Project.MorphState>();
            var initList = new List<Raw.Components.Project.MorphKeyFrame>();
            var framesList = new List<Raw.Components.Project.MorphKeyFrame>();
            int dataIndex = src.Morphs.Count;

            for (int i = 0; i < src.Morphs.Count; i++)
            {
                currentList.Add(new Raw.Components.Project.MorphState() { Weight = src.Morphs[i].CurrentStatus.Weight });

                var frames = src.Morphs[i].KeyFrames.Select(p => new Raw.Components.Project.MorphKeyFrame()
                {
                    KeyFrameIndex = p.KeyFrameIndex,
                    Value = new Raw.Components.Project.MorphState() { Weight = p.Value.Weight },
                    IsSelected = p.IsSelected
                }).ToList();

                var init = frames.Single(p => p.KeyFrameIndex == 0);
                init.IsInitialKeyFrame = true;
                init.NextDataIndex = frames.Count > 1 ? dataIndex : 0;
                initList.Add(init);

                framesList.AddRange(frames.Where(p => p.KeyFrameIndex > 0).ToList().Pack(dataIndex, i));
                dataIndex += frames.Count - 1;
            }
            obj.CurrentMorphStatuses = currentList;
            obj.InitialMorphKeyFrames = initList;
            obj.MorphKeyFrames = framesList;

            return obj;
        }
    }
}
