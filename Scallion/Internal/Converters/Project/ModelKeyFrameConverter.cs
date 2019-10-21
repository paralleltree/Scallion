using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Project
{
    /// <summary>
    /// Converts key frames of a model consist of external parent status, IK status, and visibility.
    /// </summary>
    internal class ModelKeyFrameConverter : IInstanceConverter<Raw.Project, DomainModels.Project>
    {
        public DomainModels.Project Convert(Raw.Project src, DomainModels.Project obj)
        {
            var modeldic = obj.Models.ToDictionary(p => p.Index, p => p);
            foreach (var model in src.Models)
            {
                var ikdic = model.IKBoneIndices.ToDictionary(p => p, p => (IKBone)modeldic[model.Index].Bones[p]);
                foreach (var keyframe in model.ModelKeyFrames.Extract(model.InitialModelKeyFrame))
                {
                    for (int i = 0; i < keyframe.Value.ExternalParentBonesCount; i++)
                    {
                        if (model.ExternalParentBoneIndices[i] < 0) continue;
                        var reference = keyframe.Value.ExternalParentBoneStatuses[i];
                        modeldic[model.Index].Bones[model.ExternalParentBoneIndices[i]].ExternalParentKeyFrames.Add(new ExternalParentKeyFrame()
                        {
                            KeyFrameIndex = keyframe.KeyFrameIndex,
                            Reference = reference.ModelIndex == -1 ? new BoneReference() : new BoneReference(
                                model: modeldic[reference.ModelIndex],
                                bone: reference.BoneIndex == -1 ? null : modeldic[reference.ModelIndex].Bones[reference.BoneIndex]
                                ),
                            IsSelected = keyframe.IsSelected
                        });
                    }

                    for (int i = 0; i < model.IKBonesCount; i++)
                    {
                        ikdic[model.IKBoneIndices[i]].IKStateKeyFrames.Add(new IKStateKeyFrame()
                        {
                            KeyFrameIndex = keyframe.KeyFrameIndex,
                            IsIKEnabled = keyframe.Value.IKEnabled[i],
                            IsSelected = keyframe.IsSelected
                        });
                    }

                    modeldic[model.Index].VisibilityKeyFrames.Add(new VisibilityKeyFrame()
                    {
                        KeyFrameIndex = keyframe.KeyFrameIndex,
                        IsVisible = keyframe.Value.IsVisible,
                        IsSelected = keyframe.IsSelected
                    });
                }

                // resolve current external parent
                for (int i = 0; i < model.ExternalParentBonesCount; i++)
                {
                    int bone = model.ExternalParentBoneIndices[i];
                    if (bone < 0) continue;
                    var reference = model.ExternalParentStatuses[i].ExternalParent;
                    modeldic[model.Index].Bones[bone].CurrentStatus.ExternalParent = reference.ModelIndex == -1 ? new BoneReference() : new BoneReference(
                        model: modeldic[reference.ModelIndex],
                        bone: reference.BoneIndex == -1 ? null : modeldic[reference.ModelIndex].Bones[reference.BoneIndex]);
                }

                // current ik
                for (int i = 0; i < model.IKBoneIndices.Count; i++)
                {
                    (modeldic[model.Index].Bones[model.IKBoneIndices[i]] as IKBone).CurrentIKStatus = new IKBoneState() { IsIKEnabled = model.IKBonesEnabled[i] };
                }
            }

            return obj;
        }

        public Raw.Project ConvertBack(DomainModels.Project src, Raw.Project obj)
        {
            var modeldic = obj.Models.ToDictionary(p => p.Index, p => p);
            var boneIndexDic = src.Models.ToDictionary(p => p, p => p.Bones.Select((q, i) => new { Bone = q, Index = i }).ToDictionary(q => q.Bone, q => q.Index));
            foreach (var model in src.Models)
            {
                var ikBoneIndices = model.Bones.Select((p, i) => new { Index = i, Bone = p }).Where(p => p.Bone is IKBone).Select(p => p.Index).ToList();
                var ikKeyFrameNodes = ikBoneIndices.Select(p => new LinkedList<IKStateKeyFrame>((model.Bones[p] as IKBone).IKStateKeyFrames.OrderBy(q => q.KeyFrameIndex)).First).ToList();
                var externalParentBoneIndices = model.Bones.Select((p, i) => new { Index = i, Count = p.ExternalParentKeyFrames.Count }).Where(p => p.Count > 0).Select(p => p.Index).ToList();
                var externalParentKeyFrameNodes = externalParentBoneIndices.Select(p => new LinkedList<ExternalParentKeyFrame>(model.Bones[p].ExternalParentKeyFrames.OrderBy(q => q.KeyFrameIndex)).First).ToList();
                var visibilityKeyFrameNode = new LinkedList<VisibilityKeyFrame>(model.VisibilityKeyFrames.OrderBy(p => p.KeyFrameIndex)).First;

                modeldic[(byte)model.Index].IKBoneIndices = ikBoneIndices;
                modeldic[(byte)model.Index].ExternalParentBoneIndices = externalParentBoneIndices;

                var modelKeyFrames = new List<Raw.Components.Project.ModelKeyFrame>();
                var keyFrameIndices = new HashSet<int>();

                foreach (int idx in ikBoneIndices.SelectMany(p => (model.Bones[p] as IKBone).IKStateKeyFrames.Select(q => q.KeyFrameIndex)))
                    keyFrameIndices.Add(idx);
                foreach (int idx in externalParentBoneIndices.SelectMany(p => model.Bones[p].ExternalParentKeyFrames.Select(q => q.KeyFrameIndex)))
                    keyFrameIndices.Add(idx);
                foreach (int idx in model.VisibilityKeyFrames.Select(p => p.KeyFrameIndex))
                    keyFrameIndices.Add(idx);

                // first item is -1(strange).
                externalParentBoneIndices.Insert(0, -1);
                externalParentKeyFrameNodes.Insert(0, new LinkedList<ExternalParentKeyFrame>(new[] { new ExternalParentKeyFrame() { Reference = new BoneReference() } }).First);

                foreach (int idx in keyFrameIndices.OrderBy(p => p))
                {
                    for (int i = 0; i < ikKeyFrameNodes.Count; i++)
                    {
                        if (ikKeyFrameNodes[i].Next != null && idx >= ikKeyFrameNodes[i].Next.Value.KeyFrameIndex)
                            ikKeyFrameNodes[i] = ikKeyFrameNodes[i].Next;
                    }
                    for (int i = 0; i < externalParentKeyFrameNodes.Count; i++)
                    {
                        if (externalParentKeyFrameNodes[i].Next != null && idx >= externalParentKeyFrameNodes[i].Next.Value.KeyFrameIndex)
                        {
                            externalParentKeyFrameNodes[i] = externalParentKeyFrameNodes[i].Next;
                        }
                    }
                    if (visibilityKeyFrameNode.Next != null && idx >= visibilityKeyFrameNode.Next.Value.KeyFrameIndex)
                        visibilityKeyFrameNode = visibilityKeyFrameNode.Next;


                    var boneRefs = externalParentKeyFrameNodes.Select(p =>
                    {
                        var reference = p.Value.Reference;
                        return reference.TargetModel == null ? Raw.Components.Project.BoneReference.Empty :
                            new Raw.Components.Project.BoneReference(reference.TargetModel.Index, reference.TargetBone == null ? -1 : boneIndexDic[reference.TargetModel][reference.TargetBone]);
                    }).ToList();
                    modelKeyFrames.Add(new Raw.Components.Project.ModelKeyFrame()
                    {
                        KeyFrameIndex = idx,
                        Value = new Raw.Components.Project.ModelState()
                        {
                            IKEnabled = ikKeyFrameNodes.Select(p => p.Value.IsIKEnabled).ToList(),
                            ExternalParentBoneStatuses = boneRefs,
                            IsVisible = visibilityKeyFrameNode.Value.IsVisible
                        },
                        IsSelected = false
                    });
                }

                var init = modelKeyFrames.Single(p => p.KeyFrameIndex == 0);
                init.IsInitialKeyFrame = true;
                init.NextDataIndex = modelKeyFrames.Count > 1 ? 1 : 0;
                modeldic[(byte)model.Index].InitialModelKeyFrame = init;
                modeldic[(byte)model.Index].ModelKeyFrames = modelKeyFrames.Where(p => p.KeyFrameIndex > 0).ToList().Pack(1);

                // current external parent
                modeldic[(byte)model.Index].ExternalParentStatuses = externalParentBoneIndices.Select(p =>
                {
                    if (p == -1) return new Raw.Components.Project.ExternalParentState() { ExternalParent = Raw.Components.Project.BoneReference.Empty };
                    var reference = model.Bones[p].CurrentStatus.ExternalParent;
                    var indexRef = reference.TargetModel == null ? Raw.Components.Project.BoneReference.Empty :
                        new Raw.Components.Project.BoneReference(reference.TargetModel.Index, reference.TargetBone == null ? -1 : boneIndexDic[reference.TargetModel][reference.TargetBone]);
                    return new Raw.Components.Project.ExternalParentState()
                    {
                        ExternalParent = indexRef,
                        CurrentKeyFrameIndex = keyFrameIndices.OrderBy(q => q).FirstOrDefault(q => q <= src.Panel.TimelinePanel.CurrentFrameIndex),
                        NextKeyFrameIndex = keyFrameIndices.OrderBy(q => q).FirstOrDefault(q => q > src.Panel.TimelinePanel.CurrentFrameIndex)
                    };
                }).ToList();

                // current ik status
                modeldic[(byte)model.Index].IKBonesEnabled = ikBoneIndices.Select(p => (model.Bones[p] as IKBone).CurrentIKStatus.IsIKEnabled).ToList();
            }

            return obj;
        }
    }
}
