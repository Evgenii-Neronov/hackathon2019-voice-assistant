using System.Web;
using System.Web.Http;
using Lib.Helpers;
using Lib.Models;

namespace Web.Controllers
{
    public class SpeechApiController : ApiController
    {
        private readonly SpeechFactory speechFactory = new SpeechFactory();

        [HttpPost]
        [Route("v1/recognize/")]
        public string Recognize()
        {
            var httpRequest = HttpContext.Current.Request;

            var bytes = httpRequest.Files[0].InputStream.ToBytes();

            var recognizer = this.speechFactory.GetRecognizer(Recognizer.YandexApi);

            return recognizer.Recognize(bytes);
        }

        [HttpGet]
        [Route("v1/synthesize/")]
        public byte[] Synthesize(string text)
        {
            var synthesizer = this.speechFactory.GetSynthesizer(Synthesizer.MicrosoftVoice);

            return synthesizer.Synthesize(text);
        }
    }
}
