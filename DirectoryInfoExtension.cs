using System.Collections;

namespace System.IO
{
    public static class DirectoryInfoExtension
    {
        private static DateTime oldestFileDate = DateTime.Now;
        public static string pathToOldestFile = "";

        public static DateTime LastCreationTime(this DirectoryInfo di)
        {
            string path = di.FullName;
            if (Directory.Exists(path))
                ProcessDirectory(path);
            else if (File.Exists(path)) computeOldestFileDate(path);
            return oldestFileDate;
        }

        public static int GetCountElements(this DirectoryInfo di)
        {
            return  Directory.GetDirectories(di.FullName).Length + Directory.GetFiles(di.FullName).Length;
        }

        private static void ProcessDirectory(string targetDirectory)
        {
            Array.ForEach(Directory.GetDirectories(targetDirectory), x => ProcessDirectory(x));
            Array.ForEach(Directory.GetFiles(targetDirectory), x => computeOldestFileDate(x));
        }

        private static void computeOldestFileDate(string fileName)
        {
            DateTime fileCreationTime = new FileInfo(fileName).CreationTime;
            if (fileCreationTime.CompareTo(oldestFileDate) == -1)
            {
                oldestFileDate = fileCreationTime;
                pathToOldestFile = fileName;
            }
        }
    }
}