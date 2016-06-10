using System;
using System.IO;
using IniParser;
using IniParser.Model;

namespace ActivityLogger.Main
{
    public class SettingsReader
    {
        private readonly FileIniDataParser _iniParser;
        private readonly string _fileName;
        
        public SettingsReader()
        {
            _iniParser = new FileIniDataParser();

            var appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ActivityLogger");
            _fileName = Path.Combine(appDataFolder, "ActivityLogger.ini");

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

        public int GetSettingAsInt(string section, string setting)
            => int.Parse(GetSetting(section, setting));

        public float GetSettingAsFloat(string section, string setting)
            => float.Parse(GetSetting(section, setting));

        private string GetSetting(string section, string setting)
        {
            var data = _iniParser.ReadFile(_fileName);

            var value = data[section][setting];
            if (value != null)
                return value;

            // Return default values if data could not be retrieved from ini file
            if (Settings.DefaultValues.ContainsKey(section) && Settings.DefaultValues[section].ContainsKey(setting))
                return Settings.DefaultValues[section][setting].ToString();

            throw new InvalidDataException("No value could be found in ini file and setting has no default value.");
        }
    }
}
