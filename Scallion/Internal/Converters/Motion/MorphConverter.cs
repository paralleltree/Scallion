using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Motion
{
    internal class MorphConverter : IConverter<IEnumerable<Raw.Components.Motion.MorphKeyFrame>, IEnumerable<Morph>>
    {
        public IEnumerable<Morph> Convert(IEnumerable<Raw.Components.Motion.MorphKeyFrame> src)
        {
            var dic = new Dictionary<string, Morph>();
            foreach (var item in src)
            {
                if (!dic.ContainsKey(item.MorphName)) dic.Add(item.MorphName, new Morph()
                {
                    Name = item.MorphName
                });
                dic[item.MorphName].KeyFrames.AddLast(new MorphKeyFrame()
                {
                    KeyFrameIndex = item.KeyFrameIndex,
                    Weight = item.Weight
                });
            }
            return dic.Values.ToList();
        }

        public IEnumerable<Raw.Components.Motion.MorphKeyFrame> ConvertBack(IEnumerable<Morph> src)
        {
            return src.SelectMany(p => p.KeyFrames.Select(q => new Raw.Components.Motion.MorphKeyFrame()
            {
                MorphName = p.Name,
                KeyFrameIndex = q.KeyFrameIndex,
                Weight = q.Weight
            }));
        }
    }
}
