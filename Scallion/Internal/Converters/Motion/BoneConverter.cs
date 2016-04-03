using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Motion
{
    internal class BoneConverter : IConverter<IEnumerable<Raw.Components.Motion.BoneKeyFrame>, IEnumerable<Bone>>
    {
        public IEnumerable<Bone> Convert(IEnumerable<Raw.Components.Motion.BoneKeyFrame> src)
        {
            var dic = new Dictionary<string, Bone>();
            foreach (var item in src)
            {
                if (!dic.ContainsKey(item.BoneName)) dic.Add(item.BoneName, new Bone() { Name = item.BoneName });
                dic[item.BoneName].KeyFrames.AddLast(new BoneKeyFrame()
                {
                    KeyFrameIndex = item.KeyFrameIndex,
                    Position = item.Position,
                    Quaternion = item.Quaternion,
                    Interpolation = item.Interpolation
                });
            }
            return dic.Values.ToList();
        }

        public IEnumerable<Raw.Components.Motion.BoneKeyFrame> ConvertBack(IEnumerable<Bone> src)
        {
            return src.SelectMany(p => p.KeyFrames.Select(q => new Raw.Components.Motion.BoneKeyFrame()
            {
                KeyFrameIndex = q.KeyFrameIndex,
                BoneName = p.Name,
                Position = q.Position,
                Quaternion = q.Quaternion,
                Interpolation = new Raw.Components.Motion.BoneInterpolationImpl()
                {
                    X = q.Interpolation.X,
                    Y = q.Interpolation.Y,
                    Z = q.Interpolation.Z,
                    R = q.Interpolation.R
                }
            }));
        }
    }
}
