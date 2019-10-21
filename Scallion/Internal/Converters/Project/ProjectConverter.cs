using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.DomainModels.Components;

namespace Scallion.Internal.Converters.Project
{
    internal class ProjectConverter : IInstanceConverter<Raw.Project, DomainModels.Project>
    {
        public DomainModels.Project Convert(Raw.Project src, DomainModels.Project obj)
        {
            obj.OutputSize = src.OutputSize;
            obj.Models = src.Models.Select(p => new ModelConverter().Convert(p)).ToList();
            obj.Accessories = src.Accessories.Select(p => new AccessoryConverter().Convert(p)).ToList();
            obj.Camera = new CameraConverter().Convert(src.Camera);
            obj.Light = new LightConverter().Convert(src.Light);
            obj.Media = new MediaConverter().Convert(src.Media);
            obj.SelfShadow = new SelfShadowConverter().Convert(src.SelfShadow);

            new PanelConverter().Convert(src, obj);
            new ViewConverter().Convert(src, obj);
            new PhysicsConverter().Convert(src, obj);

            obj.Camera.CurrentStatus.AngleOfView = (int)src.AngleOfView; // :thinking_face:

            // resolving bone references
            var modeldic = obj.Models.ToDictionary(p => p.Index, p => p);

            BoneReference ResolveReference(Raw.Components.Project.BoneReference reference)
            {
                return reference.ModelIndex == -1 ? new BoneReference() :
                    new BoneReference(modeldic[reference.ModelIndex], modeldic[reference.ModelIndex].Bones[reference.BoneIndex]);
            }

            // for bones
            new ModelKeyFrameConverter().Convert(src, obj);

            // for camera
            var camframedic = obj.Camera.KeyFrames.ToDictionary(p => p.KeyFrameIndex, p => p);
            foreach (var frame in src.Camera.KeyFrames.Extract(src.Camera.InitialKeyFrame))
            {
                camframedic[frame.KeyFrameIndex].FollowingBone = ResolveReference(frame.FollowingBone);
            }

            obj.Camera.CurrentStatus.FollowingBone = ResolveReference(src.CameraFollowingBone);

            // for accessories
            var accdic = obj.Accessories.ToDictionary(p => p.Index, p => p);
            foreach (var accessory in src.Accessories)
            {
                var framedic = accdic[accessory.Index].KeyFrames.ToDictionary(p => p.KeyFrameIndex, p => p);
                foreach (var keyframe in accessory.KeyFrames.Extract(accessory.InitialKeyFrame))
                {
                    var reference = keyframe.Value.ExternalParent;
                    framedic[keyframe.KeyFrameIndex].ExternalParent = ResolveReference(keyframe.Value.ExternalParent);
                }

                accdic[accessory.Index].CurrentStatus.ExternalParent = ResolveReference(accessory.CurrentStatus.ExternalParent);
            }

            // resolving range selections
            foreach (var item in src.RangeSelections)
            {
                modeldic[item.ModelIndex].TimelinePanel.RangeSelectorSelectedIndex = item.SelectedIndex;
            }

            return obj;
        }

        public Raw.Project ConvertBack(DomainModels.Project src, Raw.Project obj)
        {
            obj.OutputSize = src.OutputSize;
            obj.Models = src.Models.Select(p => new ModelConverter().ConvertBack(p)).ToList();
            obj.Accessories = src.Accessories.Select(p => new AccessoryConverter().ConvertBack(p)).ToList();
            obj.Camera = new CameraConverter().ConvertBack(src.Camera);
            obj.Light = new LightConverter().ConvertBack(src.Light);
            obj.Media = new MediaConverter().ConvertBack(src.Media);
            obj.SelfShadow = new SelfShadowConverter().ConvertBack(src.SelfShadow);

            new PanelConverter().ConvertBack(src, obj);
            new ViewConverter().ConvertBack(src, obj);
            new PhysicsConverter().ConvertBack(src, obj);

            obj.AngleOfView = src.Camera.CurrentStatus.AngleOfView;

            // bone references
            var boneIndexDic = src.Models.ToDictionary(p => p, p => p.Bones.Select((q, i) => new { Bone = q, Index = i }).ToDictionary(q => q.Bone, q => q.Index));

            Raw.Components.Project.BoneReference ResolveReference(BoneReference reference)
            {
                return reference.TargetModel == null ? Raw.Components.Project.BoneReference.Empty :
                    new Raw.Components.Project.BoneReference(reference.TargetModel.Index, boneIndexDic[reference.TargetModel][reference.TargetBone]);
            }

            new ModelKeyFrameConverter().ConvertBack(src, obj);

            // for camera
            var camframedic = obj.Camera.KeyFrames.Extract(obj.Camera.InitialKeyFrame).ToDictionary(p => p.KeyFrameIndex, p => p);
            foreach (var frame in src.Camera.KeyFrames)
            {
                camframedic[frame.KeyFrameIndex].FollowingBone = ResolveReference(frame.FollowingBone);
            }

            obj.CameraFollowingBone = ResolveReference(src.Camera.CurrentStatus.FollowingBone);

            // for accesories
            var accdic = obj.Accessories.ToDictionary(p => p.Index, p => p);
            foreach (var accessory in src.Accessories)
            {
                var framedic = accdic[(byte)accessory.Index].KeyFrames.Extract(accdic[(byte)accessory.Index].InitialKeyFrame).ToDictionary(p => p.KeyFrameIndex, p => p);
                foreach (var keyframe in accessory.KeyFrames)
                {
                    framedic[keyframe.KeyFrameIndex].Value.ExternalParent = ResolveReference(keyframe.ExternalParent);
                }
                accdic[(byte)accessory.Index].CurrentStatus.ExternalParent = ResolveReference(accessory.CurrentStatus.ExternalParent);
            }

            // range selections
            obj.RangeSelections = src.Models.Select(p => new Raw.Components.Project.RangeSelection()
            {
                ModelIndex = (byte)p.Index,
                SelectedIndex = p.TimelinePanel.RangeSelectorSelectedIndex
            }).ToList();

            return obj;
        }
    }
}
