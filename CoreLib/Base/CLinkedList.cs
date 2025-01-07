namespace CoreLib.Base;

//
// Summary:
//     LinkedList<T>
//
public class CLinkedList<T>
{
    // Summary:
    //     virtual head node
    private readonly CLinkedListNode<T> _head;

    // Summary:
    //     the structure of the linked list
    public CLinkedList()
    {
        _head = new CLinkedListNode<T>(default(T));
    }

    // Summary:
    //     Insert the node to the linked list
    public void InsertNode(T value)
    {
        CLinkedListNode<T> newNode = new CLinkedListNode<T>(value);

        newNode.Next = _head.Next;
        _head.Next = newNode;
    }

    // Summary:
    //     Delete the node which the value is equal to the parameter
    // Return:
    //     The deleted node's index
    public int DeleteNode(T value)
    {
        CLinkedListNode<T>? curr = _head;

        // the virtual head node's index is -1
        int index = -1;

        while (curr.Next != null)
        {
            index++;

            if (EqualityComparer<T>.Default.Equals(curr.Next.Value, value))
            {
                curr.Next = curr.Next.Next;
                return index;
            }

            curr = curr.Next;
        }

        return -1;
    }

    // Summary:
    //     Access the linked node
    public CLinkedListNode<T> AccessNode(int index)
    {
        CLinkedListNode<T>? curr = _head.Next;

        for (int i = 0; curr != null && i < index; i ++)
        {
            curr = curr.Next;
        }

        return curr;
    }

    // Summary:
    //     Find the node which value is equal to the parameter
    // Return:
    //     The index
    public int FindNode(T value)
    {
        CLinkedListNode<T>? curr = _head.Next;
        int index = 0;

        while (curr != null)
        {
            if (EqualityComparer<T>.Default.Equals(curr.Value, value))
                return index;

            curr = curr.Next;
            index ++;
        }

        return -1;
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