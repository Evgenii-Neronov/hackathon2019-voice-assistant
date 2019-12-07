using System;
using System.IO;
using Newtonsoft.Json;

namespace Lib.Models.Settings
{
    public class UserSettings
    {
        public Recognizer Recognizer { get; set; }
        public Synthesizer Synthesizer { get; set; }

        public static UserSettings Load(Guid userId)
        {
            var fileName = Path.Combine(AppSettings.GetSettingsPath(), $"{userId}.json");

            return File.Exists(fileName) 
                ? JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(fileName))
                : new UserSettings();
        }

        public static void Save(Guid userId, UserSettings settings)
        {
            var fileName = Path.Combine(AppSettings.GetSettingsPath(), $"{userId}.json");

            File.WriteAllText(fileName, JsonConvert.SerializeObject(settings));
        }
    }
}
