using BaseClassEmoji;
using LabLibrary;
using System.Text;

namespace Lab12_4
{
    internal class Program
    {
        static MyCollection<Emoji> myCollection = new MyCollection<Emoji>();
        static MyCollection<Emoji> clone = new MyCollection<Emoji>();
        static MyCollection<Emoji> copy = new MyCollection<Emoji>();
        static Emoji[] array = new Emoji[1];

        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc1251 = Encoding.GetEncoding(1251);

            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            System.Console.InputEncoding = enc1251;

            Menu();
        }

        public static void Menu()
        {
            string[] tasks = {"Сформировать коллекцию",
                "Распечатать коллекцию",
                "Добавить элемент в коллекцию",
                "Удалить элемент из коллекции",
                "Найти элемент по значению",
                "Копировать в список элементы",
                "Клонировать элементы в другое дерево (через конструктор)",
                "Скопировать элементы в другое дерево",
                "Выход"};

            byte apply = Display(tasks);
            do
            {
                switch (apply)
                {
                    case 0:
                        Console.WriteLine("Задайте размерность списка целым положительным числом");
                        uint size = LabLib.ExtensionDoWhile<uint>();
                        myCollection = new MyCollection<Emoji>((int)size);
                        Menu();
                        break;
                    case 1:
                        if (myCollection.Count != 0)
                        {
                            Console.WriteLine("Обычный вывод");
                            myCollection.ShowTree();
                            Console.WriteLine("Вывод перебором");
                            PrintCollection(myCollection);
                        }
                        else
                        {
                            Console.WriteLine("Дерево пусто");
                        }
                        Console.ReadKey();
                        Menu();
                        break;
                    case 2:
                        Emoji item = new Emoji();
                        item.Init();
                        myCollection.Add(item);
                        myCollection.ShowTree();
                        Console.ReadKey();
                        Menu();
                        break;
                    case 3:
                        myCollection.ShowTree();
                        item = new Emoji();
                        item.Init();
                        if (myCollection.Count != 0)
                        {
                            myCollection.Remove(item);
                            Console.WriteLine("Элемент удален");
                        }
                        else
                        {
                            Console.WriteLine("Коллекция пуста, удалять нечего");
                        }
                        myCollection.ShowTree();
                        Console.ReadKey();
                        Menu();
                        break;
                    case 4:
                        PrintCollection(myCollection);
                        item = new Emoji();
                        item.Init();
                        if (myCollection.Count != 0)
                        {
                            myCollection.Contains(item);
                            Console.WriteLine("Элемент найден");
                        }
                        else
                        {
                            Console.WriteLine("Коллекция пуста, находить нечего");
                        }
                        myCollection.ShowTree();
                        Console.ReadKey();
                        Menu();
                        break;
                    case 5:
                        PrintCollection(myCollection);
                        foreach (Emoji elem in array)
                        {
                            Console.WriteLine(elem?.ToString() ?? "Пусто");
                        }
                        if (myCollection.Count != 0)
                        {
                            Console.WriteLine("Введите индекс с которого хотите добавлять элементы в массив");
                            int index = LabLib.ExtensionDoWhile<int>();

                            myCollection.CopyTo(array, index);
                        }
                        else
                        {
                            Console.WriteLine("Коллекция пуста, добавлять нечего");
                        }
                        foreach (Emoji elem in array)
                        {
                            Console.WriteLine(elem);
                        }
                        myCollection.ShowTree();
                        Console.ReadKey();
                        Menu();
                        break;
                    case 6:
                        clone = new MyCollection<Emoji>(myCollection);
                        Console.WriteLine("Оригинал");
                        myCollection.ShowTree();
                        Console.WriteLine("Клон");
                        clone.ShowTree();
                        Console.ReadKey();
                        Menu();
                        break;
                    case 7:
                        foreach (Emoji elem in myCollection)
                        {
                            copy.Add(elem);
                        }
                        Console.WriteLine("Оригинал");
                        myCollection.ShowTree();
                        Console.WriteLine("Копия");
                        clone.ShowTree();
                        Console.ReadKey();
                        Menu();
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                }
            } while (apply < 1);
        }

        public static byte Display(string[] options)
        {
            byte selectedItem = 0;
            ConsoleKeyInfo pressedKey;

            do
            {
                ColorText(selectedItem, options);
                pressedKey = Console.ReadKey();
                if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    selectedItem = selectedItem > 0 ? selectedItem -= (byte)1 : selectedItem = (byte)(options.Length - 1);
                }
                if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    selectedItem = (byte)((selectedItem + 1) % options.Length);
                }
            } while (pressedKey.Key != ConsoleKey.Enter);

            return selectedItem;
        }

        public static void ColorText(byte apply, string[] options)
        {
            Console.Clear();
            for (byte i = 0; i < options.Length; i++)
            {
                string prefix;
                if (apply == i)
                {
                    prefix = "<";
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    prefix = "";
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(options[i] + prefix);
            }
        }

        static void PrintCollection(MyCollection<Emoji> collection)
        {
            if(collection.Count == 0)
            {
                Console.WriteLine("Коллекция пуста");
            }
            foreach (Emoji item in collection)
            {
                Console.WriteLine(item);
            }
        }
    }
}