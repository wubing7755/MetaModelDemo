namespace CoreLib.Base;

public class CLinkedList<T>
{
    private readonly CLinkedListNode<T> _head;

    public CLinkedList()
    {
        _head = new CLinkedListNode<T>(default(T));

        _head.Next = null;
    }

    public void InsertNode(T value)
    {
        CLinkedListNode<T> node = new CLinkedListNode<T>(value);

        node.Next = _head.Next;
        _head.Next = node;
    }

    public void DeleteNode(T value)
    {
        CLinkedListNode<T>? curr = _head;

        while(curr.Next != null)
        {
            if(curr.Next.Value!.Equals(value))
            {
                curr.Next = curr.Next.Next;
            }

            curr = curr.Next;
        }
    }
}

public class CLinkedListNode<T>
{
    public T Value;

    public CLinkedListNode<T>? Next;

    public CLinkedListNode(T value)
    {
        Value = value;
    }
}