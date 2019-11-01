using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using System.Numerics;
using Scallion.Core;
using Scallion.Raw.Components.Pose;

namespace Scallion.Raw
{
    internal class Pose : IMMDFile<Pose>
    {
        public readonly string Signature = "Vocaloid Pose Data file";

        public string ModelName { get; set; }
        public List<Bone> Bones { get; set; }

        public Pose Load(string path)
        {
            using (var reader = new StreamReader(path, Encoding.GetEncoding("shift_jis")))
            {
                if (Signature != reader.ReadLine())
                    throw new ArgumentException("Unsupported or invalid .vpd file");
                reader.ReadLine();
                ModelName = Regex.Match(reader.ReadLine(), @".*(?=\.osm)").Value;
                int bonesCount = int.Parse(Regex.Match(reader.ReadLine(), @"\d+").Value);
                reader.ReadLine();

                Bones = new List<Bone>(bonesCount);
                for (int i = 0; i < bonesCount; i++)
                {
                    string name = Regex.Match(reader.ReadLine(), @"(?<={).*").Value;
                    float[] pos = Array.ConvertAll(Regex.Match(reader.ReadLine(), @"(?<=\s*).+(?=;)").Value.Split(','), float.Parse);
                    float[] q = Array.ConvertAll(Regex.Match(reader.ReadLine(), @"(?<=\s*).+(?=;)").Value.Split(','), float.Parse);
                    Bones.Add(new Bone()
                    {
                        Name = name,
                        Position = new Vector3(pos[0], pos[1], pos[2]),
                        Quaternion = new Quaternion(q[0], q[1], q[2], q[3])
                    });
                    reader.ReadLine();
                    reader.ReadLine();
                }
            }
            return this;
        }

        public void Save(string path)
        {
            using (var writer = new StreamWriter(path, false, Encoding.GetEncoding("shift_jis")))
            {
                writer.WriteLine(Signature);
                writer.WriteLine();
                writer.WriteLine("{0}.osm;", ModelName);
                writer.WriteLine("{0};", Bones.Count);
                writer.WriteLine();
                for (int i = 0; i < Bones.Count; i++)
                {
                    writer.WriteLine(@"Bone{0}{{{1}", i, Bones[i].Name);
                    writer.WriteLine("  {0},{1},{2};", Bones[i].Position.X, Bones[i].Position.Y, Bones[i].Position.Z);
                    writer.WriteLine("  {0},{1},{2},{3};", Bones[i].Quaternion.X, Bones[i].Quaternion.Y, Bones[i].Quaternion.Z, Bones[i].Quaternion.W);
                    writer.WriteLine("}");
                    writer.WriteLine();
                }
                writer.WriteLine();
            }
        }
    }
}
