using System;
using System.Text;
using BaseClassEmoji;
using LabLibrary;

namespace lab
{
    public class Program
    {
        static MyList<Emoji> mainList = new MyList<Emoji>();
        static MyList<Emoji> cloneList = new MyList<Emoji>();

        static void Main(string[] args)
        {
            Menu();
        }
        public static void Menu()
        {
            string[] tasks = {"Сформировать двунаправленный список Emoji",
            "Распечатать полученный список",
            "Удалить из списка первый элемент с заданным именем",
            "Добавить в список элементы с номерами 1, 3, 5 и т. д.",
            "Выполнить глубокое клонирование списка",
            "Распечатать клонированный список",
            "Удалить список из памяти",
            "Выход"};

            byte apply = Display(tasks);
            do
            {
                switch (apply)
                {
                    case 0:
                        Console.WriteLine("Задайте размерность списка целым положительным числом");
                        uint size = LabLib.ExtensionDoWhile<uint>();
                        mainList = new MyList<Emoji>((int)size);
                        Menu();
                        break;
                    case 1:
                        mainList.PrintList();
                        Console.ReadKey();
                        Menu();
                        break;
                    case 2:
                        mainList.PrintList();
                        Console.WriteLine("Введите имя эмоджи");
                        string name = Console.ReadLine();
                        if (mainList.RemoveItem(name))
                        {
                            Console.WriteLine("Элемент удалён");
                        }
                        else
                        {
                            Console.WriteLine("Элемента не найден");
                        }
                        Console.ReadKey();
                        Menu();
                        break;
                    case 3:
                        mainList.PrintList();
                        if (mainList.AddOddItem())
                        {
                            Console.WriteLine("Элементы добавлены");
                        }
                        else
                        {
                            Console.WriteLine("Элемента не добавлены");
                        }
                        mainList.PrintList();
                        Console.ReadKey();
                        Menu();
                        break;
                    case 4:
                        cloneList = mainList.Clone();
                        cloneList.PrintList();
                        Console.ReadKey();
                        Menu();
                        break;
                    case 5:
                        Console.WriteLine("Оригинальный список");
                        mainList.PrintList();
                        Console.WriteLine("Клониврованный список");
                        cloneList.PrintList();
                        Console.ReadKey();
                        Menu();
                        break;
                    case 6:
                        mainList.Clear();
                        Console.WriteLine("Оригинальный список");
                        mainList.PrintList();
                        Console.ReadKey();
                        Menu();
                        break;
                    case 7:
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
    }
}
