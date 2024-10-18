using System;
using System.Linq;

namespace VoidENV
{
    public enum PathType
    {
        User,
        System
    }

    public static class PathManager
    {
        private const int PageSize = 15;
        private const int MaxPathLength = 100;

        public static void NavigatePathEntries(PathType pathType)
        {
            string path = GetPath(pathType);
            string[] directories = path.Split(';');
            int selectedIndex = 0;
            int pageIndex = 0;
            int totalPages = (directories.Length + PageSize - 1) / PageSize;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(" _    ______  ________     _______   ___    __");
                Console.WriteLine("| |  / / __ \\/  _/ __ \\   / ____/ | / / |  / /");
                Console.WriteLine("| | / / / / // // / / /  / __/ /  |/ /| | / / ");
                Console.WriteLine("| |/ / /_/ // // /_/ /  / /___/ /|  / | |/ /  ");
                Console.WriteLine("|___/\\____/___/_____/  /_____/_/ |_/  |___/   ");
                Console.WriteLine("                                               ");
                Console.WriteLine("                    v1.0                       ");
                Console.WriteLine("           Use arrow keys to move");
                Console.WriteLine("                                               ");
                Console.WriteLine($"== Current {(pathType == PathType.User ? "USER" : "SYSTEM")} PATH Entries ==");

                int start = pageIndex * PageSize;
                int end = Math.Min(start + PageSize, directories.Length);

                for (int i = start; i < end; i++)
                {
                    string displayText = directories[i].Length > MaxPathLength
                        ? directories[i].Substring(0, MaxPathLength) + "..."
                        : directories[i];

                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"> {displayText}");
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.WriteLine($"  {displayText}");
                    }
                }

                Console.ResetColor();
                Console.WriteLine($"\nPage {pageIndex + 1} of {totalPages}");

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedIndex > start)
                        {
                            selectedIndex--;
                        }
                        else if (pageIndex > 0)
                        {
                            pageIndex--;
                            selectedIndex = (pageIndex + 1) * PageSize - 1;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedIndex < end - 1)
                        {
                            selectedIndex++;
                        }
                        else if (pageIndex < totalPages - 1)
                        {
                            pageIndex++;
                            selectedIndex = pageIndex * PageSize;
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (pageIndex > 0)
                        {
                            pageIndex--;
                            selectedIndex = pageIndex * PageSize;
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (pageIndex < totalPages - 1)
                        {
                            pageIndex++;
                            selectedIndex = pageIndex * PageSize;
                        }
                        break;

                    case ConsoleKey.Enter:
                        ManageDirectoryAction(directories, selectedIndex, pathType);
                        break;

                    case ConsoleKey.Escape:
                        return;
                }
            }
        }

        private static void ManageDirectoryAction(string[] directories, int index, PathType pathType)
        {
            string[] options = { "Edit", "Remove", "Open in File Explorer", "Back to list" };
            int selectedOption = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"== Managing: {directories[index]} ==");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"> {options[i]}");
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.WriteLine($"  {options[i]}");
                    }
                }
                Console.ResetColor();

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = (selectedOption > 0) ? selectedOption - 1 : options.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption < options.Length - 1) ? selectedOption + 1 : 0;
                        break;
                    case ConsoleKey.Enter:
                        switch (selectedOption)
                        {
                            case 0:
                                EditDirectory(directories, index, pathType);
                                return;
                            case 1:
                                ConfirmAndRemoveDirectory(directories, index, pathType);
                                return;
                            case 2:
                                FileExplorer.OpenDirectory(directories[index]);
                                return;
                            case 3:
                                return;
                        }
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }

        private static void ConfirmAndRemoveDirectory(string[] directories, int index, PathType pathType)
        {
            Console.Clear();
            Console.WriteLine($"Are you sure you want to remove: {directories[index]}? (y/n)");
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Y)
            {
                directories = directories.Where((_, i) => i != index).ToArray();
                string newPath = string.Join(";", directories);
                SetPath(newPath, pathType);
                Console.WriteLine("Directory removed from PATH.");
            }
            else
            {
                Console.WriteLine("Operation canceled.");
            }
        }

        private static void EditDirectory(string[] directories, int index, PathType pathType)
        {
            Console.Write("\nEnter the new directory path (Press ESC to cancel): ");

            string newDirectory = "";
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("\nOperation canceled.");
                    return;
                }

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }

                Console.Write(keyInfo.KeyChar);
                newDirectory += keyInfo.KeyChar;
            }

            if (string.IsNullOrWhiteSpace(newDirectory))
            {
                Console.WriteLine("\nInvalid input. No changes made.");
                return;
            }

            directories[index] = newDirectory;
            string newPath = string.Join(";", directories);
            SetPath(newPath, pathType);

            Console.WriteLine($"\nDirectory updated to: {newDirectory}");
        }

        public static void CheckDirectoryInPath(PathType pathType)
        {
            Console.Write("\nEnter directory to check: ");
            string directoryToCheck = Console.ReadLine() ?? string.Empty;

            string[] directories = GetPath(pathType).Split(';');

            if (directories.Contains(directoryToCheck, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine($"{directoryToCheck} is in the PATH.");
            }
            else
            {
                Console.WriteLine($"{directoryToCheck} is not in the PATH.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void AddDirectoryToPath(PathType pathType)
        {
            Console.Write("\nEnter directory to add: ");
            string directoryToAdd = Console.ReadLine() ?? string.Empty;

            string[] directories = GetPath(pathType).Split(';');

            if (directories.Contains(directoryToAdd, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine($"{directoryToAdd} is already in the PATH.");
                return;
            }

            Console.WriteLine($"Are you sure you want to add: {directoryToAdd}? (y/n)");
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Y)
            {
                var newDirectories = directories.ToList();

                if (pathType == PathType.System)
                {
                    newDirectories.Insert(0, directoryToAdd);
                }
                else
                {
                    newDirectories.Add(directoryToAdd);
                }

                var uniqueDirectories = newDirectories.Distinct(StringComparer.OrdinalIgnoreCase).ToArray();

                string newPath = string.Join(";", uniqueDirectories);

                SetPath(newPath, pathType);
                Console.WriteLine($"{directoryToAdd} has been added to the {(pathType == PathType.User ? "USER" : "SYSTEM")} PATH.");
            }
            else
            {
                Console.WriteLine("Operation canceled.");
            }
        }

        private static string GetPath(PathType pathType)
        {
            return Environment.GetEnvironmentVariable("PATH", pathType == PathType.User ? EnvironmentVariableTarget.User : EnvironmentVariableTarget.Machine) ?? string.Empty;
        }

        private static void SetPath(string newPath, PathType pathType)
        {
            Environment.SetEnvironmentVariable("PATH", newPath, pathType == PathType.User ? EnvironmentVariableTarget.User : EnvironmentVariableTarget.Machine);
        }
    }
}
