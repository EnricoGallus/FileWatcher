using System;
using System.IO;
using System.Linq;

namespace FileWatcher
{
    class Program
    {
        private static string pathToFileToObserve;
        private static string targetFileName;
        
        static void Main(string[] args)
        {
            pathToFileToObserve = Path.Combine(args[0], args[1]);
            targetFileName = Path.Combine(args[0], "targetFile.txt");
            var watch = new FileSystemWatcher
            {
                Path = args[0],
                Filter = args[1],
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite,
                EnableRaisingEvents = true
            };
            watch.Changed += OnChanged;
            Console.ReadLine();
        }
        
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                string lastLine = File.ReadLines(pathToFileToObserve).Last();
                using (StreamWriter file = new StreamWriter(targetFileName))
                {
                    file.Write(lastLine);
                }
            }
        }
    }
}