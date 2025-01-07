using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Base;

// Summary:
//     dynamic array
public class CList<T>
{
    T[] _list;

    // Summary:
    //     the actual item
    int _count;

    public CList(int capacity)
    {
        _list = new T[capacity];

        _count = 0;
    }

    // Summary:
    //     Indexer
    public T this[int index]
    {
        get { return _list[index]; }

        set { _list[index] = value; }
    }

    public T AccessValue(int index)
    {
        return _list[index];
    }

    public void AddValue(T value)
    {

    }
}
