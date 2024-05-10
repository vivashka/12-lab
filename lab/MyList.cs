using BaseClassEmoji;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    public class MyList<T> where T : IInit, ICloneable, new()
    {
        Point<T>? beg;
        Point<T>? end;

        private int count = 0;

        public int Count => count;

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

            for (int i = 1; i <= size; i++)
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

        public Point<T> FindItem(string tag)
        {
            Point<T> current = beg;
            while (current != null)
            {
                if (current.Data == null)
                    throw new Exception("Пустые данные");

                if (current.Data.Tag == tag)
                    return current;
                current = current.Next;
            }
            return null;
        }

        public bool ChangeItem()
        {
            if (beg != null || count != 0)
            {
                Point<T> temp = MakeRandomData();

                temp.Next = beg.Next;
                beg = temp;
                return true;
            }
            return false;
            
        }

        public bool AddOddItem()
        {
            if (beg == null)
            {
                return false;
            }
            Point<T> current = beg.Next;

            while (current != null)
            {
                Point<T> temp = new Point<T>(MakeRandomItem());
                temp.Prev = current.Prev;
                current.Prev.Next = temp;
                temp.Next = current;
                current.Prev = temp;
                current = current.Next;
                count++;
            }
            return true;
        }

        public void AddToEnd(T item)
        {
            T newData = (T)item.Clone();
            Point<T> newItem = new Point<T>(newData);
            if (beg == null)
            {
                beg = newItem;
            }
            else
            {
                end.Next = newItem;
                newItem.Prev = end;
            }
            end = newItem;
            count++;
        }

        public bool RemoveItem(string tag)
        {
            if (beg == null) throw new Exception("Пустой список");

            Point<T>? pos = FindItem(tag);

            if (pos == null) return false;
            count--;

            if (beg == end)
            {
                beg = end = null;
                return true;
            }

            if (pos.Prev ==  null)
            {
                beg = beg?.Next;
                beg.Prev = null;
                return true;
            }

            if (pos.Next == null)
            {
                end = end.Prev;
                end.Next = null;
                return true;
            }

            Point<T> next = pos.Next;
            Point<T> prev = pos.Prev;
            pos.Next.Prev = prev;
            pos.Prev.Next = next;
            return true;
        }

        public void Clear()
        {
            beg = null;
            end = null;
            count = 0;
        }

        public T MakeRandomItem()
        {
            T data = new();
            data.RandomInit();
            return data;
        }

        public void PrintList()
        {
            if (count == 0)
                Console.WriteLine("Список пуст");
            
            Point<T> current = beg;
            for (int i = 0; current != null; i++)
            {
                Console.WriteLine($"{i} - {current}");
                current = current.Next;
            }
            Console.WriteLine($"Количество элементов в коллекции = {Count}");
        }

        public MyList<T> Clone()
        {
            MyList<T> newCollection = new MyList<T>();
            Point<T> current = beg;
            for (int i = 0; current != null; i++)
            {
                newCollection.AddToEnd(current.Data);
                current = current.Next;
            }
            return newCollection;
        }
    }
}
