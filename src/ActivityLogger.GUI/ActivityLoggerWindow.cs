using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ActivityLogger.Core;
using ActivityLogger.Core.Constants;

namespace ActivityLogger.GUI
{
    public partial class ActivityLoggerWindow : Form, ITimeReceiver
    {
        public ActivityLoggerWindow()
        {
            InitializeComponent();
        }

        public void ReportTime(Tuple<string, ActivityType, int> timeReport)
        {
            var processName = timeReport.Item1;
            var activityType = timeReport.Item2;
            var seconds = timeReport.Item3;
            var elapsed = TimeSpan.FromSeconds(seconds);

            labelActiveTimeValue.SetPropertyThreadSafe("Text", elapsed.ToString("g"));
        }
    }
}
