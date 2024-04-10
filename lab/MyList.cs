using BaseClassEmoji;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    internal class MyList<T> where T : IInit, ICloneable, new()
    {
        Point<T>? beg = null;
        Point<T>? end = null;

        int _count = 0;

        public int Count => _count;

        public Point<T> MakeRandomData()
        {
            T data = new();
            data.RandomInit();
            return new Point<T>(data);
        }

        public MyList() { }

        public MyList(int size)
        {
            if (size <= 0) throw new Exception("Размер меньше нуля");
            beg = MakeRandomData();
            end = beg;
            for (int i = 1; i < size; i++)
            {
                T newItem = MakeRandomItem();
                AddToEnd(newItem);
            }
        }

        public MyList(T[] collection)
        {
            if (collection == null) throw new Exception("Размер меньше нуля");
            
            if (collection.Length == 0) throw new Exception("Коллекция пуста");

            T newData = (T)collection.Clone();
            beg = new Point<T>(newData);
            end = beg;

            for (int i = 0; i < collection.Length; i++)
            {
                AddToEnd(collection[i]);
            }
        }

        public Point<T> FindItem(T item)
        {
            Point<T> current = beg;
            while (current != null)
            {
                if (current.Data == null)
                    throw new Exception("Пустые данны");

                if (current.Data.Equals(item))
                    return current;

                current = current.Next;
            }
            return null;
        }

        public bool RemoveItem(T item)
        {
            if (beg == null) throw new Exception("Пустой список");

            Point<T>? pos = FindItem(item);

            if (pos == null) return false;
            _count--;

            if (beg == end)
            {
                beg = end = null;
                return true;
            }

            if (pos.Prev ==  null)
            {
                beg = beg?.Next;
                beg.Next = null;
                return true;
            }

            if (pos.Next == null)
            {
                end = end.Prev;
                beg.Next = null;
                return true;
            }

            Point<T> next = pos.Next;
            Point<T> prev = pos.Prev;
            pos.Next.Prev = prev;
            pos.Prev.Next = next;
            return true;
        }

        public T MakeRandomItem()
        {
            T data = new();
            data.RandomInit();
            return data;
        }

        public void AddToEnd(T item)
        {
            T newData = (T)item.Clone();
            Point<T> newItem = new Point<T>(newData);
            _count++;
            if (end  != null)
            {
                end.Next = newItem;
                newItem.Next = end;
                end = newItem;
            }
            else
            {
                beg = newItem;
                end = beg;
            }
        }

        public void PrintList()
        {
            if (_count == 0)
                Console.WriteLine("Список пуст");
            
            Point<T> current = beg;
            for (int i = 0; current != null; i++)
            {
                Console.WriteLine(current);
                current = current.Next;
            }
        }
    }
}
