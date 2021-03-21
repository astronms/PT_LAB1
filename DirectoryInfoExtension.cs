using System.Collections;

namespace System.IO
{
    public static class DirectoryInfoExtension
    {
        private static DateTime oldestFileDate = DateTime.Now;
        public static string pathToOldestFile = "";
        
        public static int GetCountElements(this DirectoryInfo di)
        {
            return  Directory.GetDirectories(di.FullName).Length + Directory.GetFiles(di.FullName).Length;
        }

    }
}