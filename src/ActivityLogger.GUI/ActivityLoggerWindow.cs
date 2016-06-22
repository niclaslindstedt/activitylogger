using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AL.Core;
using AL.Core.Interfaces;
using AL.Core.Models;
using AL.Gui.Elements;

namespace AL.Gui
{
    public partial class ActivityLoggerWindow : Form, IActivityReceiver
    {
        private IReportCentral _reporter;
        private static IActivityReport _activityReport;
        private static bool _graphThreadStarted;
        private static readonly object ThreadLock = new object();
        
        private readonly IList<IControlUpdater> _controls;

        public ActivityLoggerWindow()
        {
            InitializeComponent();

            _controls = new List<IControlUpdater>
            {
                new LogElement(textBoxLog, "Text"),
                new ProcessReportElement(textBoxProcesses, "Text"),
                new ActiveProcessElement(labelActiveProcessValue, "Text"),
                new ActiveProcessElapsedElement(labelProcessTimeValue, "Text"),
                new ActivityTimeElapsedElement(labelActivityTimeValue, "Text"),
                new ProgressElement(progressBarProgress, "Value"),
                new ProcessShareElement(progressBarProcessShare, "Value"),
                new TimeUntilIdleProgressElement(progressBarTimeUntilIdle, "Value"),
                new ActivityColorIndicatorElement(labelActivityTime, "ForeColor"),
                new ActivityColorIndicatorElement(labelProcessTime, "ForeColor"),
                new ActivityColorIndicatorElement(labelActiveProcess, "ForeColor"),
                new ActivityColorIndicatorElement(labelProgress, "ForeColor"),
                new ActivityColorIndicatorElement(labelProcessShare, "ForeColor"),
                new ActivityColorIndicatorElement(labelTimeUntilIdle, "ForeColor"),
                new ActivityColorIndicatorElement(groupBoxProcessInformation, "ForeColor"),
                new ActivityColorIndicatorElement(groupBoxProgressInformation, "ForeColor")
            };
        }

        public void Register(IReportCentral reporter)
        {
            _reporter = reporter;
        }

        public void StartGraphThread(params IControlUpdater[] controls)
        {
            Task.Factory.StartNew(() =>
            {
                _graphThreadStarted = true;

                while (true)
                {
                    Thread.Sleep(250);
                    lock (ThreadLock)
                    {
                        _graphThreadStarted = true;

                        foreach (var control in controls)
                        {
                            control.Update(_activityReport);
                        }
                    }
                }
            });
            
            _graphThreadStarted = false;
        }
        
        public void ReportActivity(IActivityReport activityReport)
        {
            lock (ThreadLock)
            {
                _activityReport = activityReport;
            }

            if (!_graphThreadStarted)
            {
                StartGraphThread(
                    new KeyStrokeCountElement(labelAxisX, "Text"),
                    new ActivityGraphElement(CreateGraphics()));
            }

            foreach (var control in _controls)
            {
                control.Update(activityReport);
            }
        }

        private void buttonReset_Click(object sender, System.EventArgs e)
        {
            _reporter?.Reset();
        }
    }
}
