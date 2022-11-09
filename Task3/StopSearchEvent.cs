using System;

namespace Task3
{
    public class StopSearchEvent : EventArgs
    {
        public DateTime StopTime { get; set; }

        public bool IsAbortedOrFinished { get; set; }
    }
}
