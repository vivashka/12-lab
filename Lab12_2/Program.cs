using AnimalEmoji;
using BaseClassEmoji;
using ClassFaceEmoji;
using LabLibrary;
using SmilingEmoji;

namespace Lab12_2
{
    internal class Program
    {
        static MyHashTable<Emoji> hashTable = new MyHashTable<Emoji>();

        static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {
            string[] tasks = {"Создать хеш-таблицу и заполнить ее элементами.",
                "Распечатать таблицу",
                "Выполнить поиск элемента в хеш-таблице",
                "Удалить найденный элемент из хеш-таблицы.",
                "Добавить элемент",
                "Выход"};

            byte apply = Display(tasks);
            do
            {
                switch (apply)
                {
                    case 0:
                        Console.WriteLine("Задайте размерность списка целым положительным числом");
                        uint size = LabLib.ExtensionDoWhile<uint>();
                        hashTable = CreateTable((int)size);
                        Menu();
                        break;
                    case 1:
                        hashTable.Print();
                        Console.ReadKey();
                        Menu();
                        break;
                    case 2:
                        hashTable.Print();
                        Console.WriteLine("Введите тег (ключ) эмоджи");
                        string name = Console.ReadLine();

                        if (hashTable.Contains(name))
                        {
                            Console.WriteLine("Элемент найден");
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден");
                        }

                        Console.ReadKey();
                        Menu();
                        break;
                    case 3:
                        hashTable.Print();
                        Console.WriteLine("Введите тег (ключ) эмоджи");
                        name = Console.ReadLine();

                        if (hashTable.RemoveData(name))
                        {
                            Console.WriteLine("Элементы удалён");
                        }
                        else
                        {
                            Console.WriteLine("Элемента не найден");
                        }
                        hashTable.Print();
                        Console.ReadKey();
                        Menu();
                        break;
                    case 4:
                        Emoji item = new Emoji();
                        item.RandomInit();
                        Console.WriteLine("Старая таблица");
                        hashTable.Print();
                        try
                        {
                            hashTable.AddItem(item.Tag, item);
                        }
                        catch
                        {

                        }
                        Console.WriteLine("Новая таблица");
                        hashTable.Print();
                        Console.ReadKey();
                        Menu();
                        break;
                    case 5:
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

        public static MyHashTable<Emoji> CreateTable(int length)
        {
            MyHashTable<Emoji> randomList = new MyHashTable<Emoji>();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int randomNumber = random.Next(1, 4);
                Emoji emoji;

                switch (randomNumber)
                {
                    case 1:
                        emoji = new FaceEmoji();
                        emoji.RandomInit();
                        break;
                    case 2:
                        emoji = new Animal();
                        emoji.RandomInit();
                        break;
                    case 3:
                        emoji = new Smiling();
                        emoji.RandomInit();
                        break;
                    default:
                        emoji = new Emoji();
                        emoji.RandomInit();
                        break;
                }

                randomList.AddItem(emoji.Tag, emoji);
            }

            return randomList;
        }
    }
}