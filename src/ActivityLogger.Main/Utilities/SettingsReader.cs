using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IniParser;
using IniParser.Model;

namespace AL.Core.Utilities
{
    public class SettingsReader : ISettingsReader
    {
        private readonly FileIniDataParser _iniParser;
        private readonly string _fileName;
        
        public SettingsReader(string filename)
        {
            _iniParser = new FileIniDataParser();

            var appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ActivityLogger");
            _fileName = Path.Combine(appDataFolder, filename);

            if (!Directory.Exists(appDataFolder))
                Directory.CreateDirectory(appDataFolder);

            if (!File.Exists(_fileName))
                InitializeSettingsFile(_fileName);

            try
            {
                _iniParser.ReadFile(_fileName);
            }
            catch
            {
                InitializeSettingsFile(_fileName);
            }
        }

        private void InitializeSettingsFile(string settingsFilename)
        {
            var data = new IniData();

            _iniParser.WriteFile(settingsFilename, data);
        }

        public Dictionary<string, object> GetSection(string sectionName)
        {
            var data = _iniParser.ReadFile(_fileName);
            var sectionData = GetSectionData(data, sectionName);

            return sectionData.Keys.ToDictionary<KeyData, string, object>(s => s.KeyName, s => s.Value);
        }

        public bool GetSettingAsBool(string sectionName, string settingName, object defaultValue)
            => bool.Parse(GetSetting(sectionName, settingName, defaultValue));

        public int GetSettingAsInt(string sectionName, string settingName, object defaultValue)
            => int.Parse(GetSetting(sectionName, settingName, defaultValue));

        public float GetSettingAsFloat(string sectionName, string settingName, object defaultValue)
            => float.Parse(GetSetting(sectionName, settingName, defaultValue));

        public string GetSettingAsString(string sectionName, string settingName, object defaultValue)
            => GetSetting(sectionName, settingName, defaultValue);

        public void WriteSetting(string sectionName, string settingName, string value)
        {
            var data = _iniParser.ReadFile(_fileName);
            var sectionData = GetSectionData(data, sectionName);
            sectionData.Keys.AddKey(settingName, value);
            _iniParser.WriteFile(_fileName, data);
        }

        private string GetSetting(string sectionName, string settingName, object defaultValue)
        {
            var data = _iniParser.ReadFile(_fileName);
            var setting = GetSettingData(data, sectionName, settingName, defaultValue);

            return setting.Value;
        }

        private KeyData GetSettingData(IniData data, string sectionName, string settingName, object defaultValue)
        {
            var sectionData = GetSectionData(data, sectionName);
            var keyData = sectionData.Keys.GetKeyData(settingName);
            if (keyData == null)
            {
                sectionData.Keys.AddKey(settingName, defaultValue.ToString());
                _iniParser.WriteFile(_fileName, data);
                keyData = sectionData.Keys.GetKeyData(settingName);
            }

            return keyData;
        }

        private SectionData GetSectionData(IniData data, string sectionName)
        {
            var sectionData = data.Sections.GetSectionData(sectionName);
            if (sectionData == null)
            {
                data.Sections.AddSection(sectionName);
                _iniParser.WriteFile(_fileName, data);
                sectionData = data.Sections.GetSectionData(sectionName);
            }

            return sectionData;
        }


    }
}
