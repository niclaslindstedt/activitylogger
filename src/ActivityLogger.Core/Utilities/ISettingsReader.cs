using System.Collections.Generic;

namespace AL.Core.Utilities
{
    public interface ISettingsReader
    {
        IDictionary<string, string> GetSection(string sectionName);
        bool GetSettingAsBool(string sectionName, string settingName, string defaultValue);
        int GetSettingAsInt(string sectionName, string settingName, string defaultValue);
        float GetSettingAsFloat(string sectionName, string settingName, string defaultValue);
        void WriteSetting(string sectionName, string settingName, string value);
    }
}
