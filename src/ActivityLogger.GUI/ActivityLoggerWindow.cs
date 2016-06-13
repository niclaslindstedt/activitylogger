using System.Drawing;
using System.Windows.Forms;
using AL.Core.Interfaces;
using AL.Core.Models;

namespace AL.Gui
{
    public partial class ActivityLoggerWindow : Form, IActivityReceiver
    {
        public ActivityLoggerWindow()
        {
            InitializeComponent();
        }

        public void ReportActivity(ActivityReport activityReport)
        {
            progressBarActiveTime.SetPropertyThreadSafe("Value", activityReport.PercentOfWorkDay);

            if (activityReport.UserIsWorking)
            {
                labelActiveTimeValue.SetPropertyThreadSafe("Text", activityReport.ElapsedProcessTimeString);
                labelActiveProcessValue.SetPropertyThreadSafe("Text", $"{activityReport.ProcessDescription} (Work)");

                labelActiveTime.SetPropertyThreadSafe("ForeColor", Color.DarkGreen);
                labelProgress.SetPropertyThreadSafe("ForeColor", Color.DarkGreen);
                labelActiveProcess.SetPropertyThreadSafe("ForeColor", Color.DarkGreen);
            }
            else
            {
                if (activityReport.UserIsActive)
                {
                    labelActiveProcessValue.SetPropertyThreadSafe("Text", $"{activityReport.ProcessDescription} (Leisure)");
                    labelActiveTimeValue.SetPropertyThreadSafe("Text", activityReport.ElapsedNonWorkTimeString);
                }
                else
                {
                    labelActiveProcessValue.SetPropertyThreadSafe("Text", $"{activityReport.ProcessDescription} (Idle)");
                    labelActiveTimeValue.SetPropertyThreadSafe("Text", activityReport.ElapsedIdleTimeString);
                }


                labelActiveProcessValue.SetPropertyThreadSafe("Text", activityReport.ProcessName);

                labelActiveTime.SetPropertyThreadSafe("ForeColor", Color.DarkRed);
                labelProgress.SetPropertyThreadSafe("ForeColor", Color.DarkRed);
                labelActiveProcess.SetPropertyThreadSafe("ForeColor", Color.DarkRed);
            }
        }
    }
}
