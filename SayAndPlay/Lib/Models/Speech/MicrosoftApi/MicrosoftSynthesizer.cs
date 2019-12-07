using System.IO;
using System.Speech.Synthesis;
using System.Threading.Tasks;

namespace Lib.Models.Speech.MicrosoftApi
{
    public class MicrosoftSynthesizer : ISynthesizer
    {
        private readonly string voiceName;

        public MicrosoftSynthesizer(string voiceName)
        {
            this.voiceName = voiceName;
        }

        public Task<byte[]> SynthesizeAsync(string text)
        {
            var task = Task.Run(() =>
            {
                using (var synth = new SpeechSynthesizer())
                using (var stream = new MemoryStream())
                {
                    synth.Rate = 0;
                    synth.Volume = 100;

                    synth.SelectVoice(voiceName);

                    synth.SetOutputToWaveStream(stream);

                    synth.Speak(text);

                    var buffer= stream.GetBuffer();

                    stream.Flush();

                    return buffer;
                }
            });

            return task;
        }
    }
}