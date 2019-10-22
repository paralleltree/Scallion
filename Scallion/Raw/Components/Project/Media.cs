using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using Scallion.Core;
using Scallion.Internal;

namespace Scallion.Raw.Components.Project
{
    internal class Media : MMDObject
    {
        public Audio Audio { get; set; }
        public Video Video { get; set; }
        public BackgroundImage BackgroundImage { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            Audio.Serialize(archive);
            Video.Serialize(archive);
            BackgroundImage.Serialize(archive);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Audio = archive.Deserialize<Audio>();
            Video = archive.Deserialize<Video>();
            BackgroundImage = archive.Deserialize<BackgroundImage>();
        }
    }

    internal class BackgroundImage : MMDObject
    {
        public Point Position { get; set; }
        public float Scale { get; set; }
        public string Path { get; set; }
        public bool IsVisible { get; set; }


        public override void Serialize(MoSerializer archive)
        {
            archive.Serialize(new PointWrapper(Position));
            archive.WriteSingle(Scale);
            archive.WriteByteString(Path, 256);
            archive.WriteByte((byte)(IsVisible ? 1 : 0));
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Position = archive.Deserialize<PointWrapper>().Value;
            Scale = archive.ReadSingle();
            Path = archive.ReadByteString(256).TrimNull();
            IsVisible = archive.ReadByte() == 1;
        }
    }

    internal class Audio : MMDObject
    {
        public bool IsEnabled { get; set; }
        public string Path { get; set; }


        public override void Serialize(MoSerializer archive)
        {
            archive.WriteByte((byte)(IsEnabled ? 1 : 0));
            archive.WriteByteString(Path, 256);
        }

        public override void Deserialize(MoDeserializer archive)
        {
            IsEnabled = archive.ReadByte() == 1;
            Path = archive.ReadByteString(256).TrimNull();
        }
    }

    internal class Video : MMDObject
    {
        public Point Position { get; set; }
        public float Scale { get; set; }
        public string Path { get; set; }
        public bool IsVisible { get; set; }

        public override void Serialize(MoSerializer archive)
        {
            archive.Serialize(new PointWrapper(Position));
            archive.WriteSingle(Scale);
            archive.WriteByteString(Path, 256);
            archive.WriteInt32((byte)(IsVisible ? 1 : 0));
        }

        public override void Deserialize(MoDeserializer archive)
        {
            Position = archive.Deserialize<PointWrapper>().Value;
            Scale = archive.ReadSingle();
            Path = archive.ReadByteString(256).TrimNull();
            IsVisible = archive.ReadInt32() == 1;
        }
    }
}
