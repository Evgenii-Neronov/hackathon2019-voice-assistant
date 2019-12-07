using System;
using Lib.Models.Settings;
using Lib.Models.Speech;
using Lib.Models.Speech.GoogleApi;
using Lib.Models.Speech.MicrosoftApi;
using Lib.Models.Speech.YandexApi;

namespace Lib.Models
{
    public class SpeechFactory
    {
        public IRecognizer GetRecognizer(UserSettings userSettings)
        {
            switch (userSettings.Recognizer)
            {
                case Recognizer.GoogleApi:
                    return new GoogleRecognizer();
                case Recognizer.YandexApi:
                    return new YandexRecognizer();
            };

            throw new NotSupportedException("Не поддерживаемый тип Recognizer");
        }

        public ISynthesizer GetSynthesizer(UserSettings userSettings)
        {
            switch (userSettings.Synthesizer)
            {
                case Synthesizer.MicrosoftVoice:
                    return new MicrosoftSynthesizer("Microsoft Irina Desktop");
                case Synthesizer.IvonaVoice:
                    return new MicrosoftSynthesizer("IVONA 2 Tatyana OEM");
                case Synthesizer.IvonaMaxim:
                    return new MicrosoftSynthesizer("IVONA 2 Maxim OEM");
                case Synthesizer.YandexOksana:
                    return new YandexSynthesizer("oksana");
                case Synthesizer.YandexJane:
                    return new YandexSynthesizer("jane");
                case Synthesizer.YandexOmazh:
                    return new YandexSynthesizer("omazh");
                case Synthesizer.YandexZahar:
                    return new YandexSynthesizer("zahar");
                case Synthesizer.YandexErmil:
                    return new YandexSynthesizer("ermil");
            }

            throw new NotSupportedException("Не поддерживаемый тип Synthesizer");
        }
    }
}