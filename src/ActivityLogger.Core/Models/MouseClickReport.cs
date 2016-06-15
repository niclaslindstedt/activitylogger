using System;

namespace AL.Core.Models
{
    public class MouseClickReport : IInputActivityReport
    {
        public int TotalClicks { get; set; }
        public int Clicks { get; set; }
        public DateTime LatestActivity { get; set; } = DateTime.Now;
    }
}
