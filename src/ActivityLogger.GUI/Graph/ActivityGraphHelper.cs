using System.Drawing;
using System.Linq;
using AL.Core.Models;

namespace AL.Gui.Graph
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

        public static Color GetBarColor(int keyStrokes, int maxKeyStrokes)
        {
            var pc = (double)keyStrokes/maxKeyStrokes;

            const int red = 255;
            var green = 255;
            const int blue = 0;

            if (pc >= 0 && pc <= 1)
            {
                green = (int)(255 - pc * 255);
            }

            return Color.FromArgb(255, red, green, blue);
        }
    }
}
