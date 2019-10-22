using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;

using NUnit.Framework;

namespace Scallion.Tests.Internal
{
    /// <summary>
    /// Provides a set of static methods for test assertions.
    /// </summary>
    internal static class HelperExtensions
    {
        /// <summary>
        /// Inserts a temporary string into the end of this file path.
        /// </summary>
        /// <param name="sourcePath">The file path that the <paramref name="suffix"/> is inserted</param>
        /// <param name="suffix">The string to be inserted. By default, this value is "temp"</param>
        /// <returns>A new string that the <paramref name="suffix"/> is inserted</returns>
        public static string GetTempPath(this string sourcePath, string suffix = "temp")
        {
            return new Regex(@"(?<=([^\\]+\\)+[^.]*(\.[^.]*)*)(?=(\.[^.\\]+)?$)", RegexOptions.Compiled)
                .Replace(sourcePath, "-" + suffix, 1);
        }

        /// <summary>
        /// Asserts properties equality recursively between specified objects.
        /// The assertion fails if their properties do not equal each other.
        /// </summary>
        /// <param name="actual">The object to be compared</param>
        /// <param name="expected">The object that the test expects</param>
        public static void AssertPropertyValuesAreEquals(this object actual, object expected)
        {
            var properties = expected.GetType().GetProperties().Where(p => p.CanWrite);
            foreach (PropertyInfo property in properties)
            {
                object expectedValue = property.GetValue(expected, null);
                object actualValue = property.GetValue(actual, null);

                if (actualValue == null)
                    Assert.Null(expectedValue);
                else if (actualValue is ICollection)
                    // Comparison for elements contained in the list
                    property.AssertListsAreEquals((ICollection)actualValue, (ICollection)expectedValue);
                else if (!actualValue.GetType().IsValueType && !actualValue.GetType().Namespace.StartsWith("System"))
                    // Recursively comparison for not built-in class
                    actualValue.AssertPropertyValuesAreEquals(expectedValue);
                else if (!actualValue.Equals(expectedValue))
                    // Comparison for definitely comparable object
                    Assert.Fail("Property {0}.{1} does not match. Expected: {2} but was: {3}.",
                        property.DeclaringType.Name, property.Name, expectedValue, actualValue);
            }
        }

        /// <summary>
        /// Asserts an equality of the specified collections.
        /// The assertion fails if their elements do not equal each other.
        /// </summary>
        /// <param name="property">The property having a collection to compare</param>
        /// <param name="actualList">The list to be compared</param>
        /// <param name="expectedList">The list that the test expects</param>
        private static void AssertListsAreEquals(this PropertyInfo property, ICollection actualList, ICollection expectedList)
        {
            if (actualList.Count != expectedList.Count)
                Assert.Fail("Property {0}.{1} does not match. Expected ICollection containing {2} elements but was ICollection containing {3} elements.",
                    property.PropertyType.Name, property.Name, expectedList.Count, actualList.Count);

            var actual = actualList.GetEnumerator();
            var expected = expectedList.GetEnumerator();
            while (actual.MoveNext() && expected.MoveNext())
            {
                actual.Current.AssertPropertyValuesAreEquals(expected.Current);
            }
        }

        public static void AssertEquals(this System.Numerics.Vector3 expected, System.Numerics.Vector3 actual, float delta)
        {
            Assert.AreEqual(expected.X, actual.X, delta);
            Assert.AreEqual(expected.Y, actual.Y, delta);
            Assert.AreEqual(expected.Z, actual.Z, delta);
        }
    }

    [TestFixture]
    internal class HelperExtensionsTest
    {
        [Test]
        [TestCase(@"C:\Projects\Foo\bin\test.exe", @"C:\Projects\Foo\bin\test-temp.exe")]
        [TestCase(@"C:\Projects\Foo\bin\bar", @"C:\Projects\Foo\bin\bar-temp")]
        [TestCase(@"C:\Projects\Foo.old\bin\bar", @"C:\Projects\Foo.old\bin\bar-temp")]
        [TestCase(@"C:\Projects\Foo.old\bin\test.exe", @"C:\Projects\Foo.old\bin\test-temp.exe")]
        [TestCase(@"C:\Projects\Foo.old\bin\test.old.exe", @"C:\Projects\Foo.old\bin\test.old-temp.exe")]
        public void GetTempPathTest(string sourcePath, string expectedPath)
        {
            Assert.AreEqual(expectedPath, sourcePath.GetTempPath());
        }
    }
}
