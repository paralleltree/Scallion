using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;

using NUnit.Framework;

namespace Scallion.Tests.Internal
{
    internal static class HelperExtensions
    {
        public static string GetTempPath(this string sourcePath, string suffix = "temp")
        {
            return new Regex(@"(?<=([^\\]+\\)+[^.]*(\.[^.]*)*)(?=(\.[^.\\]+)?$)", RegexOptions.Compiled)
                .Replace(sourcePath, "-" + suffix, 1);
        }

        public static void AssertPropertyValuesAreEquals(this object actual, object expected)
        {
            var properties = expected.GetType().GetProperties().Where(p => p.CanWrite);
            foreach (PropertyInfo property in properties)
            {
                object expectedValue = property.GetValue(expected, null);
                object actualValue = property.GetValue(actual, null);

                if (actualValue is ICollection)
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
