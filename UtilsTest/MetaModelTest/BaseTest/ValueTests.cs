
using Xunit.Abstractions;

namespace UtilsTest.MetaModelTest.BaseTest;

public class ValueTests : TestBase
{
    public ValueTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) {}

    [Fact]
    public void AllocateFixedStack()
    {
        int length = 3;

        // Span 摘要：
        // 为任意内存的连续区域提供类型和内存安全表示。
        // 内存的连续区域。

        Span<int> numbers = stackalloc int[length];

        for (var i = 0; i < length; i++)
        {
            numbers[i] = i;
        }
    }

    [Fact]
    public void AllocateStackInExpression()
    {
        int length = 1000;

        Span<byte> buffer = length <= 1024 ? stackalloc byte[length] : new byte[length];
    }

    [Fact]
    public void AllocateStackUsingReadOnlySpan()
    {
        Span<int> numbers = stackalloc[] {1, 2, 3, 4, 5, 6};
        var ind = numbers.IndexOfAny(stackalloc[] { 2, 4, 6, 8 });
        Assert.Equal(1, ind);

        Span<int> numbers2 = new int[] { 1, 2, 3, 4, 5, 6 };
        var ind2 = numbers2.IndexOfAny(new int[] { 2, 4, 6, 8 });
        Assert.Equal(1, ind2);
    }

    [Fact]
    public void AllocateStackUnsafe()
    {
        unsafe
        {
            int length = 3;

            int* numbers = stackalloc int[length];

            for(var i = 0; i < length; i ++)
            {
                numbers[i] = i;
            }
        }

        int inputLength = 1000;

        const int MaxStackLimit = 1024;
        Span<byte> buffer = inputLength <= MaxStackLimit ? stackalloc byte[MaxStackLimit] : new byte[inputLength];

        Span<int> first = stackalloc int[3] { 1, 2, 3 };
        Span<int> second = stackalloc int[] {1, 2, 3 };
        ReadOnlySpan<int> third = stackalloc[] { 1, 2, 3 };

        Span<int> fourth = new int[] {1, 2, 3 };
        ReadOnlySpan<int> fifth = new int[] { 1, 2, 3 };
    }

    /// <summary>
    /// Address-of 运算符 &amp;
    /// </summary>
    [Fact]
    public void AddressOperator()
    {
        unsafe
        {
            int number = 27;
            int* pointerToNumber = &number;

            Assert.Equal(27, number);
            Console.WriteLine($"Address num:{(long)pointerToNumber:X}");

            byte[] bytes = new byte[] { 1, 2, 3 };
            fixed(byte* pointerToFirst = &bytes[0])
            {
                Console.WriteLine($"Address num:{(long)pointerToFirst:X}");
            }
        }
    }
}
