using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private class Node
        {
            public T Value { get; private set; }
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

        private Node Actual;
        public int Count { get; private set; }
        public HybridFlowProcessor()
        {
            Head = null;
            Tail = null;
            Actual = null;
            Count = 0;
        }
        public T Dequeue()
        {
            if (Head == null)
                throw new InvalidOperationException();

            T data = Head.Value;

            if (Tail == null) 
            {
                Head = null;
                return data;
            }

            Node temp = Tail;

            while (temp.NextNode != null)
                temp = temp.NextNode;

            temp.NextNode = null;
            Head = temp;

            if (Tail == Head)
                Tail = null;

            return data;
        }

        public void Enqueue(T item)
        {
            Node current = Head;
            if (current == null)
                Head = new Node(item);
            else if (Tail == null)
                Tail = new Node(item, Head);
            else
                Tail = new Node(item, Tail);
        }

        public T Pop()
        {
            if (Actual == null)
            {
                throw new InvalidOperationException();
            }
            return Actual.Value;
        }

        public void Push(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            if (Count == 0)
            {
                Head = new Node(item);
                Actual = new Node(item);
                Tail = Head;
                Count++;
            }
            else
            {
                Actual = new Node(item, Tail);
                Tail = new Node(item, Tail); 
                Count++;
            }
        }
    }
}
