using System;
using System.Diagnostics;
using System.IO;

namespace VoidENV
{
    public static class FileExplorer
    {
        public static void OpenDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Process.Start("explorer.exe", path);
            }
            else
            {
                Console.WriteLine($"Directory not found: {path}");
            }
        }
    }
}
