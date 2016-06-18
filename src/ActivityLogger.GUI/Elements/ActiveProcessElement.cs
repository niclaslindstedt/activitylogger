using System.Windows.Forms;
using AL.Core.Models;

namespace AL.Gui.Elements
{
    public class ActiveProcessElement : IControlUpdater
    {
        private readonly Control _coupledControl;
        private readonly string _propertyName;

        public ActiveProcessElement(Control coupledControl, string propertyName)
        {
            _coupledControl = coupledControl;
            _propertyName = propertyName;
        }

        public void Update(IActivityReport activityReport)
        {
            var activityType = activityReport.ActivityType;

            if (!activityReport.UserIsActive)
                activityType = activityReport.UserIsIdle ? "Idle" : "Other";
            
            _coupledControl.SetPropertyThreadSafe(_propertyName, $"{activityReport.ProcessDescription} ({activityType})");
        }
    }
}
