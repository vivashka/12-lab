using BaseClassEmoji;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace Lab12_3
{
    internal class BinaryTree<T> where T : IInit, IComparable, ICloneable, new()
    {
        protected Point<T> root = null;

        int count = 0;

        public int Count => count;

        public BinaryTree() { }

        public BinaryTree(int length)
        {
            count = length;
            T data = new T();
            for (int i = 0; i < length; i++)
            {
                data.RandomInit();
                root = Insert(root, data);
            }
        }

        //высота дерева
        sbyte Height(Point<T> point)
        {
            return point != null ? point.Height : (sbyte)0;
        }

        //Вычисление баланса работает только с !null значениями
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

        //Балансировка узла point
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

        public Point<T> Insert(Point<T> point, T key)
        {
            T temp = (T)key.Clone();
            if (point == null)
            {
                return new Point<T>(temp);
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

        public int LeafsAmount()
        {
            int amount = 1;
            NullPoint(root, ref amount);
            return amount;
        }

        void NullPoint(Point<T> point, ref int amount)
        {
            if (point != null)
            {
                if (point.Left != null)
                    NullPoint(point.Left, ref amount);
                if (point.Right != null)
                    NullPoint(point.Right, ref amount);
                if (point.Right != null & point.Left != null)
                    amount++;
            }
            else
            {
                amount = 0;
            }
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

        public void DeleteTree()
        {
            root = null;
            count = 0;
        }


        Point<T> AddPoint(Point<T> point, T key)
        {
            T temp = (T)key.Clone();
            if (point == null)
                return new Point<T>(temp);

            while (point.Data.CompareTo(key) == 0)
            {
                key.RandomInit();
            }

            if (point.Data.CompareTo(key) < 0)
                point.Left = AddPoint(point.Left, key);
            else
                point.Right = AddPoint(point.Right, key);
            return point;
        }

        void TranformToArray(Point<T>? point, T[] array, ref int current)
        {
            if (point != null)
            {
                TranformToArray(point.Left, array, ref current);
                array[current] = (T)point.Data.Clone();
                current++;
                TranformToArray(point.Right, array, ref current);
            }
        }

        public BinaryTree<T> TransformToFindTree()
        {
            T[] array = new T[count];
            int current = 0;

            TranformToArray(root, array, ref current);
            BinaryTree<T> clone = new BinaryTree<T>() { root = new Point<T>((T)array[0].Clone()) };

            for (int i = 1; i < array.Length; i++)
            {
                clone.AddPoint(clone.root, array[i]);
            }
            return clone;
        }
        public void ChangeRoot()
        {
            if (root != null)
            {
                root.Data.RandomInit();
            }
        }
    }
}
