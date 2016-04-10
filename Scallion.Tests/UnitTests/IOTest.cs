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
    /// <summary>
    /// Tests serialization and deserialization of the <see cref="IMMDFile{T}"/> interface.
    /// This class is abstract.
    /// </summary>
    /// <typeparam name="T">The type to test</typeparam>
    [TestFixture]
    public abstract class IOTest<T> where T : IMMDFile<T>, new()
    {
        /// <summary>
        /// Tests the deserialization process for <paramref name="T"/> by comparing with a expected result.
        /// </summary>
        /// <param name="binaryPath">The path to a binary file to be read</param>
        /// <param name="fixturePath">The path to a json formatted file that the test expects</param>
        [Test]
        [TestCaseSource("TestCases")]
        public void LoadingRawBinaryTest(string binaryPath, string fixturePath)
        {
            var expected = JsonConvert.DeserializeObject<T>(File.ReadAllText(fixturePath));
            var actual = new T().Load(binaryPath);
            actual.AssertPropertyValuesAreEquals(expected);
        }

        /// <summary>
        /// Tests the serialization and deserialization process for <paramref name="T"/> by comparing with a expected result after serialization.
        /// </summary>
        /// <param name="binaryPath">The path to a binary file to be read</param>
        /// <param name="fixturePath">The path to a json formatted file that the test expects</param>
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

        /// <summary>
        /// Enumerates the parameters of test cases.
        /// </summary>
        /// <returns>A collection containing the path to the binary file and the json formatted file of expected results</returns>
        protected abstract IEnumerable<object[]> TestCases();
    }
}
