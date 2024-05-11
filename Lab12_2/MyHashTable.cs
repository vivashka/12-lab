using BaseClassEmoji;
using System.Collections;
using System.Data.SqlTypes;

namespace Lab12_2
{
    public class MyHashTable<T> where T : IInit, IComparable, ICloneable, new()
    {

        Data<T>[] table;
        int count = 0;
        float fillRatio;

        public int Capacity => table.Length;

        public int Count => count;

        public MyHashTable(int length = 10, float fillRatio = 0.72f)
        {
            table = new Data<T>[length];
            this.fillRatio = fillRatio;
        }

        public bool Contains(string key)
        {
            if (count == 0)
            {
                return false;
            }
            return !(FindItem(key) < 0);
        }

        public bool RemoveData(string key)
        {
            if (count == 0)
            {
                return false;
            }
            int index = FindItem(key);
            if (index < 0) return false;
            
            table[index] = default;
            count--;
            return true;
        }

        public void Print()
        {
            if (count == 0)
            {
                Console.WriteLine("Массив пуст");
                return;
            }
            int i = 0;
            foreach(Data<T> item in table)
            {
                if (item != null)
                    Console.WriteLine($"{i}: Ключ: {item.Key}, Значение:{item.Value}," +
                        $" {Math.Abs(item.Key.GetHashCode()) % Capacity}");
                else
                    Console.WriteLine($"{i}");
                i++;
            }
        }

        public void AddItem(string key, T value)
        {
            if (!this.Contains(key))
            {
                if ((float)Count / Capacity > fillRatio)
                {
                    Data<T>[] temp = (Data<T>[])table.Clone();
                    table = new Data<T>[temp.Length*2];
                    count = 0;
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i] != null)
                        {
                            AddData(temp[i]?.Key, temp[i].Value);
                        }
                    }
                    
                }
                AddData(key, value);
            }
        }

        int GetIndex(string key)
        {
            return Math.Abs(key.GetHashCode()) % Capacity;
        }

        void AddData(string? key, T? value)
        {
            if (key == null) return;

            key = (string)key.Clone();

            value = (T)value.Clone();

            int index = GetIndex(key);

            int current = index;

            if (table[index] != null)
            {
                while (current < table.Length && table[current] != null)
                    current++;
                if (current == table.Length)
                {
                    current = 0;
                    while(current < index && table[current] != null)
                        current++;
                    if (current == index)
                    {
                        throw new Exception("Нет места в таблице");
                    }
                }
            }
            table[current] = new Data<T> { Key = key, Value = value };
            count++;
        }

        int FindItem(string key)
        {
            int index = GetIndex(key);
           
            
            if (table[index] != null && table[index].Key.Equals(key))
            {
                return index;
            }
            else
            {
                int current = index;
                while (current < table.Length)
                {
                    if (table[current] != null && table[current].Key.Equals(key))
                        return current;
                    current++;
                }
                current = 0;
                while (current < index)
                {
                    if (table[current] != null && table[current].Key.Equals(key))
                        return current;
                    current++;
                }
            }
            
            return -1;
        }
    }

    public class Data<T> where T : IInit, IComparable, new()
    {
        public string Key { get; set; }

        public T? Value { get; set; }
    }
}
