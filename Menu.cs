using System;

namespace VoidENV
{
    public class Menu
    {
        private string[] options = {
            "Manage Current User PATH",
            "Manage System PATH",
            "Check if a directory exists in PATH",
            "Add new directory to PATH",
            "Exit"
        };
        private int selectedIndex = 0;

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                DisplayMenu();
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : options.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex < options.Length - 1) ? selectedIndex + 1 : 0;
                        break;
                    case ConsoleKey.Enter:
                        ExecuteOption(selectedIndex);
                        break;
                }
            }
        }

private bool IsUserAdministrator()
{
#pragma warning disable CA1416 // Validate platform compatibility
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
#pragma warning restore CA1416 // Validate platform compatibility
#pragma warning disable CA1416 // Validate platform compatibility
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
#pragma warning restore CA1416 // Validate platform compatibility
#pragma warning disable CA1416 // Validate platform compatibility
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
#pragma warning restore CA1416 // Validate platform compatibility
        }

private void DisplayMenu()
{
    Console.WriteLine(" _    ______  ________     _______   ___    __");
    Console.WriteLine("| |  / / __ \\/  _/ __ \\   / ____/ | / / |  / /");
    Console.WriteLine("| | / / / / // // / / /  / __/ /  |/ /| | / / ");
    Console.WriteLine("| |/ / /_/ // // /_/ /  / /___/ /|  / | |/ /  ");
    Console.WriteLine("|___/\\____/___/_____/  /_____/_/ |_/  |___/   ");
    Console.WriteLine("                                               ");
    Console.WriteLine("                    v1.0                       ");
    Console.WriteLine("           Use arrow keys to move");
    Console.WriteLine("                                               ");

    if (!IsUserAdministrator())
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("** You must run this application as an administrator to add or remove directories from the System PATH. **");
        Console.ResetColor();
        Console.WriteLine("                                               ");
    }

    for (int i = 0; i < options.Length; i++)
    {
        if (i == selectedIndex)
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
}

        private void ExecuteOption(int index)
        {
            switch (index)
            {
                case 0:
                    PathManager.NavigatePathEntries(PathType.User);
                    break;
                case 1:
                    PathManager.NavigatePathEntries(PathType.System);
                    break;
                case 2:
                    PathType pathTypeCheck = GetPathTypeFromUser();
                    if (pathTypeCheck != PathType.User && pathTypeCheck != PathType.System)
                    {
                        Console.WriteLine("Invalid selection.");
                    }
                    else
                    {
                        PathManager.CheckDirectoryInPath(pathTypeCheck);
                    }
                    break;
                case 3:
                    PathType pathTypeAdd = GetPathTypeFromUser();
                    if (pathTypeAdd != PathType.User && pathTypeAdd != PathType.System)
                    {
                        Console.WriteLine("Invalid selection.");
                    }
                    else
                    {
                        PathManager.AddDirectoryToPath(pathTypeAdd);
                    }
                    break;
                case 4:
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    break;
            }
        }

        private PathType GetPathTypeFromUser()
        {
            string[] options = { "System PATH", "User PATH" };
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select PATH type:");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
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

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : options.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex < options.Length - 1) ? selectedIndex + 1 : 0;
                        break;
                    case ConsoleKey.Enter:
                        Console.ResetColor();
                        return selectedIndex == 0 ? PathType.System : PathType.User;
                }
            }
        }

    }
}
