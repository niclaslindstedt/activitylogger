using AL.Core.Models;

namespace AL.Core.Interfaces
{
    public interface IProcessReceiver
    {
        void ReportProcess(ProcessReport processReport);
    }
}
