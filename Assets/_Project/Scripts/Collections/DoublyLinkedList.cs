using UnityEngine;
namespace Game.Collections
{
    public class DLNode<T>
    {
        public T Value;
        public DLNode<T> Next;
        public DLNode<T> Prev;
        public DLNode(T value)
        {
            Value = value;
        }
    }
    public sealed class DoublyLinkedList<T>
    {
        public DLNode<T> Head;
        public DLNode<T> Tail;
        public int Count;
        public DLNode<T> AddLast(T value)
        {
            DLNode<T> newNode = new DLNode<T>(value);
            if (Head == null)
            {
                Head = newNode;
                Tail = newNode;
            }
            else
            {
                Tail.Next = newNode;
                newNode.Prev = Tail;
                Tail = newNode;
            }
            Count++;
            return newNode;
        }
        public void Remove(DLNode<T> node)
        { 
            if(node.Prev != null)
            {
                node.Prev.Next = node.Next;
            }
            else
            {
                Head = node.Next;
            }
            if (node.Next != null)
            {
                node.Next.Prev = node.Prev;
            }
            else
            {
                Tail = node.Prev;
            }
            node.Next = null;
            node.Prev = null;
            Count--;
        }
    }
}