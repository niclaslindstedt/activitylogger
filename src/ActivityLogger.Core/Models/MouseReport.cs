using System;

namespace AL.Core.Models
{
    public class MouseReport : IInputActivityReport
    {
        public double Distance { get; set; }
        public DateTime LatestActivity { get; set; } = DateTime.Now;
    }
}
