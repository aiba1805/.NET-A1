using System;
using System.Collections.Generic;
using System.Text;

namespace FileExplorer
{
    public class DirectoryEventArgs : EventArgs
    {
        public DirectoryItem DirectoryItem { get; set; }
        public bool IsStop { get; set; }

    }
}
