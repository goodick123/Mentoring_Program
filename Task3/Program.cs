using System;
using System.IO;

namespace Task3
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Enter source folder path:");
            var sourcePath = Console.ReadLine();

            if (string.IsNullOrEmpty(sourcePath))
            {
                Console.WriteLine("Source folder path was not entered.");
                return;
            }

            try
            {
                var fileSystemVisitor = new EventsSearchHandler(sourcePath);
                fileSystemVisitor.FileFound += FileFoundEventHandler;
                fileSystemVisitor.FolderFound += FolderFoundEventHandler;
                fileSystemVisitor.FilteredFileFound += FilteredFileFoundEventHandler;
                fileSystemVisitor.FilteredFolderFound += FilteredFolderFoundEventHandler;
                fileSystemVisitor.StartSearch += StartSearchEventHandler;
                fileSystemVisitor.StopSearch += StopSearchEventHandler;

                var foundedData = fileSystemVisitor.ReturnFolderData();

                foreach (var item in foundedData)
                {
                    Console.WriteLine(item);
                }
            }
            catch (DirectoryNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        private static void FileFoundEventHandler(object sender, FileFoundEvent e)
        {
            Console.WriteLine("File with name {0} was found.", e.FileName);
            Console.WriteLine("Press '1' key to include this item to final sequence, press any other button to skip this file or press '2' to abort searching.");
            var input = Console.ReadKey(true).KeyChar;
            e.IsIncluded = input == '1';
            e.IsAborted = input == '2';
        }

        private static void FolderFoundEventHandler(object sender, FolderFoundEvent e)
        {
            Console.WriteLine("Folder with name {0} was found.", e.FolderName);
            Console.WriteLine("Press '1' key to include this item to final sequence, press any other button to skip this folder or press '2' to abort searching.");
            var input = Console.ReadKey(true).KeyChar;
            e.IsIncluded = input == '1';
            e.IsAborted = input == '2';
        }

        private static void FilteredFileFoundEventHandler(object sender, FileFoundEvent e)
        {
            Console.WriteLine("Filtered file with name {0} was found.", e.FileName);
            Console.WriteLine("Press '1' key to include this item to final sequence, press any other button to skip this file or press '2' to abort searching.");
            var input = Console.ReadKey(true).KeyChar;
            e.IsIncluded = input == '1';
            e.IsAborted = input == '2';
        }

        private static void FilteredFolderFoundEventHandler(object sender, FolderFoundEvent e)
        {
            Console.WriteLine("Filtered folder with name {0} was found.", e.FolderName);
            Console.WriteLine("Press '1' key to include this item to final sequence, press any other button to skip this folder or press '2' to abort searching.");
            var input = Console.ReadKey(true).KeyChar;
            e.IsIncluded = input == '1';
            e.IsAborted = input == '2';
        }

        private static void StartSearchEventHandler(object sender, StartSearchEvent e)
        {
            Console.WriteLine("Started at {0}.", e.StartTime);
        }

        private static void StopSearchEventHandler(object sender, StopSearchEvent e)
        {
            if (!e.IsAbortedOrFinished)
            {
                Console.WriteLine("Press '2' if you want to abort search or press any other button to continue.");
                var input = Console.ReadKey(true).KeyChar;
                e.IsAbortedOrFinished = input == '2';
            }
            if (e.IsAbortedOrFinished)
            {
                e.StopTime = DateTime.Now;
                Console.WriteLine("Ended at {0}.", e.StopTime);
            }
        }
    }
}
