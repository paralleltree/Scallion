using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Motion
{
    internal class ModelKeyFrameConverter : IInstanceConverter<Raw.Motion, DomainModels.Motion>
    {
        public DomainModels.Motion Convert(Raw.Motion src, DomainModels.Motion obj)
        {
            var iknames = new HashSet<string>(src.ModelKeyFrames.SelectMany(p => p.IKData.Select(q => q.BoneName)));
            // Replace Bone with IKBone
            obj.Bones = obj.Bones.Select(p =>
            {
                if (iknames.Contains(p.Name))
                    return new IKBone()
                    {
                        KeyFrames = p.KeyFrames,
                        Name = p.Name
                    };
                return p;
            }).ToList();

            // Restore the missing IK bone which does not have any key frames
            obj.Bones.AddRange(iknames.Except(obj.Bones.Select(p => p.Name)).Select(p => new IKBone() { Name = p }));

            var ikdic = new Dictionary<string, IKBone>();
            foreach (var ik in obj.IKBones)
                ikdic.Add(ik.Name, ik);

            // Extract key frames
            foreach (var item in src.ModelKeyFrames)
            {
                foreach (var ik in item.IKData)
                {
                    ikdic[ik.BoneName].IKStateKeyFrames.AddLast(new IKStateKeyFrame()
                    {
                        KeyFrameIndex = item.KeyFrameIndex,
                        IsIKEnabled = ik.IsEnabled
                    });
                }

                obj.VisibilityKeyFrames.AddLast(new VisibilityKeyFrame()
                {
                    KeyFrameIndex = item.KeyFrameIndex,
                    IsVisible = item.IsVisible
                });
            }

            return obj;
        }


        public Raw.Motion ConvertBack(DomainModels.Motion src, Raw.Motion obj)
        {
            var ikbones = src.IKBones.ToList();
            var visibilities = src.VisibilityKeyFrames.OrderBy(p => p.KeyFrameIndex).ToList();

            // Merge model visibilities and IK bone states into a key frame by indices
            obj.ModelKeyFrames = src.IKBones.SelectMany(p => p.IKStateKeyFrames.Select(q => q.KeyFrameIndex))
                .Concat(visibilities.Select(p => p.KeyFrameIndex))
                .OrderBy(p => p)
                .Distinct()
                .Select(p => new Raw.Components.Motion.ModelKeyFrame()
                {
                    KeyFrameIndex = p,
                    IKData = ikbones.Select(q => new Raw.Components.Motion.IKBoneData()
                    {
                        BoneName = q.Name,
                        IsEnabled = (q.IKStateKeyFrames.LastOrDefault(r => r.KeyFrameIndex <= p) ?? q.IKStateKeyFrames.First()).IsIKEnabled
                    }).ToList(),
                    IsVisible = (visibilities.LastOrDefault(q => q.KeyFrameIndex <= p) ?? visibilities.First()).IsVisible
                })
                .ToList();
            return obj;
        }
    }
}
