using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lib.Models;

namespace Web.Models
{
    public class SettingsModel
    {
        public Recognizer Recognizer { get; set; }

        public Synthesizer Synthesizer { get; set; }
    }
}