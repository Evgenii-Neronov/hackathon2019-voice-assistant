using System;
using Lib.Models.Speech;
using Lib.Models.Speech.GoogleApi;
using Lib.Models.Speech.MicrosoftApi;
using Lib.Models.Speech.YandexApi;

namespace Lib.Models
{
    public class SpeechFactory
    {
        public IRecognizer GetRecognizer(Recognizer recognizer)
        {
            switch (recognizer)
            {
                case Recognizer.GoogleApi:
                    return new GoogleRecognizer();
                case Recognizer.YandexApi:
                    return new YandexRecognizer();
            };

            throw new NotSupportedException("Не поддерживаемый тип Recognizer");
        }

        public ISynthesizer GetSynthesizer(Synthesizer synthesizer)
        {
            switch (synthesizer)
            {
                case Synthesizer.MicrosoftVoice:
                    return new MicrosoftSynthesizer("Microsoft Irina Desktop");
                case Synthesizer.IvonaVoice:
                    return new MicrosoftSynthesizer("IVONA 2 Tatyana OEM");
                case Synthesizer.YandexVoice:
                    return new YandexSynthesizer();
            }

            throw new NotSupportedException("Не поддерживаемый тип Synthesizer");
        }
    }
}