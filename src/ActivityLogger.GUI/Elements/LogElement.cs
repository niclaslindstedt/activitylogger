using System;
using System.Linq;
using System.Windows.Forms;
using AL.Core.Models;
using AL.Gui.Graph;

namespace AL.Gui.Elements
{
    public class LogElement : IControlUpdater
    {
        private string _content;
        private int _nextIndex;

        private readonly Control _coupledControl;
        private readonly string _propertyName;

        public LogElement(Control coupledControl, string propertyName)
        {
            _coupledControl = coupledControl;
            _propertyName = propertyName;

            LogMessage("Activity Logger initialized");
        }
        
        public void Update(IActivityReport activityReport)
        {
            while (activityReport.LogMessages.Count > _nextIndex)
            {
                var logMessage = activityReport.LogMessages.ElementAt(_nextIndex);
                LogMessage(logMessage);

                _nextIndex++;
            }

            LogMessage(ActivityGraphHelper.GetBarColor(activityReport.TotalClicks, activityReport.TotalKeyStrokes).ToString());
        }

        public void LogMessage(string logMessage)
        {
            _content += $"[{DateTime.Now.ToString("HH:mm:ss")}] {logMessage}" + Environment.NewLine;

            _coupledControl.SetPropertyThreadSafe(_propertyName, _content);
        }
    }
}
