using System;
using AL.Core.Interfaces;
using AL.Core.Models;

namespace AL.Core.Reporters
{
    public class MouseClickReporter : Reporter<MouseClickReport>
    {
        private static MouseClickReporter _instance;
        public static MouseClickReporter Instance(IMouseClickReceiver mouseClickReceiver = null)
        {
            return _instance ?? (_instance = new MouseClickReporter(mouseClickReceiver));
        }

        public MouseClickReport MouseClickReport { get; private set; }

        private readonly IMouseClickReceiver _mouseClickReceiver;

        private MouseClickReporter(IMouseClickReceiver mouseClickReceiver)
        {
            if (mouseClickReceiver == null)
                throw new ArgumentNullException(nameof(mouseClickReceiver));

            _mouseClickReceiver = mouseClickReceiver;
        }

        protected override void Act(MouseClickReport mouseClickReport)
        {
            MouseClickReport = mouseClickReport;

            _mouseClickReceiver.ReportMouseClick(mouseClickReport);
        }
    }
}
