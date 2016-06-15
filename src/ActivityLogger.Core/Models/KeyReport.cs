using System;

namespace AL.Core.Models
{
    public class KeyReport : IInputActivityReport
    {
        public int KeyStrokes { get; set; }
        public DateTime LatestActivity { get; set; } = DateTime.Now;
    }
}
