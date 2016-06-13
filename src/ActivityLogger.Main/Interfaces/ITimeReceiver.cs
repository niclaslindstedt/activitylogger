using AL.Core.Models;

namespace AL.Core.Interfaces
{
    public interface ITimeReceiver
    {
        void ReportTime(TimeReport timeReport);
    }
}
