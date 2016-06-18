using System.Drawing;
using System.Windows.Forms;
using AL.Core.Models;

namespace AL.Gui.Elements
{
    public class ActivityColorIndicatorElement : IControlUpdater
    {
        private readonly Control _coupledControl;
        private readonly string _propertyName;

        public ActivityColorIndicatorElement(Control coupledControl, string propertyName)
        {
            _coupledControl = coupledControl;
            _propertyName = propertyName;
        }

        public void Update(IActivityReport activityReport)
        {
            Color color;

            if (activityReport.UserIsActive)
            {
                color = Color.DarkGreen;
            }
            else if (activityReport.UserIsIdle)
            {
                color = Color.DarkRed;
            }
            else
            {
                color = Color.OrangeRed;
            }
            
            _coupledControl.SetPropertyThreadSafe(_propertyName, color);
        }
    }
}
