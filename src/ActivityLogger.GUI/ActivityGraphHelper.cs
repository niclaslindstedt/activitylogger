using System.Linq;
using AL.Core.Models;
using AL.Gui.Graph;

namespace AL.Gui
{
    public static class ActivityGraphHelper
    {
        public static int[] GetKpmList(IActivityReport activityReport)
        {
            var kpmList = activityReport?.KeyStrokesPerMinute;

            if (kpmList == null || kpmList.Count == 0)
                return new [] {1};

            return activityReport.KeyStrokesPerMinute
                .Select(x => x.Value)
                .Reverse()
                .Take(GraphProperties.Width / GraphProperties.BarWidth)
                .Reverse()
                .ToArray();
        } 
    }
}
