using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace UtilsTest;

public abstract class TestBase
{
    protected readonly ITestOutputHelper _output;

    protected TestBase(ITestOutputHelper testOutputHelper)
    {
        _output = testOutputHelper;
    }
}
