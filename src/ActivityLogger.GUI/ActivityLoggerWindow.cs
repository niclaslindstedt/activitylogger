using System.Collections.Generic;
using System.Windows.Forms;
using AL.Core.Interfaces;
using AL.Core.Models;
using AL.Gui.Elements;

namespace AL.Gui
{
    public partial class ActivityLoggerWindow : Form, IActivityReceiver
    {
        private readonly IList<IControlUpdater> _controls;

        public ActivityLoggerWindow()
        {
            InitializeComponent();
            
            _controls = new List<IControlUpdater>
            {
                new LogElement(textBoxLog, "Text"),
                new ProcessReportElement(textBoxProcesses, "Text"),
                new ActiveProcessElement(labelActiveProcessValue, "Text"),
                new ActiveProcessElapsedElement(labelActiveTimeValue, "Text"),
                new ProgressElement(progressBarActiveTime, "Value"),
                new ActivityColorIndicatorElement(labelActiveTime, "ForeColor"),
                new ActivityColorIndicatorElement(labelProgress, "ForeColor"),
                new ActivityColorIndicatorElement(labelActiveProcess, "ForeColor"),
                new KeyStrokeCountElement(labelAxisX, "Text"),
                new ActivityGraphElement(CreateGraphics())
            };
        }
        
        public void ReportActivity(IActivityReport activityReport)
        {
            foreach (var control in _controls)
            {
                control.Update(activityReport);
            }
        }
    }
}
