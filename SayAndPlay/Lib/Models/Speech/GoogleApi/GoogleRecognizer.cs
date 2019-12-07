using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Speech.V1;

namespace Lib.Models.Speech.GoogleApi
{
    public class GoogleRecognizer : IRecognizer
    {
        public async Task<string> RecognizeAsync(byte[] bytes)
        {
            var speech = SpeechClient.Create();

            var response = await speech.RecognizeAsync(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 48000,
                LanguageCode = "ru",
            }, RecognitionAudio.FromBytes(bytes));

            return string.Join(" ", response.Results.SelectMany(x => x.Alternatives.Select(a => a.Transcript)));
        }
    }
}
