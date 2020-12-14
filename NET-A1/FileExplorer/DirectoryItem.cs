using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FileExplorer
{
    public class DirectoryItem : IEnumerable<DirectoryItem>
    {
        public DirectoryItem()
        {
            this.Items = new List<DirectoryItem>();
        }

        public string Name { get; set; }
        public List<FileItem> FileItems { get; set; }

        public List<DirectoryItem> Items { get; set; }

        public IEnumerator<DirectoryItem> GetEnumerator()
        {
            foreach (var item in Items)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in Items)
            {
                yield return item;
            }
        }
    }
}
