using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Lib.Models.History
{
    public class UserHistory
    {
        public List<HistoryItem> HistoryItems = new List<HistoryItem>();

        public static UserHistory Load(Guid userId)
        {
            var fileName = Path.Combine(AppSettings.GetHistoryPath(), $"{userId}.json");

            return File.Exists(fileName)
                ? JsonConvert.DeserializeObject<UserHistory>(File.ReadAllText(fileName))
                : new UserHistory();
        }

        public static void Save(Guid userId, HistoryItem historyItem)
        {
            var userHistory = UserHistory.Load(userId);

            userHistory.HistoryItems.Add(historyItem);

            var fileName = Path.Combine(AppSettings.GetHistoryPath(), $"{userId}.json");

            File.WriteAllText(fileName, JsonConvert.SerializeObject(userHistory));
        }

        public static void Clear(Guid userId)
        {
            var fileName = Path.Combine(AppSettings.GetHistoryPath(), $"{userId}.json");

            File.WriteAllText(fileName, JsonConvert.SerializeObject(new UserHistory()));
        }
    }
}
