using System;

namespace AL.Core.Models
{
    public class MouseReport : IInputActivityReport
    {
        public double Distance { get; set; }
        public DateTime LastActivity { get; set; } = DateTime.Now;
    }
}
