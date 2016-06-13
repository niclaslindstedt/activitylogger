using System.Reflection;
using System.Windows.Forms;

namespace AL.Gui
{
    public static class ControlExtensions
    {
        private delegate void SetControlPropertyThreadSafeDelegate(
            Control control,
            string propertyName,
            object propertyValue);

        public static void SetPropertyThreadSafe(
            this Control control,
            string propertyName,
            object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetPropertyThreadSafe),
                    new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(
                    propertyName,
                    BindingFlags.SetProperty,
                    null,
                    control,
                    new object[] { propertyValue });
            }
        }
    }
}
