using System;

namespace AL.Core.Models
{
    public class Activity
    {
        public Activity()
        {
            Started = DateTime.Now;
        }

        public DateTime Started { get; }
        public string ProcessName { get; set; }
        public string ProcessDescription { get; set; }
        public int Seconds { get; set; }
        public int Clicks { get; set; }
        public int KeyStrokes { get; set; }
        public double Distance { get; set; }

        public TimeSpan Elapsed => TimeSpan.FromSeconds(Seconds);
        public int Percent => (DateTime.Now - Started).Seconds * 100 / Seconds;
    }
}
