using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Scallion.DomainModels.Components;

namespace Scallion.Tests.UnitTests.DomainModels
{
    [TestFixture]
    internal class InterpolationTest
    {
        private object[] interpolationParams = new[]
        {
            new object[] { new InterpolationParameter(20, 20), new InterpolationParameter(107, 107), 0.2f, 0.2f },
            new object[] { new InterpolationParameter(20, 20), new InterpolationParameter(107, 107), 0.7f, 0.7f },
            new object[] { new InterpolationParameter(84, 84), new InterpolationParameter(10, 108), 0.3f, 0.402165f },
            new object[] { new InterpolationParameter(84, 84), new InterpolationParameter(10, 108), 0.4f, 0.684518f },
        };

        [Test]
        [TestCaseSource("interpolationParams")]
        public void CalculateInterpolatedValueTest(InterpolationParameter first, InterpolationParameter second, float t, float y)
        {
            var interpolation = new Interpolation(first, second);
            Assert.AreEqual(y, interpolation.GetInterpolatedValue(t), 1e-5);
        }
    }
}
