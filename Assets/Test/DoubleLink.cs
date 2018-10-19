// using System;
// using System.Collections;
// using System.Collections.Generic;

// namespace Test
// {
//     public class DoubleLink<T> : IEnumerable<T>
//     {
//         private class DoubleLinkEnumerator : IEnumerator<T>
//         {
//             private Node<T> head = null;
//             private Node<T> curNode = null;

//             public T Current
//             {
//                 get
//                 {
//                     return curNode.Value;
//                 }
//             }

//             object IEnumerator.Current =>  curNode.Value;

//             public DoubleLinkEnumerator(Node<T> head)
//             {
//                 this.head = new Node<T>(head.Value, head.Prev, head.Next);
//                 this.curNode = this.head;
//             }

//             public void Dispose()
//             {
//                 head = null;
//                 curNode = null;
//             }

//             public bool MoveNext()
//             {
//                 curNode = curNode.Next;
//                 return curNode != head;
//             }

//             public void Reset()
//             {
//                 head = null;
//                 curNode = null;
//             }
//         }

//         private class Node<TNode>
//         {
//             public Node<T> Prev;
//             public Node<T> Next;
//             public T Value;
//             public Node(T value, Node<T> prev, Node<T> next)
//             {
//                 Value = value;
//                 Prev = prev;
//                 Next = next;
//             }
//         }

//         private Node<T> headNode;
//         public int Count { get; private set; }

//         public DoubleLink()
//         {
//             headNode = new Node<T>(default(T), null, null);
//             headNode.Prev = headNode.Next = headNode;
//             Count = 0;
//         }

//         public bool IsEmpty()
//         {
//             return Count == 0;
//         }

//         public T GetByIndex(int index)
//         {
//             return GetNode(index).Value;
//         }

//         public T GetFirst()
//         {
//             return GetNode(0).Value;
//         }

//         public T GetLast()
//         {
//             return GetNode(Count - 1).Value;
//         }

//         public void Insert(int index, T value)
//         {
//             Node<T> node = GetNode(index);
//             Node<T> newNode = new Node<T>(value, node.Prev, node);
//             node.Prev.Next = newNode;
//             node.Prev = newNode;
//             Count++;
//         }

//         public void Append(T value)
//         {
//             Node<T> node = GetNode(Count - 1);
//             Node<T> newNode = new Node<T>(value, node, headNode);
//             node.Next = newNode;
//             headNode.Prev = newNode;
//             Count++;
//         }

//         public void Remove(int index)
//         {
//             Node<T> node = GetNode(index);
//             node.Prev.Next = node.Next;
//             node.Next.Prev = node.Prev;
//             Count--;
//             node = null;
//         }

//         public void RemoveLast()
//         {
//             Remove(Count - 1);
//         }

//         private Node<T> GetNode(int index)
//         {
//             if (index < 0 || index >= Count)
//                 throw new IndexOutOfRangeException();

//             Node<T> node = null;
//             if (index < Count / 2)
//             {
//                 node = headNode.Next;
//                 for (int i = 0; i <= index; i++)
//                 {
//                     node = node.Next;
//                 }
//             }
//             else
//             {
//                 node = headNode.Prev;
//                 for (int i = Count; i > index; i--)
//                 {
//                     node = node.Prev;
//                 }
//             }
//             return node;
//         }

//         public IEnumerator<T> GetEnumerator()
//         {
//             throw new NotImplementedException();
//         }

//         IEnumerator IEnumerable.GetEnumerator()
//         {
//             throw new NotImplementedException();
//         }
//     }


// }