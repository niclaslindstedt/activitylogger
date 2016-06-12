using System.Drawing;

namespace ActivityLogger.Core.Services
{
    public class MouseReporter : ActivityReporter<Point>
    {
        protected override void Act(Point value)
        {
            // Nothing to do
        }

        public MouseReporter(Settings settings) : base(settings) { }
    }
}
