using System.Reflection;

namespace AL.Tests.Helpers
{
    public static class SingletonHelper
    {
        public static void Dispose<TType>(TType objectToDispose)
        {
            var field = typeof(TType).GetField("_instance", BindingFlags.Static | BindingFlags.NonPublic);
            field?.SetValue(objectToDispose, null);
        }
    }
}
