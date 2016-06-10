using System.Collections.Generic;

namespace ActivityLogger.Main
{
    public class Settings
    {
        public int AllowedIdleSecondsForWork { get; private set; }
        public int AllowedIdleSecondsForWorkRelated { get; private set; }
        public int SecondsBeforeConsideredIdle { get; private set; }

        public static Dictionary<string, Dictionary<string, object>> DefaultValues =
            new Dictionary<string, Dictionary<string, object>>()
            {
                {
                    "TimeConstraints", new Dictionary<string, object>()
                    {
                        {"AllowedIdleSecondsForWork", 10},
                        {"AllowedIdleSecondsForWorkRelated", 15},
                        {"SecondsBeforeConsideredIdle", 5}
                    }
                }
            };

        public Settings()
        {
            var settingsReader = new SettingsReader();

            AllowedIdleSecondsForWork = settingsReader.GetSettingAsInt("TimeConstraints", "AllowedIdleSecondsForWork");
            AllowedIdleSecondsForWorkRelated = settingsReader.GetSettingAsInt("TimeConstraints", "AllowedIdleSecondsForWorkRelated");
            SecondsBeforeConsideredIdle = settingsReader.GetSettingAsInt("TimeConstraints", "SecondsBeforeConsideredIdle");
        }
    }
}
