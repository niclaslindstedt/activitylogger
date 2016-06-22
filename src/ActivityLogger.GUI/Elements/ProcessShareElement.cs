using System.Windows.Forms;
using AL.Core.Models;

namespace AL.Gui.Elements
{
    public class ProcessShareElement : IControlUpdater
    {
        private readonly Control _coupledControl;
        private readonly string _propertyName;

        public ProcessShareElement(Control coupledControl, string propertyName)
        {
            _coupledControl = coupledControl;
            _propertyName = propertyName;
        }

        public void Update(IActivityReport activityReport)
        {
            _coupledControl.SetPropertyThreadSafe(_propertyName, (int)(activityReport.CurrentActivityShare*100));
        }
    }
}
