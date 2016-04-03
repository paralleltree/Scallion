using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Scallion.DomainModels;

namespace Scallion.Tests.UnitTests.DomainModels
{
    internal class MotionTest : IOTest<Motion>
    {
        protected override IEnumerable<object[]> TestCases()
        {
            return Directory.EnumerateFiles(@"Resources\Binaries", "*.vmd")
                .Select(p =>
                {
                    string name = Path.GetFileNameWithoutExtension(p);
                    return new object[]
                    {
                        string.Format(@"Resources\Binaries\{0}.vmd",name),
                        string.Format(@"Resources\Fixtures\DomainModels\{0}.json",name)
                    };
                });
        }
    }
}
