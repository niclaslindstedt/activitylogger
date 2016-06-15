using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AL.Core.Constants;
using AL.Core.Utilities;
using SettingsCollection = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>;

namespace AL.Core
{
    public class Settings
    {
        public int SecondsBeforeConsideredIdle { get; private set; }

        public List<string> Sections { get; private set; }
        public IDictionary<string, List<string>> SectionActivities { get; private set; }
        public IDictionary<string, IDictionary<string, string>> SectionSettings { get; private set; }
        public string TrackSection { get; set; }

        private readonly ISettingsReader _settingsReader;

        public static SettingsCollection DefaultValues = new SettingsCollection
            {
                {
                    SettingStrings.Program, new Dictionary<string, string>
                    {
                        {SettingStrings.DailyHourGoal, "8"},
                        {SettingStrings.SecondsBeforeConsideredIdle, "5"}
                    }
                }, {
                    SettingStrings.Sections, new Dictionary<string, string>
                    {
                        {"1", SettingStrings.Work},
                        {"2", SettingStrings.Browsing},
                        {"3", SettingStrings.Gaming}
                    }
                }, {
                    SettingStrings.Work, new Dictionary<string, string>
                    {
                        {"1", ".+? - Microsoft Visual Studio"},
                        {"2", "Slack - .+?"},
                        {"3", ".+? - Sublime Text"}
                    }
                }, {
                    $"{SettingStrings.Work}{SettingStrings.SettingsSuffix}", new Dictionary<string, string>
                    {
                        {SettingStrings.DailyHourGoal, "4.5"},
                        {SettingStrings.SecondsBeforeConsideredIdle, "45"},
                        {SettingStrings.RelatedSections, "Browsing"},
                        {SettingStrings.RelatedSectionsGraceTime, "600"}
                    }
                }, {
                    SettingStrings.Browsing, new Dictionary<string, string>
                    {
                        {"1", "chrome.exe"}
                    }
                }, {
                    $"{SettingStrings.Browsing}{SettingStrings.SettingsSuffix}", new Dictionary<string, string>
                    {
                        {"SecondsBeforeConsideredIdle", "90"}
                    }
                }, {
                    SettingStrings.Gaming, new Dictionary<string, string>
                    {
                        {"1", "Steam.exe"}
                    }
                }, {
                    $"{SettingStrings.Gaming}{SettingStrings.SettingsSuffix}", new Dictionary<string, string>
                    {
                        {SettingStrings.DailyHourGoal, "2"},
                        {SettingStrings.SecondsBeforeConsideredIdle, "5"},
                        {SettingStrings.RelatedSections, "Browsing"},
                        {SettingStrings.RelatedSectionsGraceTime, "60"}
                    }
                }
            };

        public Settings(ISettingsReader settingsReader)
        {
            _settingsReader = settingsReader;
            
            SecondsBeforeConsideredIdle = GetSettingAsInt("ActivityLogger", "SecondsBeforeConsideredIdle");

            SetupCustomSections();
        }

        private int GetSettingAsInt(string sectionName, string settingName)
        {
            return _settingsReader.GetSettingAsInt(sectionName, settingName, DefaultValues[sectionName][settingName]);
        }
        
        private void SetupCustomSections()
        {
            InitializeSections();
            
            foreach (var section in Sections)
            {
                var sectionSettings = _settingsReader.GetSection(section);
                if (sectionSettings == null || sectionSettings.Count == 0)
                {
                    var settings = DefaultValues[section].Select(x => x.Value.ToString()).ToList();
                    for (var i = 0; i < settings.Count; ++i)
                    {
                        var index = (i + 1).ToString();
                        _settingsReader.WriteSetting(section, index, settings[i]);
                    }
                }
                else
                {
                    SectionActivities[section] = sectionSettings.Select(x => x.Value.ToString()).ToList();
                }
            }
        }

        private void InitializeSections()
        {
            SectionSettings = new Dictionary<string, IDictionary<string, string>>();
            var userDefinedSections = _settingsReader.GetSection("Sections");
            if (userDefinedSections == null || userDefinedSections.Count == 0)
            {
                Sections = DefaultValues["Sections"].Select(x => x.Value.ToString()).ToList();
                for (var i = 0; i < Sections.Count; ++i)
                {
                    var index = (i + 1).ToString();
                    _settingsReader.WriteSetting("Sections", index, Sections.ElementAt(i));
                    WriteSectionSettings(Sections.ElementAt(i));
                }
            }
            else
            {
                Sections = userDefinedSections.Select(x => x.Value.ToString()).ToList();

                foreach (var section in Sections)
                {
                    WriteSectionSettings(section);
                }
            }

            SectionActivities = new Dictionary<string, List<string>>();
            foreach (var section in Sections)
            {
                SectionActivities.Add(section, new List<string>());
            }
        }

        private void WriteSectionSettings(string section)
        {
            var userDefinedActivitySettings = _settingsReader.GetSection($"{section}Settings");
            if (userDefinedActivitySettings == null || userDefinedActivitySettings.Count == 0)
            {
                SectionSettings.Add(section, new Dictionary<string, string>());
                foreach (var setting in DefaultValues[$"{section}Settings"])
                {
                    _settingsReader.WriteSetting($"{section}Settings", setting.Key, setting.Value.ToString());
                    SectionSettings[section].Add(setting);
                }
            }
            else
            {
                SectionSettings.Add(section, new Dictionary<string, string>());
                foreach (var setting in userDefinedActivitySettings)
                {
                    SectionSettings[section].Add(setting);
                }
            }
        }

        public IDictionary<string, string> GetActivitySettings(string section)
        {
            if (!SectionSettings.ContainsKey(section))
            {
                return new Dictionary<string, string>();
            }

            return SectionSettings[section];
        }

        public string GetActivitySetting(string section, string setting)
        {
            var settings = GetActivitySettings(section);
            if (!settings.ContainsKey(setting))
            {
                var programSettings = GetActivitySettings(SettingStrings.Program);
                if (programSettings.ContainsKey(setting))
                    return programSettings[setting];

                return null;
            }

            return settings[setting];
        }

        public int GetActivitySettingAsInt(string section, string setting)
            => int.Parse(GetActivitySetting(section, setting) ?? "0", NumberStyles.Any);

        public float GetActivitySettingAsFloat(string section, string setting)
            => float.Parse(GetActivitySetting(section, setting) ?? "0.0", NumberStyles.Any);
    }
}
