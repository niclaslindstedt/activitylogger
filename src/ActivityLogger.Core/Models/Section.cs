using System;
using System.Collections.Generic;
using System.Linq;

namespace AL.Core.Models
{
    public class Section
    {
        public Section()
        {
            Activities = new List<Activity>();
        }

        public string SectionName { get; set; }
        public float HourGoal { get; set; }

        public ICollection<Activity> Activities { get; set; }

        public int Seconds => Activities.Sum(x => x.Seconds);
        public int Clicks => Activities.Sum(x => x.Clicks);
        public int KeyStrokes => Activities.Sum(x => x.KeyStrokes);
        public double Distance => Activities.Sum(x => x.Distance);
        public TimeSpan Elapsed => TimeSpan.FromSeconds(Seconds);
        public int Percent => (DateTime.Now - Started).Seconds * 100 / Seconds;
        public DateTime Started => Activities.OrderBy(x => x.Started).Select(x => x.Started).Last();
    }
}
