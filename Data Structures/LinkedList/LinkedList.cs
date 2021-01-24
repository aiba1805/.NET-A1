using LinkedList;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TechTask
{
    public class LinkedList<T> : IEnumerable<T>, IHybridFlowProcessor<T>
    {
        Node<T> head;
        Node<T> tail;

        public T this[int index]
        {
            get
            {
                var current = head;
                for (var i = 0; i < index && current.Next != null; i++)
                {
                    current = current.Next;
                }
                return current.Data;
            }
            set
            {
                var current = head;
                for (var i = 0; i < index && current.Next != null; i++)
                {
                    current = current.Next;
                }
                current.Data = value;
            }
        }

        public T ElementAt(int index)
        {
            return this[index];
        }

        public void Add(T data)
        {
            var node = new Node<T>(data);

            if (head == null)
                head = node;
            else
            {
                tail.Next = node;
                node.Previous = tail;
            }
            tail = node;
            Length++;
        }

        public void AddAt(int index, T data)
        {
            var current = head;
            for (var i = 0; i < index && current.Next != null; i++)
            {
                current = current.Next;
            }

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
            var current = tail.Previous;
            current.Next = null;
            tail = current;
            Length--;
        }

        public void RemoveAt(int index)
        {
            var current = head;

            for (var i = 0; i < index && current.Next != null; i++)
            {
                current = current.Next;
            }

            if (current == null) throw new ArgumentException("Index out of range");

            if (current.Next != null)
            {
                current.Next.Previous = current.Previous;
            }
            else
            {
                tail = current.Previous;
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
        public ulong Length { get; private set; }
        public bool IsEmpty { get { return Length == 0; } }

        public T Last()
        {
            return tail.Data;
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
            var temp = head.Next;
            var curr = new Node<T>(item);
            curr.Next = head;
            temp.Previous = head;
            head = curr;
            Length++;
        }

        public T Dequeue()
        {
            var temp = head.Next;
            var curr = head;
            temp.Previous = null;
            head = temp;
            return curr.Data;
        }
    }
}