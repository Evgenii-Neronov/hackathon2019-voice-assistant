using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DialogFlow.Model;
using Lib;
using Newtonsoft.Json;

namespace Web.Models
{
    public class UserFlowContenxt
    {
        public FlowContext FlowContenxt { get; set; }

        public static FlowContext Load(Guid userId)
        {
            var fileName = Path.Combine(AppSettings.GetSettingsPath(), $"{userId}-flow.json");

            return File.Exists(fileName)
                ? JsonConvert.DeserializeObject<FlowContext>(File.ReadAllText(fileName))
                : new FlowContext(File.ReadAllText(AppSettings.GetDialogFlowPath()));
        }

        public static void Save(Guid userId, FlowContext settings)
        {
            var fileName = Path.Combine(AppSettings.GetSettingsPath(), $"{userId}-flow.json");

            File.WriteAllText(fileName, JsonConvert.SerializeObject(settings));
        }
    }
}