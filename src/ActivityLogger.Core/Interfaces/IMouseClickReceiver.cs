using AL.Core.Models;

namespace AL.Core.Interfaces
{
    public interface IMouseClickReceiver
    {
        void ReportMouseClick(MouseClickReport mouseReport);
    }
}
