using System;

namespace AL.Core.Models
{
    public class KeyReport : IInputActivityReport
    {
        public int KeyStrokes { get; set; }
        public DateTime LastActivity { get; set; } = DateTime.Now;
    }
}
