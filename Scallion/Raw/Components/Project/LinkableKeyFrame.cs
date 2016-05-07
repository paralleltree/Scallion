using System;
using System.Collections.Generic;
using System.Linq;

using Scallion.Core;

namespace Scallion.Raw.Components.Project
{
    /// <summary>
    /// Represents a key frame used in MMD Project File.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class LinkableKeyFrame<T> : KeyFrame where T : MMDObject, new()
    {
        public int DataIndex { get; set; }
        public int PreviousDataIndex { get; set; }
        public int NextDataIndex { get; set; }
        public bool IsInitialKeyFrame { get; set; }
        public bool IsSelected { get; set; }
        public T Value { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            if (!IsInitialKeyFrame) archive.WriteInt32(DataIndex);
            archive.WriteInt32(KeyFrameIndex);
            archive.WriteInt32(PreviousDataIndex);
            archive.WriteInt32(NextDataIndex);
            SerializeKeyFrame(archive);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            if (!IsInitialKeyFrame) DataIndex = archive.ReadInt32();
            KeyFrameIndex = archive.ReadInt32();
            PreviousDataIndex = archive.ReadInt32();
            NextDataIndex = archive.ReadInt32();
            DeserializeKeyFrame(archive);
        }

        /// <summary>
        /// When overridden in a derived class, serializes this key frame except its metadata by its own way.
        /// </summary>
        /// <param name="archive">The instance of <see cref="MoSerializer"/> to serialize</param>
        internal virtual void SerializeKeyFrame(MoSerializer archive)
        {
            SerializeKeyFrameValue(archive);
            archive.WriteByte((byte)(IsSelected ? 1 : 0));
        }

        /// <summary>
        /// When overriden in a derived class, deserializes this key frame except its metadata by its own way.
        /// </summary>
        /// <param name="archive">The instance of <see cref="MoDeserializer"/> to deserialize</param>
        internal virtual void DeserializeKeyFrame(MoDeserializer archive)
        {
            DeserializeKeyFrameValue(archive);
            IsSelected = archive.ReadByte() == 1;
        }

        /// <summary>
        /// When overridden in a derived class, serializes the value of this key frame by its own way.
        /// </summary>
        /// <param name="archive">The instance of <see cref="MoSerializer"/> to serialize</param>
        internal virtual void SerializeKeyFrameValue(MoSerializer archive)
        {
            archive.Serialize(Value);
        }

        /// <summary>
        /// When overriden in a derived class, deserializes the value of this key frame by its own way.
        /// </summary>
        /// <param name="archive">The instance of <see cref="MoDeserializer"/> to deserialize</param>
        internal virtual void DeserializeKeyFrameValue(MoDeserializer archive)
        {
            Value = archive.Deserialize<T>();
        }
    }
}
