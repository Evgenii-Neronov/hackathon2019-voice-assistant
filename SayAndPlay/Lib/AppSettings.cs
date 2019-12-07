using System.Configuration;
using System.IO;

namespace Lib
{
    public static class AppSettings
    {
        public static string GetSettingsPath() => ConfigurationManager.AppSettings["SettingsPath"] ?? Path.GetTempPath();

        public static string GetAudioPath() => ConfigurationManager.AppSettings["AudioPath"] ?? Path.GetTempPath();

        public static string GetHistoryPath() => ConfigurationManager.AppSettings["HistoryPath"] ?? Path.GetTempPath();
    }
}
