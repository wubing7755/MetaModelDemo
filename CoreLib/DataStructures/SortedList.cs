using System.Collections;

namespace CoreLib.DataStructures;

/// <summary>
/// 二分查找实现有序列表
/// </summary>
public class SortedList<T> : IEnumerable<T>
{
    private readonly IComparer<T> comparer;
    private readonly List<T> memory;

    public SortedList() : this(Comparer<T>.Default)
    {
    }
    
    public int Count => memory.Count;
    
    public SortedList(IComparer<T> comparer)
    {
        this.comparer = comparer;
        memory = new List<T>();
    }

    public void Add(T item)
    {
        var index = IndexFor(item, out _);
        memory.Insert(index, item);
    }

    private int IndexFor(T item, out bool found)
    {
        var left = 0;
        var right = memory.Count;

        while (right - left > 0)
        {
            var mid = (left + right) / 2;

            switch (comparer.Compare(item, memory[mid]))
            {
                case > 0:
                    left = mid + 1;
                    break;
                case < 0:
                    right = mid;
                    break;
                default:
                    found = true;
                    return mid;
            }
        }
        
        found = false;
        return left;
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}