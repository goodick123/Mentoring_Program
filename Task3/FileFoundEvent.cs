using System;

namespace Task3
{
    public class FileFoundEvent : EventArgs
    {
        public string FileName { get; set; }

        public bool IsIncluded { get; set; }

        public bool IsAborted { get; set; }
    }
}
