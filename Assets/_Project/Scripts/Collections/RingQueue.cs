using UnityEditor;
using UnityEngine;

namespace Game.Collections
{
    public sealed class RingQueue<T>
    {
        private T[] _buffer;
        private int _head;
        private int _tail;
        private int _count;
        public int Count => _count;
        public RingQueue(int capacity)
        {
            _buffer = new T[capacity];
            _head = 0;
            _tail = 0;
            _count = 0;
        }
        public void Enqueue(T item)
        {
            if(_count == _buffer.Length)
            {
                Grow();
            }
            _buffer[_tail] = item;
            _tail = (_tail + 1) % _buffer.Length;
            _count++;
        }
        public T Dequeue()
        {
            if (_count == 0) return default;
            T item = _buffer[_head];
            _buffer [_head] = default;
            _head = (_head + 1) % _buffer.Length;
            _count--;
            return item;
        }
        private void Grow()
        {
            int newCapacity = _buffer.Length * 2;
            T[] newBuffer = new T[newCapacity];
            for (int i = 0; i < _count; i++)
            {
                newBuffer[i] = _buffer[(_head + i) % _buffer.Length];
            }
            _buffer = newBuffer;
            _head = 0;
            _tail = _count;
        }
    }
}
