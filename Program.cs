using System;
using System.IO;

public static class FileExplorer
{
    public static void ShowDrives()
    {
        DriveInfo[] allDrives = DriveInfo.GetDrives();
        foreach (DriveInfo d in allDrives)
        {
            Console.WriteLine($"Диск {d.Name}");
            Console.WriteLine($"  Тип файла: {d.DriveType}");
            if (d.IsReady)
            {
                Console.WriteLine($"  Обьем: {d.VolumeLabel}");
                Console.WriteLine($"  Система: {d.DriveFormat}");
                Console.WriteLine($"  Свободное пространство для файла:{(d.AvailableFreeSpace / (1024 * 1024 * 1024))} GB");
                Console.WriteLine($"  Общее доступное пространство: {(d.TotalFreeSpace / (1024 * 1024 * 1024))} GB");
                Console.WriteLine($"  Размер диска: {(d.TotalSize / (1024 * 1024 * 1024))} GB");
            }
        }
    }

    public static void ShowDirectoryContent(string path)
    {
        string[] dirs = Directory.GetDirectories(path);
        string[] files = Directory.GetFiles(path);

        Console.WriteLine("Directories:");
        foreach (string dir in dirs)
        {
            Console.WriteLine($"{Path.GetFileName(dir)}");
        }

        Console.WriteLine("Files:");
        foreach (string file in files)
        {
            Console.WriteLine($"{Path.GetFileName(file)}");
        }
    }

    public static void OpenFile(string path)
    {
        try
        {
            System.Diagnostics.Process.Start(path);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

public static class ConsoleMenu
{
    public static int ShowMenu(string[] options)
    {
        int selectedIndex = 0;

        while (true)
        {
            Console.Clear();
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(options[i]);
                Console.ResetColor();
            }

            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.DownArrow)
            {
                selectedIndex++;
                if (selectedIndex == options.Length)
                {
                    selectedIndex = 0;
                }
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                selectedIndex--;
                if (selectedIndex < 0)
                {
                    selectedIndex = options.Length - 1;
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                return selectedIndex;
            }
        }
    }
}

public class Program
{
    public static void Main()
    {
        string[] menuOptions = { "Диски", "Выход" };

        while (true)
        {
            int selectedOption = ConsoleMenu.ShowMenu(menuOptions);

            if (selectedOption == 0)
            {
                FileExplorer.ShowDrives();
                Console.ReadKey();
            }
            else if (selectedOption == 1)
            {
                break;
            }
        }
    }
}
