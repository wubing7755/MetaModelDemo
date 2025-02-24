using System.Diagnostics.CodeAnalysis;

namespace CoreLib.DataStructures.LinkedList;

public class SinglyLinkedList<T>
{
    // head
    private SinglyLinkedListNode<T>? Head { get; set; }
    
    // add first
    public SinglyLinkedListNode<T> AddFirst(T data)
    {
        var newListElement = new SinglyLinkedListNode<T>(data)
        {
            Next = Head,
        };
        
        Head = newListElement;
        return newListElement;
    }

    public SinglyLinkedListNode<T> AddLast(T data)
    {
        var newListElement = new SinglyLinkedListNode<T>(data);

        if (Head is null)
        {
            Head = newListElement;
            return newListElement;
        }
        
        var tempElement = Head;

        while (tempElement.Next is not null)
        {
            tempElement = tempElement.Next;
        }
        
        tempElement.Next = newListElement;
        return newListElement;
    }

    public T GetElementByIndex(int index)
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        var tempElement = Head;

        for (var i = 0; tempElement is not null && i < index; i++)
        {
            tempElement = tempElement.Next;
        }

        if (tempElement is null)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        
        return tempElement.Data;
    }

    public IEnumerable<T> GetListData()
    {
        var tempElement = Head;

        while (tempElement is not null)
        {
            yield return tempElement.Data;
            
            tempElement = tempElement.Next;
        }
    }
}

#nullable enable
public class X
{
    [DisallowNull]
    public string? ReviewComment
    {
        get => _comment;
        set => _comment = value ?? throw new ArgumentNullException(nameof(value),
            "Cannot set to null");
    }
    private string? _comment = default;
}
