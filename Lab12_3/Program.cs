using AnimalEmoji;
using BaseClassEmoji;
using ClassFaceEmoji;
using LabLibrary;
using SmilingEmoji;
using System.Collections;

namespace Lab12_3
{
    internal class Program
    {
        static BinaryTree<Emoji> binaryTree = new BinaryTree<Emoji>();
        static BinaryTree<Emoji> cloneTree = new BinaryTree<Emoji>();
        static void Main(string[] args)
        {
            Menu();
        }
        public static void Menu()
        {
            string[] tasks = {"Сформировать АВЛ дерево",
                "Распечатать дерево",
                "Найти количество листьев в дереве",
                "Преобразовать АВЛ дерево в дерево поиска",
                "Удалить из дерева поиска элемент с заданным ключом - !не реализованно!",
                "Удалить дерево из памяти.",
                "Изменить первый элемент клонированного дерева",
                "Выход"};

            byte apply = Display(tasks);
            do
            {
                switch (apply)
                {
                    case 0:
                        Console.WriteLine("Задайте размерность списка целым положительным числом");
                        uint size = LabLib.ExtensionDoWhile<uint>();
                        binaryTree = new BinaryTree<Emoji>((int)size);
                        Menu();
                        break;
                    case 1:
                        if(binaryTree.Count != 0)
                        {
                            binaryTree.ShowTree();
                        }
                        else
                        {
                            Console.WriteLine("Дерево пусто");
                        }
                        Console.ReadKey();
                        Menu();
                        break;
                    case 2:
                        binaryTree.ShowTree();
                        Console.WriteLine(binaryTree.LeafsAmount());
                        Console.ReadKey();
                        Menu();
                        break;
                    case 3:
                        try
                        {
                            Console.WriteLine("Старое дерево");
                            if (binaryTree.Count != 0)
                            {
                                binaryTree.ShowTree();
                            }
                            else
                            {
                                Console.WriteLine("Дерево пусто");
                            }
                            cloneTree = binaryTree.TransformToFindTree();
                            Console.WriteLine("Новое дерево");
                            cloneTree.ShowTree();
                        }
                        catch
                        {
                            Console.WriteLine("Нечего клонировать!");
                        }
                        Console.ReadKey();
                        Menu();
                        break;
                    case 4:
                        binaryTree.DeleteTree();
                        if (binaryTree.Count == 0)
                        {
                            Console.WriteLine("Дерево удалено");
                        }
                        Console.ReadKey();
                        Menu();
                        break;
                    case 5:
                        binaryTree.DeleteTree();
                        if (binaryTree.Count == 0)
                        {
                            Console.WriteLine("Дерево удалено");
                        }
                        Console.ReadKey();
                        Menu();
                        break;
                    case 6:
                        ChangeItem();
                        if (binaryTree.Count != 0)
                        {
                            Console.WriteLine("Сбалансированное дерево");
                            binaryTree.ShowTree();
                            Console.WriteLine("Дерево поиска");
                            cloneTree.ShowTree();
                        }
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

        public static void ChangeItem()
        {
            cloneTree.ChangeRoot();
            if (cloneTree.Count != 0)
            {
                Console.WriteLine("Успешно изменено");
            }
            else
            {
                Console.WriteLine("Дерево пусто");
            }
        }
    }
}