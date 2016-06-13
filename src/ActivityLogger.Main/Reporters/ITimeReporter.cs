namespace AL.Core.Reporters
{
    public interface ITimeReporter
    {
        int GetWorkActivity(string processName);
        int GetNonWorkActivity(string processName);
        int GetIdleTime(string processName);
    }
}