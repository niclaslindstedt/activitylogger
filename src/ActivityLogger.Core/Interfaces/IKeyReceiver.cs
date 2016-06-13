using AL.Core.Models;

namespace AL.Core.Interfaces
{
    public interface IKeyReceiver
    {
        void ReportKey(KeyReport keyReport);
    }
}
