using LinkedList;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TechTask
{
    public class LinkedList<T> : IEnumerable<T>, IHybridFlowProcessor<T>
    {
        Node<T> head;

        public T this[int index]
        {
            get
            {
                var current = GetCurrent(index);
                return current.Data;
            }
            set
            {
                var current = GetCurrent(index);
                current.Data = value;
            }
        }

        public T ElementAt(int index)
        {
            return this[index];
        }

        public void Add(T data)
        {
            AddAt(Length + 1, data);
        }

        private Node<T> GetCurrent(int index)
        {
            var current = head;
            for (var i = 0; i < index && current.Next != null; i++)
            {
                current = current.Next;
            }
            return current;
        }

        public void AddAt(int index, T data)
        {
            var current = GetCurrent(index);

            var node = new Node<T>(data);
            var prev = current.Previous;
            if (prev != null)
            {
                prev.Next = node;
                node.Previous = prev;
            }
            node.Next = current;
            current.Previous = node;
            Length++;
        }

        public void Remove()
        {
            RemoveAt(Length);
        }

        public void RemoveAt(int index)
        {
            var current = GetCurrent(index);

            if (current == null) throw new ArgumentException("Index out of range");

            if (current.Next != null)
            {
                current.Next.Previous = current.Previous;
            }

            if (current.Previous != null)
            {
                current.Previous.Next = current.Next;
            }
            else
            {
                head = current.Next;
            }
            Length--;
        }
        private int Length { get; set; }
        public bool IsEmpty { get { return Length == 0; } }

        public T Last()
        {
            return GetCurrent(Length).Data;
        }

        private sealed class __ListEnumerator<T> : IEnumerator<T>
        {
            public __ListEnumerator(LinkedList<T> linkedList)
            {
                _LinkedList = linkedList;
                _CurrentNode = _LinkedList.head;
                _Current = _CurrentNode.Data;
            }
            LinkedList<T> _LinkedList;
            T _Current;
            Node<T> _CurrentNode;
            T IEnumerator<T>.Current
            {
                get
                {
                    return _Current;
                }
            }

            public object Current
            {
                get
                {
                    return _Current;
                }
            }

            public bool MoveNext()
            {
                if (_CurrentNode.Next != null)
                {
                    _CurrentNode = _CurrentNode.Next;
                    _Current = _CurrentNode.Data;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                _CurrentNode = _LinkedList.head;
            }

            public void Dispose()
            {
                _LinkedList = null;
            }
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            var result = new __ListEnumerator<T>(this);
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Push(T item)
        {
            Add(item);
        }

        public T Pop()
        {
            var curr = Last();
            Remove();
            return curr;
        }

        public void Enqueue(T item)
        {
            AddAt(1, item);
        }

        public T Dequeue()
        {
            return Pop();
        }
    }
}