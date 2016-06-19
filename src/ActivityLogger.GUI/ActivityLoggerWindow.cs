using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AL.Core.Interfaces;
using AL.Core.Models;
using AL.Gui.Elements;

namespace AL.Gui
{
    public partial class ActivityLoggerWindow : Form, IActivityReceiver
    {
        private static IActivityReport _activityReport;
        private static bool _graphThreadStarted = false;
        private static readonly object ThreadLock = new object();

        private readonly IList<IControlUpdater> _controls;

        public ActivityLoggerWindow()
        {
            InitializeComponent();

            DoubleBuffered = true;
            
            _controls = new List<IControlUpdater>
            {
                new LogElement(textBoxLog, "Text"),
                new ProcessReportElement(textBoxProcesses, "Text"),
                new ActiveProcessElement(labelActiveProcessValue, "Text"),
                new ActiveProcessElapsedElement(labelActiveTimeValue, "Text"),
                new ProgressElement(progressBarActiveTime, "Value"),
                new ActivityColorIndicatorElement(labelActiveTime, "ForeColor"),
                new ActivityColorIndicatorElement(labelProgress, "ForeColor"),
                new ActivityColorIndicatorElement(labelActiveProcess, "ForeColor")
            };
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
    }
}
