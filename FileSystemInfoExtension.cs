namespace System.IO
{
    public static class FileSystemInfoExtension
    {
        public static string Rahs(this FileSystemInfo fileInfo)
        {
            string rahs = "";

            if (fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly))
                rahs += 'r';
            else
                rahs += '-';
            if (fileInfo.Attributes.HasFlag(FileAttributes.Archive))
                rahs += 'a';
            else
                rahs += '-';
            if (fileInfo.Attributes.HasFlag(FileAttributes.Hidden))
                rahs += 'h';
            else
                rahs += '-';
            if (fileInfo.Attributes.HasFlag(FileAttributes.System))
                rahs += 's';
            else
                rahs += '-';
            return rahs;
        }
    }
}