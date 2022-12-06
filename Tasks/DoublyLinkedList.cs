using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private class Node
        {
            public T Value { get;  set; }
            public Node NextNode { get; set; }
            public Node(T element)
            {
                Value = element;
                NextNode = null;
            }
            public Node(T element, Node previousNode) : this(element)
            {
                previousNode.NextNode = this;
                NextNode = null;
            }
        }
        private Node Head;

        private Node Tail;
        public int Count { get; private set; }
        public DoublyLinkedList()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }
        public int Length => Count;
        public void Add(T e)
        {
            if (e == null)
            {
                throw new ArgumentNullException();
            }
            if (Count == 0)
            {
                Head = new Node(e);
                Tail = Head;
                Count++;
            }
            else
            {
                Tail = new Node(e, Tail);
                Count++;
            }
        }

        public void AddAt(int index, T e)
        {
            if (e == null)
            {
                throw new ArgumentNullException();
            }
            if (index == 0)
            {
                Head = new Node(e);
                Tail = Head;
                Count++;
            }
            else
            {
                Node current = Head;
                for (int i = 0; i <= index; i++)
                {
                    if (index == i)
                    {
                        if (current == null)
                        {
                            Tail = new Node(e, Tail);
                            Count++;
                            break;
                        }
                        current.Value = e;
                        if (current.NextNode == null)
                        {
                            Tail = current;
                            break;
                        }
                    }
                    current = current.NextNode;
                }
            }

        }

        public T ElementAt(int index)
        {
            Node current = Head;

            if (current == null || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (index == 0)
            {
                return current.Value;
            }

            for (int i = 1; i <= index; i++)
            {
                current = current.NextNode;
            }

            if (current == null)
            {
                throw new IndexOutOfRangeException();
            }

            return current.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T temp in this)
            {
                yield return temp;
            }
        }

        public void Remove(T item)
        {
            var currentNode = Head;
            if (item != null || Count != 0)
            {
           
                while (!Equals(currentNode.Value, item))
                {
                    if (currentNode.NextNode == null)
                    {
                        break;
                    }
                    currentNode = currentNode.NextNode;
                }

                if (!Equals(currentNode, null))
                {
                    if (currentNode == Head)
                    {
                        Head = currentNode.NextNode;
                        Count--;
                    }else if (!Equals(currentNode.Value, item))
                    {

                    }
                    else
                    {
                        Count--;
                    }
                }

            }

        }

        public T RemoveAt(int index)
        {
            var currentNode = Head;

            if (index < 0 || currentNode == null)
            {
                throw new IndexOutOfRangeException();
            }
            var value = currentNode.Value;
            if (index == 0)
            {
                Head = currentNode.NextNode;
                Count--;
                return currentNode.Value;
            }
            else
            {
                for (int i = 1; i <= index; i++)
                {
                    
                    if (index == i)
                    {
                        var nodeBeforeFoundNode = currentNode;
                        currentNode = currentNode.NextNode;
                        value = currentNode.Value;
                        nodeBeforeFoundNode.NextNode = currentNode.NextNode;
                        Count--;
                        break;
                    }

                    currentNode = currentNode.NextNode;
                    if (currentNode.NextNode == null)
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
            }

            return value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var node = Head;
            while (node != null)
            {
                yield return node.Value;
                node = node.NextNode;
            }
        }
    }
}
