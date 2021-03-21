using System;
using System.IO;

namespace PT_LAB1
{
    internal class Item : IComparable<Item>
    {

        public Item Parent { get; set; }

        public bool IsDirectory { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        public string Rahs { get; set; }

        public int Size { get; set; }

        public int Level { get; set; }

        public DateTime ModificationDate { get; set; }

        public Item(string path, int size, int level, DateTime modificationDate,Item parent,bool isDirectory)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            Name = setNameFromPath(path); ;
            Path = path;
            Rahs =  di.Rahs();
            Size = size;
            Level = level;
            ModificationDate = modificationDate;
            Parent = parent;
            IsDirectory = isDirectory;
        }
        public int CompareTo(Item other)
        {
            return other.Size.CompareTo(Size);
        }

        private string setNameFromPath(string path)
        {
            return path.Remove(0, path.LastIndexOf('\\') + 1);
        }
    }
}