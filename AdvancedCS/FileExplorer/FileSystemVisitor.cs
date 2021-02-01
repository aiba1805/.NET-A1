using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace FileExplorer
{
    public class FileSystemVisitor
    {
        public event EventHandler<FileEventArgs> FileFinded;
        public event EventHandler<FileEventArgs> FilteredFileFinded;
        public event EventHandler<DirectoryEventArgs> DirectoryFinded;
        public event EventHandler<DirectoryEventArgs> FilteredDirectoryFinded;
        public event EventHandler Start;
        public event EventHandler Finish;
        private bool isStop = false;

        public DirectoryItem ExploreDirectory(string path, string nameFilter = "")
        {
            OnStart(null);
            var info = new DirectoryInfo(path);
            var directoryItem = new DirectoryItem();
            directoryItem.Items = GetDirectories(path, nameFilter);
            directoryItem.FileItems = ExploreFiles(info.FullName, nameFilter);
            OnFinish(null);
            return directoryItem;
        }

        private DirectoryItem GetDirectory( DirectoryInfo info)
        {
            return new DirectoryItem()
            {
                Name = info.Name,
                FileItems = ExploreFiles(info.FullName),
                Items = GetDirectories(info.FullName)
            };
        }

        private List<DirectoryItem> GetDirectories(string path, string nameFilter = "")
        {
            var info = new DirectoryInfo(path);
            var directoryItems = new List<DirectoryItem>();
            foreach (var item in info.GetDirectories())
            {
                if (isStop) break;
                if (!string.IsNullOrEmpty(nameFilter))
                {
                    if (item.Name.StartsWith(nameFilter, StringComparison.OrdinalIgnoreCase))
                    {
                        var dirItem = GetDirectory(item);
                        OnFilteredDirectoryFinded(new DirectoryEventArgs
                        {
                            DirectoryItem = dirItem,
                            IsStop = isStop
                        });
                        directoryItems.Add(dirItem);
                    }
                }
                else
                {
                    var dirItem = GetDirectory(item);
                    OnDirectoryFinded(new DirectoryEventArgs
                    {
                        DirectoryItem = dirItem,
                        IsStop = isStop
                    });
                    directoryItems.Add(dirItem);
                }
            }
            return directoryItems;
        }

        private FileItem GetFile(FileInfo info)
        {
            return new FileItem
            {
                Name = info.Name
            };
        }

        public List<FileItem> ExploreFiles(string path, string nameFilter = "")
        {
            var info = new DirectoryInfo(path);
            var fileItems = new List<FileItem>();
            foreach (var fileItem in info.GetFiles())
            {
                if (isStop) break;
                if (!string.IsNullOrEmpty(nameFilter))
                {
                    if (fileItem.Name.StartsWith(nameFilter, StringComparison.OrdinalIgnoreCase))
                    {
                        var item = GetFile(fileItem);
                        OnFilteredFileFinded(new FileEventArgs
                        {
                            FileItem = item,
                            IsStop = isStop
                        });
                        fileItems.Add(item);
                    }
                }
                else
                {
                    var item = GetFile(fileItem);
                    OnFileFinded(new FileEventArgs
                    {
                        FileItem = item,
                        IsStop = isStop
                    });
                    fileItems.Add(item);
                }

            }
            return fileItems;
        }

        protected virtual void OnFilteredFileFinded(FileEventArgs e)
        {
            FilteredFileFinded?.Invoke(this, e);
            isStop = e.IsStop;
        }

        protected virtual void OnFileFinded(FileEventArgs e)
        {
            FileFinded?.Invoke(this, e);
            isStop = e.IsStop;
        }

        protected virtual void OnFilteredDirectoryFinded(DirectoryEventArgs e)
        {
            FilteredDirectoryFinded?.Invoke(this, e);
            isStop = e.IsStop;
        }

        protected virtual void OnDirectoryFinded(DirectoryEventArgs e)
        {
            DirectoryFinded?.Invoke(this, e);
            isStop = e.IsStop;
        }

        protected virtual void OnStart(EventArgs e)
        {
            Start?.Invoke(this, e);
        }
        protected virtual void OnFinish(EventArgs e)
        {
            Finish?.Invoke(this, e);
        }
    }
}
