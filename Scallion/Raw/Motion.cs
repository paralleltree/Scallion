using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;
using Scallion.Internal;
using Scallion.Raw.Components.Motion;

namespace Scallion.Raw
{
    internal class Motion : MMDFile<Motion>
    {
        public static readonly string Signature = "Vocaloid Motion Data 0002";
        public static readonly string CameraName = "カメラ・照明\0on Data";

        public string ModelName { get; set; }
        public List<BoneKeyFrame> BoneKeyFrames { get; set; }
        public List<MorphKeyFrame> MorphKeyFrames { get; set; }
        public List<CameraKeyFrame> CameraKeyFrames { get; set; }
        public List<LightKeyFrame> LightKeyFrames { get; set; }
        public List<SelfShadowKeyFrame> SelfShadowKeyFrames { get; set; }
        public List<ModelKeyFrame> ModelKeyFrames { get; set; }

        public Motion()
        {
            BoneKeyFrames = new List<BoneKeyFrame>();
            MorphKeyFrames = new List<MorphKeyFrame>();
            CameraKeyFrames = new List<CameraKeyFrame>();
            LightKeyFrames = new List<LightKeyFrame>();
            SelfShadowKeyFrames = new List<SelfShadowKeyFrame>();
            ModelKeyFrames = new List<ModelKeyFrame>();
        }


        public override void Serialize(MoSerializer archive)
        {
            archive.WriteByteString(Signature, 30);
            archive.WriteByteString(ModelName, 20);
            archive.SerializeList(BoneKeyFrames);
            archive.SerializeList(MorphKeyFrames);
            archive.SerializeList(CameraKeyFrames);
            archive.SerializeList(LightKeyFrames);
            archive.SerializeList(SelfShadowKeyFrames);
            archive.SerializeList(ModelKeyFrames);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            if (archive.ReadByteString(30).TrimNull() != Signature)
                throw new ArgumentException("Unsupported or invalid .vmd file");

            string name = archive.ReadByteString(20);
            ModelName = name == CameraName ? name : name.TrimNull();

            BoneKeyFrames = archive.DeserializeList<BoneKeyFrame>();
            MorphKeyFrames = archive.DeserializeList<MorphKeyFrame>();
            CameraKeyFrames = archive.DeserializeList<CameraKeyFrame>();
            LightKeyFrames = archive.DeserializeList<LightKeyFrame>();
            SelfShadowKeyFrames = archive.DeserializeList<SelfShadowKeyFrame>();
            ModelKeyFrames = archive.DeserializeList<ModelKeyFrame>();
        }
    }
}
