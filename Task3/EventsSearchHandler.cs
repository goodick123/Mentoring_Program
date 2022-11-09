using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task3
{
    public class EventsSearchHandler
    {
        public event EventHandler<FileFoundEvent> FileFound;
        public event EventHandler<FolderFoundEvent> FolderFound;
        public event EventHandler<FileFoundEvent> FilteredFileFound;
        public event EventHandler<FolderFoundEvent> FilteredFolderFound;
        public event EventHandler<StartSearchEvent> StartSearch;
        public event EventHandler<StopSearchEvent> StopSearch;

        public bool IsSearchAborted { get; private set; }

        private string _path;

        public Predicate<string> Predicate { get; set; }

        public EventsSearchHandler(string path)
        {
            _path = path;
        }

        public List<string> ReturnFolderData()
        {
            var args = new StartSearchEvent
            {
                StartTime = DateTime.Now
            };

            StartSearch.Invoke(this, args);

            var folderData = GetDataFromPath(_path).ToList();

            if (folderData != null && IsSearchAborted == false)
            {
                folderData = ReturnFilteredFolderData(folderData);
            }

            return folderData;
        }

        private List<string> ReturnFilteredFolderData(List<string> folderData)
        {
            Console.WriteLine(
                "Enter file extension or name you would like to filter (if you don't need to filter - skip with pressing enter button):");
            var expression = Console.ReadLine();

            Predicate<string> predicate = str => str.Contains(expression ?? "");

            Predicate = predicate;

            var filteredFolderData = FilterFolderData(folderData).ToList();

            if (IsSearchAborted)
            {
                var args = new StopSearchEvent
                {
                    StopTime = DateTime.Now,
                    IsAbortedOrFinished = true
                };

                StopSearch.Invoke(this, args);
            }

            Console.WriteLine("Filtered folder data:");

            return filteredFolderData;
        }

        private IEnumerable<string> GetDataFromPath(string sourceFolder)
        {
            if (!Directory.Exists(sourceFolder))
            {
                throw new DirectoryNotFoundException("Directory was not found.");
            }

            var folders = Directory.GetDirectories(sourceFolder);

            if (folders.Any())
            {
                folders = ExtractFileNames(folders).ToArray();

                foreach (var item in folders)
                {
                    var args = new FolderFoundEvent
                    {
                        FolderName = item
                    };

                    FolderFound.Invoke(this, args);

                    if (args.IsAborted)
                    {
                        AbortSearch();
                        yield break;
                    }

                    if (!args.IsIncluded)
                    {
                        continue;
                    }

                    yield return item;
                }
            }

            var files = Directory.GetFiles(sourceFolder);

            if (files.Any())
            {
                files = ExtractFileNames(files).ToArray();

                foreach (var item in files)
                {
                    var args = new FileFoundEvent
                    {
                        FileName = item
                    };

                    FileFound.Invoke(this, args);

                    if (args.IsAborted)
                    {
                        AbortSearch();
                        yield break;
                    }

                    if (!args.IsIncluded)
                    {
                        continue;
                    }

                    yield return item;

                }
            }
        }

        private IEnumerable<string> FilterFolderData(IReadOnlyCollection<string> folderItems)
        {
            if (folderItems.Count == 0)
            {
                yield break;
            }

            foreach (var item in folderItems)
            {
                if (Predicate.Invoke(item))
                {
                    if (Directory.Exists(_path += $@"\{item}"))
                    {
                        var args = new FolderFoundEvent()
                        {
                            FolderName = item
                        };

                        FilteredFolderFound.Invoke(this, args);

                        if (args.IsAborted)
                        {
                            AbortSearch();
                            yield break;
                        }

                        if (args.IsIncluded)
                            yield return item;
                    }
                    else
                    {
                        var args = new FileFoundEvent()
                        {
                            FileName = item
                        };

                        FilteredFileFound.Invoke(this, args);

                        if (args.IsAborted)
                        {
                            AbortSearch();
                            yield break;
                        }

                        if (args.IsIncluded)
                            yield return item;
                    }
                }
            }
        }

        private static IEnumerable<string> ExtractFileNames(string[] namesArray)
        {
            return namesArray.Select(Path.GetFileName);
        }

        private void AbortSearch()
        {
            var args = new StopSearchEvent
            {
                StopTime = DateTime.Now,
                IsAbortedOrFinished = true
            };

            StopSearch.Invoke(this, args);
        }
    }
}
