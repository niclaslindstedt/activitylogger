using System.Linq;
using System.Windows.Forms;
using AL.Core.Models;
using AL.Gui.Graph;

namespace AL.Gui.Elements
{
    public class KeyStrokeCountElement : IControlUpdater
    {
        private readonly Control _coupledControl;
        private readonly string _propertyName;

        public KeyStrokeCountElement(Control coupledControl, string propertyName)
        {
            _coupledControl = coupledControl;
            _propertyName = propertyName;
        }

        public void Update(IActivityReport activityReport)
        {
            var kpmList = ActivityGraphHelper.GetKpmList(activityReport);
            
            _coupledControl.SetPropertyThreadSafe(_propertyName, kpmList.Max().ToString());
        }
    }
}
