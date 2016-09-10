using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Scallion.DomainModels;

namespace Scallion.Tests.UnitTests.DomainModels
{
    internal class PoseTest : IOTest<Pose>
    {
        protected override IEnumerable<object[]> TestCases()
        {
            return Directory.EnumerateFiles(@"Resources\Binaries", "*.vpd")
                .Select(p =>
                {
                    string name = Path.GetFileNameWithoutExtension(p);
                    return new object[]
                    {
                        string.Format(@"Resources\Binaries\{0}.vpd", name),
                        string.Format(@"Resources\Fixtures\DomainModels\{0}.json", name)
                    };
                });
        }
    }
}
