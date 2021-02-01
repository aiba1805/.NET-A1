using System;
using System.Collections.Generic;
using System.Text;

namespace FileExplorer
{
    public class FileEventArgs : EventArgs
    {
        public FileItem FileItem { get; set; }

        public bool  IsStop { get; set; }

    }
}
