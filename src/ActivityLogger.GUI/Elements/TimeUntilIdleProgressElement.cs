using System.Windows.Forms;
using AL.Core.Models;

namespace AL.Gui.Elements
{
    public class TimeUntilIdleProgressElement : IControlUpdater
    {
        private readonly Control _coupledControl;
        private readonly string _propertyName;

        public TimeUntilIdleProgressElement(Control coupledControl, string propertyName)
        {
            _coupledControl = coupledControl;
            _propertyName = propertyName;
        }

        public void Update(IActivityReport activityReport)
        {
            _coupledControl.SetPropertyThreadSafe(_propertyName, activityReport.TimeUntilIdlePercentage);
        }
    }
}
