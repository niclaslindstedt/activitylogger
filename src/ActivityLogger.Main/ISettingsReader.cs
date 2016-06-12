using System.Collections.Generic;

namespace ActivityLogger.Core
{
    public interface ISettingsReader
    {
        Dictionary<string, object> GetSection(string sectionName);
        bool GetSettingAsBool(string sectionName, string settingName, object defaultValue);
        int GetSettingAsInt(string sectionName, string settingName, object defaultValue);
        float GetSettingAsFloat(string sectionName, string settingName, object defaultValue);
        string GetSettingAsString(string sectionName, string settingName, object defaultValue);
        void WriteSetting(string sectionName, string settingName, string value);
    }
}
