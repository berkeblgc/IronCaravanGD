using UnityEngine;

namespace Game.Collections
{
    public sealed class ArrayStack<T>
    {
        private readonly T[] _items;
        private int _top;

        public int Count => _top;
        public ArrayStack(int capacity)
        {
            _items = new T[capacity];
            _top = 0; 
        }
        public void Push(T item)
        {
            if(_top == _items.Length)
            {
                return;
            }
            _items[_top] = item;
            _top++;
        }
        public bool TryPop(out T item)
        {
            if(_top == 0)
            {
                item = default;
                return false;
            }
            _top--;
            item = _items[_top];
            _items[_top] = default;
            return true;
        }
        public void Clear()
        {
            _top = 0;
        }
    }
}