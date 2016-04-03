using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

using NUnit.Framework;
using Newtonsoft.Json;
using Scallion.Core;
using Scallion.Tests.Internal;

namespace Scallion.Tests.UnitTests
{
    [TestFixture]
    public abstract class IOTest<T> where T : IMMDFile<T>, new()
    {
        [Test]
        [TestCaseSource("TestCases")]
        public void LoadingRawBinaryTest(string binaryPath, string fixturePath)
        {
            var expected = JsonConvert.DeserializeObject<T>(File.ReadAllText(fixturePath));
            var actual = new T().Load(binaryPath);
            actual.AssertPropertyValuesAreEquals(expected);
        }

        [Test]
        [Category("UseTempFile")]
        [TestCaseSource("TestCases")]
        public void LoadingSavedBinaryTest(string binaryPath, string fixturePath)
        {
            var expected = JsonConvert.DeserializeObject<T>(File.ReadAllText(fixturePath));
            var source = new T();
            source.Load(binaryPath);
            string tempPath = binaryPath.GetTempPath();
            try
            {
                source.Save(tempPath);
                var restored = new T();
                restored.Load(tempPath);
                restored.AssertPropertyValuesAreEquals(expected);
            }
            finally
            {
                File.Delete(tempPath);
            }
        }

        protected abstract IEnumerable<object[]> TestCases();
    }
}
