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
                    ikdic[ik.BoneName].IKStateKeyFrames.Add(new IKStateKeyFrame()
                    {
                        KeyFrameIndex = item.KeyFrameIndex,
                        IsIKEnabled = ik.IsEnabled
                    });
                }

                obj.VisibilityKeyFrames.Add(new VisibilityKeyFrame()
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
            var ikKeyFrameNodes = ikbones.Select(p => new LinkedList<IKStateKeyFrame>(p.IKStateKeyFrames.OrderBy(q => q.KeyFrameIndex)).First).ToList();
            var visibilitiesNode = new LinkedList<VisibilityKeyFrame>(src.VisibilityKeyFrames.OrderBy(p => p.KeyFrameIndex)).First;
            var keyframeIndices = new HashSet<int>();

            foreach (int idx in src.IKBones.SelectMany(p => p.IKStateKeyFrames.Select(q => q.KeyFrameIndex)))
                keyframeIndices.Add(idx);
            foreach (int idx in src.VisibilityKeyFrames.Select(p => p.KeyFrameIndex))
                keyframeIndices.Add(idx);

            // Merge model visibilities and IK bone states into a key frame by indices
            var list = new List<Raw.Components.Motion.ModelKeyFrame>(keyframeIndices.Count);
            foreach (int idx in keyframeIndices.OrderBy(p => p))
            {
                for (int i = 0; i < ikKeyFrameNodes.Count; i++)
                {
                    if (ikKeyFrameNodes[i].Next != null && idx >= ikKeyFrameNodes[i].Next.Value.KeyFrameIndex)
                        ikKeyFrameNodes[i] = ikKeyFrameNodes[i].Next;
                }
                if (visibilitiesNode.Next != null && idx >= visibilitiesNode.Next.Value.KeyFrameIndex)
                    visibilitiesNode = visibilitiesNode.Next;

                list.Add(new Raw.Components.Motion.ModelKeyFrame()
                {
                    KeyFrameIndex = idx,
                    IKData = ikKeyFrameNodes.Select((p, i) => new Raw.Components.Motion.IKBoneData()
                    {
                        BoneName = ikbones[i].Name,
                        IsEnabled = ikKeyFrameNodes[i].Value.IsIKEnabled
                    }).ToList(),
                    IsVisible = visibilitiesNode.Value.IsVisible
                });
            }

            obj.ModelKeyFrames = list;
            return obj;
        }
    }
}
