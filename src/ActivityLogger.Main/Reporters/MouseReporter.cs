using AL.Core.Interfaces;
using AL.Core.Models;

namespace AL.Core.Reporters
{
    public class MouseReporter : Reporter<MouseReport>
    {
        public MouseReport MouseReport { get; private set; }

        private readonly IMouseReceiver _mouseReceiver;

        public MouseReporter(IMouseReceiver mouseReceiver)
        {
            _mouseReceiver = mouseReceiver;
        }

        protected override void Act(MouseReport mouseReport)
        {
            MouseReport = mouseReport;

            _mouseReceiver.ReportMouse(mouseReport);
        }
    }
}
