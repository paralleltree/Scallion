using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using System.Numerics;
using Scallion.Tests.Internal;

namespace Scallion.Tests.ConverterTests.Project
{
    internal class CameraTest
    {
        private object[] cameraViewPositionTestSource = new[]
        {
            new object[] { new Vector3(0, 10, 5), 45, new Vector3(90, 0, 0), new Vector3(0, 55, 5), new Vector3(0, 5, -55) },
            new object[] { new Vector3(5, 10, -5), 45, new Vector3(45, 135, -90), new Vector3(27.5f, 41.8198f, 17.5f), new Vector3(7.07f, 7.07f, -52.07f) },
            new object[] { new Vector3(5, 5, 5), 25, new Vector3(45, 90, 135), new Vector3(22.677f, 22.677f, 5), new Vector3(-3.5355f, 3.5355f, -32.071f) },
            new object[] { new Vector3(5, 5, 10), -25, new Vector3(30, 90, 135), new Vector3(-16.65f, -7.5f, 10), new Vector3(-8.365f, 5.7769f, 18.1698f) },
            new object[] { new Vector3(-10, 10, 20), 45, new Vector3(-90, 180, 270), new Vector3(-10, -35, 20), new Vector3(20, -10, -35) },
            new object[] { new Vector3(0, 10, 0), 45, new Vector3(0, 90, 0), new Vector3(45, 10, 0), new Vector3(0, 10, -45) },
            new object[] { new Vector3(0, 10, 0), 45, new Vector3(0, 0, 90), new Vector3(0, 10, -45), new Vector3(-10, 0, -45) },
        };
        [Test]
        [TestCaseSource("cameraViewPositionTestSource")]
        public void ModelViewPositionTest(Vector3 centerPos, float dist, Vector3 ang, Vector3 worldPos, Vector3 localPos)
        {
            var raw = new Raw.Components.Project.Camera()
            {
                CurrentStatus = new Raw.Components.Project.CurrentCameraState()
                {
                    CenterPosition = centerPos,
                    Rotation = ang.ToRad(),
                    OffsetPosition = localPos
                },
                InitialKeyFrame = new Raw.Components.Project.CameraKeyFrame()
                {
                    Interpolation = new Raw.Components.Project.CameraInterpolationImpl(),
                    Value = new Raw.Components.Project.CameraState()
                },
                KeyFrames = new List<Raw.Components.Project.CameraKeyFrame>()
            };
            var conv = new Scallion.Internal.Converters.Project.CameraConverter(true);
            var dest = conv.Convert(raw);
            Assert.AreEqual(dist, dest.CurrentStatus.Distance, 0.005f);
            localPos.AssertEquals(conv.ConvertBack(dest).CurrentStatus.OffsetPosition, 0.005f);
        }

        [Test]
        public void CameraViewPositionTest()
        {
            var raw = new Raw.Components.Project.Camera()
            {
                CurrentStatus = new Raw.Components.Project.CurrentCameraState()
                {
                    CenterPosition = new Vector3(0, 10, 0),
                    Rotation = new Vector3(0, 0, 0).ToRad(),
                    OffsetPosition = new Vector3(0, 0, -30)
                },
                InitialKeyFrame = new Raw.Components.Project.CameraKeyFrame()
                {
                    Interpolation = new Raw.Components.Project.CameraInterpolationImpl(),
                    Value = new Raw.Components.Project.CameraState()
                },
                KeyFrames = new List<Raw.Components.Project.CameraKeyFrame>()
            };
            var conv = new Scallion.Internal.Converters.Project.CameraConverter(false);
            var dest = conv.Convert(raw);
            Assert.AreEqual(30, dest.CurrentStatus.Distance);
            Assert.AreEqual(raw.CurrentStatus.OffsetPosition, conv.ConvertBack(dest).CurrentStatus.OffsetPosition);
        }
    }
}
