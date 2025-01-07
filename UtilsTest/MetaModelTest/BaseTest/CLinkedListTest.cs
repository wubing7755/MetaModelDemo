using CoreLib.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace UtilsTest.MetaModelTest.BaseTest;

public class CLinkedListTest : TestBase
{
    public CLinkedListTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void TestCLinkedList()
    {
        CLinkedList<int> linkedList = new CLinkedList<int>();

        // total insert six nodes,
        // the value is 0, 1, 2, 3, 4, 5
        for (int i = 5; i >= 0; i--)
        {
            linkedList.InsertNode(i);
        }

        Assert.Equal(3, linkedList.FindNode(3));
        _output.WriteLine("The value {0}'s index is {1}", 3, linkedList.FindNode(3));

        Assert.Equal(3, linkedList.AccessNode(3).Value);
        _output.WriteLine("The value {0}'s index is {1}", linkedList.AccessNode(3).Value, 3);

        Assert.Equal(3, linkedList.DeleteNode(3));
        _output.WriteLine("The deleted value {0}'s index is {1}", 3, 3);
    }
}
