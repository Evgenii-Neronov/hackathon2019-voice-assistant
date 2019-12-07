using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public enum Recognizer
    {
        GoogleApi,
        YandexApi
    }

    public enum Synthesizer
    {
        MicrosoftVoice,
        IvonaVoice,
        IvonaMaxim,
        YandexOksana,
        YandexJane,
        YandexOmazh,
        YandexZahar,
        YandexErmil
    }
}
