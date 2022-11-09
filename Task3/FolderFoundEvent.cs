using System;

namespace Task3
{
    public class FolderFoundEvent : EventArgs
    {
        public string FolderName { get; set; }

        public bool IsIncluded { get; set; }

        public bool IsAborted { get; set; }
    }
}
