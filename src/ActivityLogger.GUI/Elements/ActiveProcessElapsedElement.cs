using System;
using System.Windows.Forms;
using AL.Core.Models;

namespace AL.Gui.Elements
{
    public class ActiveProcessElapsedElement : IControlUpdater
    {
        private readonly Control _coupledControl;
        private readonly string _propertyName;

        public ActiveProcessElapsedElement(Control coupledControl, string propertyName)
        {
            _coupledControl = coupledControl;
            _propertyName = propertyName;
        }

        public void Update(IActivityReport activityReport)
        {
            var elapsed = TimeSpan.FromSeconds(0).ToString("g");

            if (activityReport.CurrentActivity != null)
                elapsed = activityReport.CurrentActivity.Elapsed.ToString("g");

            _coupledControl.SetPropertyThreadSafe(_propertyName, elapsed);
        }
    }
}
