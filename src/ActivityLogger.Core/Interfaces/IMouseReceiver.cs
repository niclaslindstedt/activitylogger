using AL.Core.Models;

namespace AL.Core.Interfaces
{
    public interface IMouseReceiver
    {
        void ReportMouse(MouseReport mouseReport);
    }
}
