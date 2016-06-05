using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using Scallion.DomainModels;
using Scallion.DomainModels.Components;

namespace Scallion.Tests.UnitTests.Core
{
    [TestFixture]
    internal class MoDeserializerTest
    {
        [Test]
        public void WriteLongStringTest()
        {
            Assert.DoesNotThrow(() =>
            {
                var m = new Motion()
                {
                    ModelName = "テストモデル"
                };

                // Add a bone whose name length is longer than the maximum.
                m.Bones.Add(new Bone()
                {
                    Name = "テストモデルのテストボーン",
                    KeyFrames = new List<BoneKeyFrame>() { new BoneKeyFrame() }
                });

                // Then, try to save it.
                m.Save("out.vmd");
            }, "An error occurred while saving a Motion.");
        }
    }
}
