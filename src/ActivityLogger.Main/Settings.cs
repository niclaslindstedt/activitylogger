using System.Collections.Generic;
using System.Linq;
using SettingsCollection = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, object>>;

namespace ActivityLogger.Main
{
    public class Settings
    {
        public int AllowedIdleSecondsForWork { get; private set; }
        public int AllowedIdleSecondsForWorkRelated { get; private set; }
        public int SecondsBeforeConsideredIdle { get; private set; }
        public List<string> WorkProcesses { get; private set; }
        public List<string> WorkRelatedProcesses { get; private set; }

        private readonly ISettingsReader _settingsReader;

        public static SettingsCollection DefaultValues = new SettingsCollection
            {
                {
                    "TimeConstraints", new Dictionary<string, object>
                    {
                        {"AllowedIdleSecondsForWork", 10},
                        {"AllowedIdleSecondsForWorkRelated", 15},
                        {"SecondsBeforeConsideredIdle", 5}
                    }
                }, {
                    "WorkProcesses", new Dictionary<string, object>
                    {
                        {"1", "Microsoft Visual Studio"}
                    }
                }, {
                    "WorkRelatedProcesses", new Dictionary<string, object>
                    {
                        {"1", "chrome"},
                        {"2", "slack"}
                    }
                }
            };

        public Settings(ISettingsReader settingsReader)
        {
            _settingsReader = settingsReader;

            AllowedIdleSecondsForWork = GetSettingAsInt("TimeConstraints", "AllowedIdleSecondsForWork");
            AllowedIdleSecondsForWorkRelated = GetSettingAsInt("TimeConstraints", "AllowedIdleSecondsForWorkRelated");
            SecondsBeforeConsideredIdle = GetSettingAsInt("TimeConstraints", "SecondsBeforeConsideredIdle");

            SetWorkProcesses();
            SetWorkRelatedProcesses();
        }

        private int GetSettingAsInt(string sectionName, string settingName)
        {
            return _settingsReader.GetSettingAsInt(sectionName, settingName, DefaultValues[sectionName][settingName]);
        }

        private IEnumerable<string> GetSection(string sectionName)
        {
            var workProcesses = _settingsReader.GetSection(sectionName);
            if (workProcesses == null || workProcesses.Count == 0)
                return DefaultValues[sectionName].Select(x => x.Value.ToString()).ToList();

            return new List<string>();
        }

        private void SetWorkProcesses()
        {
            var workProcesses = _settingsReader.GetSection("WorkProcesses");
            if (workProcesses == null || workProcesses.Count == 0)
            {
                WorkProcesses = DefaultValues["WorkProcesses"].Select(x => x.Value.ToString()).ToList();
                for (var i = 0; i < WorkProcesses.Count; ++i)
                {
                    _settingsReader.WriteSetting("WorkProcesses", i.ToString(), WorkProcesses.ElementAt(i));
                }
            }
            else
            {
                WorkProcesses = workProcesses.Select(x => x.Value.ToString()).ToList();
            }
        }

        private void SetWorkRelatedProcesses()
        {
            var workRelatedProcesses = _settingsReader.GetSection("WorkRelatedProcesses");
            if (workRelatedProcesses == null || workRelatedProcesses.Count == 0)
            {
                WorkRelatedProcesses = DefaultValues["WorkRelatedProcesses"].Select(x => x.Value.ToString()).ToList();
                for (var i = 0; i < WorkRelatedProcesses.Count; ++i)
                {
                    _settingsReader.WriteSetting("WorkRelatedProcesses", i.ToString(), WorkRelatedProcesses.ElementAt(i));
                }
            }
            else
            {
                WorkRelatedProcesses = workRelatedProcesses.Select(x => x.Value.ToString()).ToList();
            }
        }
    }
}
