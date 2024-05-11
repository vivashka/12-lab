using BaseClassEmoji;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab12_4
{
    public class Point<T> where T : IComparable
    {
        public T? Data { get; set; }

        public sbyte Height { get; set; }

        public Point<T> Left { get; set; }

        public Point<T> Right { get; set; }

        public Point()
        {
            Data = default(T);
            Left = null;
            Right = null;
        }

        public Point(T data)
        {
            Data = data;
            Left = null;
            Right = null;
        }

        public override string ToString()
        {
            return Data == null ? "" : Data.ToString();
        }
        
    }

    public class MyCollection<T> : IEnumerable<T>, ICollection<T> where T : IInit, IComparable, ICloneable, new()
    {
        public Point<T> root = null;

        private int _indexCounter = 0;

        int count = 0;

        public int Count => count;
        public MyCollection() { }

        public MyCollection(int length)
        {
            count = length;
            T data = new T();
            for (int i = 0; i < length; i++)
            {
                data.RandomInit();
                Add(data);
                data = new T();
            }
        }

        public MyCollection(MyCollection<T> collection)
        {
            foreach (T item in collection)
            {
                this.Add((T)item.Clone());
            }
        }

        public bool IsReadOnly => throw new NotImplementedException();

        sbyte Height(Point<T> point)
        {
            return point != null ? point.Height : (sbyte)0;
        }

        sbyte BalanceFactor(Point<T> point)
        {
            return (sbyte)(Height(point.Right) - Height(point.Left));
        }

        //Восстановление корректного значния свойства Height
        void FixedHeight(Point<T> point)
        {
            sbyte hl = Height(point.Left);
            sbyte hr = Height(point.Right);
            point.Height = (sbyte)((hl > hr ? hl : hr) + 1);
        }

        //правый поворот вокруг point
        Point<T> RotateRight(Point<T> point)
        {
            Point<T> temp = point.Left;
            point.Left = temp.Right;
            temp.Right = point;
            FixedHeight(point);
            FixedHeight(temp);
            return temp;
        }

        //левый поворот вокруг point
        Point<T> RotateLeft(Point<T> point)
        {
            Point<T> temp = point.Right;
            point.Right = temp.Left;
            temp.Left = point;
            FixedHeight(temp);
            FixedHeight(point);
            return temp;
        }

        Point<T> Balance(Point<T> point)
        {
            FixedHeight(point);
            sbyte factor = BalanceFactor(point);
            if (factor == 2)
            {
                //Смена позиций родительского и дочернего узла
                if (BalanceFactor(point.Right) < 0)
                    point.Right = RotateRight(point.Right);
                return RotateLeft(point);
            }

            if (factor == -2)
            {
                if (BalanceFactor(point.Left) > 0)
                    point.Left = RotateLeft(point.Left);
                return RotateRight(point);
            }
            return point;
        }

        public void Add(T key)
        {
            root = Balance(Insert(root, key));
        }

        public Point<T> Insert(Point<T> point, T key)
        {
            //T temp = key;
            if (point == null)
            {
                return new Point<T>(key);
            }

            while (point.Data.CompareTo(key) == 0)
            {
                key.RandomInit();
            }

            if (point.Data.CompareTo(key) < 0)
                point.Left = Insert(point.Left, key);
            else
                point.Right = Insert(point.Right, key);

            return Balance(point);
        }

        public void Clear()
        {
            root = null;
            count = 0;
        }

        public bool Contains(T item)
        {
            return FindItem(root, item) != null;
        }

        Point<T> FindItem(Point<T> point, T item)
        {
            if (point == null)
                return null;

            if (point.Data.Equals(item))
            {
                return point;
            }
            else
            {
                if (point.Data.CompareTo(item) < 0)
                    return FindItem(point.Left, item);
                else
                    return FindItem(point.Right, item);
            }
        }

        public bool Remove(T item)
        {
            root = RemoveItem(root, item);
            return FindItem(root, item) == null;
        }

        Point<T> FindMin(Point<T> point)
        {
            return point.Left != null ? FindMin(point.Left) : point; 
        }

        Point<T> RemoveMin(Point<T> point)
        {
            if (point.Left == null)
                return point.Right;

            point.Left = RemoveMin(point.Left);
            return Balance(point);
        }

        Point<T> RemoveItem(Point<T> point, T item)
        {
            if (point == null)
                return null;

            if (point.Data.CompareTo(item) < 0)
            {
                point.Left = RemoveItem(point.Left, item);
            }
            else if(point.Data.CompareTo(item) > 0)
            {
                point.Right = RemoveItem(point.Right, item);
            }
            else
            {
                Point<T> tempLeft = point.Left;
                Point<T> tempRight = point.Right;

                point = null;

                if (tempRight == null)
                {
                    return tempLeft;
                }

                Point<T> current = FindMin(tempRight);
                current.Right = RemoveMin(tempRight);
                current.Left = tempLeft;
                return Balance(current);
            }
            count--;
            return Balance(point);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach(T item in this)
            {
                if (array.Length <= arrayIndex)
                {
                    T[] temp = new T[array.Length*2];
                    for (int i = 0; i < array.Length; i++)
                    {
                        temp[i] = array[i];
                    }
                    array = temp;
                }
                array[arrayIndex] = (T)item.Clone();
                arrayIndex++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator<T>(this).InOrder(this.root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        void Show(Point<T>? point, int spaces)
        {
            if (point != null)
            {
                Show(point.Left, spaces + 10);
                for (int i = 0; i < spaces; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(point.Data);
                Show(point.Right, spaces + 10);
            }
        }

        public void ShowTree()
        {
            Show(root, 0);
        }
    }

    internal class MyEnumerator<T> : IEnumerator<T> where T : IInit, IComparable, ICloneable, new()
    {
        public T Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        public MyEnumerator(MyCollection<T> collection) { }

        public IEnumerable<T> InOrder(Point<T>? node)
        {
            if (node != null)
            {
                foreach (T item in InOrder(node.Left))
                {
                    yield return item;
                }

                yield return node.Data;

                foreach (T item in InOrder(node.Right))
                {
                    yield return item;
                }
            }
        }

        public void Dispose() { }

        public bool MoveNext() => false;

        public void Reset() { }
    }
}
