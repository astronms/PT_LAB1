using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PT_LAB1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SortedDictionary<Item, int> itemSortedDictionary = new SortedDictionary<Item, int>();
            DirectoryInfo di = new DirectoryInfo(args[0]);

            int level = 0;
            string path = args[0];
            string sortMethod = args[1];
            if (Directory.Exists(path))
                ProcessDirectory(path, level, itemSortedDictionary, null);
            else if (File.Exists(path)) ProcessFile(path, level, itemSortedDictionary, null);

            var resultToPrint = sort(sortMethod, itemSortedDictionary);
            foreach (var item in resultToPrint.Where(i => i.Key.Parent == null))
            {
                if (item.Key.IsDirectory)
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                else
                    Console.ForegroundColor = ConsoleColor.White;
                WriteItem(item);
                PrintChild(resultToPrint, item.Key);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(di.LastCreationTime().ToString());
        }


        private static void PrintChild(Dictionary<Item, int> itemDictionary, Item parent)
        {
            foreach (var item in itemDictionary.Where(i => i.Key.Parent == parent))
                if (item.Key.IsDirectory)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    WriteItem(item);
                    PrintChild(itemDictionary, item.Key);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    WriteItem(item);
                }
        }

        private static Dictionary<Item, int> sort(string sortMethod, SortedDictionary<Item, int> itemSortedDictionary)
        {
            switch (sortMethod)
            {
                case "sizeDesc":
                    return itemSortedDictionary.ToDictionary(p => p.Key, p => p.Value);
                case "sizeAsc":
                    return itemSortedDictionary.Reverse().ToDictionary(p => p.Key, p => p.Value);
                case "dateDesc":
                    var test = itemSortedDictionary.OrderByDescending(i => i.Key.ModificationDate);
                    return itemSortedDictionary.OrderByDescending(i => i.Key.ModificationDate)
                        .ToDictionary(p => p.Key, p => p.Value);
                case "dateAsc":
                    return itemSortedDictionary.OrderBy(i => i.Key.ModificationDate)
                        .ToDictionary(p => p.Key, p => p.Value);
                case "nameDesc":
                    return itemSortedDictionary.OrderByDescending(i => i.Key.Name)
                        .ToDictionary(p => p.Key, p => p.Value);
                case "nameAsc":
                    return itemSortedDictionary.OrderBy(i => i.Key.Name)
                        .ToDictionary(p => p.Key, p => p.Value);
                default:
                    return itemSortedDictionary.ToDictionary(p => p.Key, p => p.Value);
            }
        }

        private static void ProcessFile(string path, int level, SortedDictionary<Item, int> itemSortedDictionary, Item parent)
        {
            DateTime modificaTime = Directory.GetLastWriteTime(path);
            int size = path.Length;
            Item item = new Item(path,  size, level, modificaTime, parent, false);
            if (!itemSortedDictionary.ContainsKey(item)) itemSortedDictionary.Add(item, item.Size);
        }

        private static void ProcessDirectory(string path, int level, SortedDictionary<Item, int> itemSortedDictionary, Item parent)
        {
            DateTime modificaTime = Directory.GetLastWriteTime(path);
            int size = new DirectoryInfo(path).GetCountElements();

            Item item = new Item(path,size, level, modificaTime, parent, true);
            if (!itemSortedDictionary.ContainsKey(item)) itemSortedDictionary.Add(item, item.Size);
            level++;

            string[] subdirectoryEntries = Directory.GetDirectories(path);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory, level, itemSortedDictionary, item);

            string[] fileEntries = Directory.GetFiles(path);
            foreach (string fileName in fileEntries) ProcessFile(fileName, level, itemSortedDictionary,  item);
        }

        private static string GenarateString(int level)
        {
            string text = "";
            for (int i = 0; i < level; i++)
                text = '\t' + text;
            return text;
        }

        private static void WriteItem(KeyValuePair<Item, int> item)
        {
            Console.WriteLine(GenarateString(item.Key.Level) + item.Key.Name + "   " + item.Key.Size + "   " +
                              item.Key.Rahs + "  " + item.Key.ModificationDate);
        }
    }
}