using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Project
{
    internal class BoneConverter : IInstanceConverter<Raw.Components.Project.Model, Model>
    {
        public Model Convert(Raw.Components.Project.Model src, Model obj)
        {
            var list = new List<Bone>(src.BonesCount);
            for (int i = 0; i < src.BonesCount; i++)
            {
                var bone = new Bone()
                {
                    Name = src.BoneNames[i],
                    KeyFrames = src.BoneKeyFrames.Extract(src.InitialBoneKeyFrames[i]).Select(p => new BoneKeyFrame()
                    {
                        KeyFrameIndex = p.KeyFrameIndex,
                        Value = new BoneState()
                        {
                            Position = p.Value.Position,
                            Quaternion = p.Value.Quaternion,
                            Interpolation = p.Value.Interpolation,
                            IsPhysicsEnabled = p.Value.IsPhysicsEnabled,
                        },
                        IsSelected = p.IsSelected
                    }).ToList(),
                    CurrentStatus = new CurrentBoneState()
                    {
                        Position = src.CurrentBoneStatuses[i].Position,
                        Quaternion = src.CurrentBoneStatuses[i].Quaternion,
                        IsSaved = src.CurrentBoneStatuses[i].IsSaved,
                        IsPhysicsEnabled = src.CurrentBoneStatuses[i].IsPhysicsEnabled,
                        IsRowSelected = src.CurrentBoneStatuses[i].IsRowSelected
                    }
                };
                list.Add(src.IKBoneIndices.Contains(i) ?
                    new IKBone(bone) { CurrentIKStatus = new IKBoneState() { IsIKEnabled = src.IKBonesEnabled[src.IKBoneIndices.IndexOf(i)] } } : bone);
            }
            obj.Bones = list;
            return obj;
        }

        public Raw.Components.Project.Model ConvertBack(Model src, Raw.Components.Project.Model obj)
        {
            obj.BoneNames = src.Bones.Select(p => p.Name).ToList();

            var currentList = new List<Raw.Components.Project.CurrentBoneState>();
            var initList = new List<Raw.Components.Project.BoneKeyFrame>();
            var framesList = new List<Raw.Components.Project.BoneKeyFrame>();
            int dataIndex = src.Bones.Count;
            for (int i = 0; i < src.Bones.Count; i++)
            {
                var current = src.Bones[i].CurrentStatus;
                currentList.Add(new Raw.Components.Project.CurrentBoneState()
                {
                    Position = current.Position,
                    Quaternion = current.Quaternion,
                    IsSaved = current.IsSaved,
                    IsPhysicsEnabled = current.IsPhysicsEnabled,
                    IsRowSelected = current.IsRowSelected
                });

                var frames = src.Bones[i].KeyFrames.Select(p => new Raw.Components.Project.BoneKeyFrame()
                {
                    KeyFrameIndex = p.KeyFrameIndex,
                    Value = new Raw.Components.Project.BoneState()
                    {
                        Position = p.Value.Position,
                        Quaternion = p.Value.Quaternion,
                        Interpolation = p.Value.Interpolation,
                        IsPhysicsEnabled = p.Value.IsPhysicsEnabled
                    },
                    IsSelected = p.IsSelected
                }).ToList();

                var init = frames.Single(p => p.KeyFrameIndex == 0);
                init.IsInitialKeyFrame = true;
                init.NextDataIndex = frames.Count > 1 ? dataIndex : 0;
                initList.Add(init);

                framesList.AddRange(frames.Where(p => p.KeyFrameIndex > 0).ToList().Pack(dataIndex, i));
                dataIndex += frames.Count - 1;
            }
            obj.CurrentBoneStatuses = currentList;
            obj.InitialBoneKeyFrames = initList;
            obj.BoneKeyFrames = framesList;

            obj.IKBoneIndices = src.Bones.Select((p, i) => new { Bone = p, Index = i }).Where(p => p.Bone is IKBone).Select(p => p.Index).ToList();
            obj.IKBonesEnabled = src.Bones.Where(p => p is IKBone).Cast<IKBone>().Select(p => p.CurrentIKStatus.IsIKEnabled).ToList();

            return obj;
        }
    }
}
