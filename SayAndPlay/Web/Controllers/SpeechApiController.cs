using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
        public Task<string> RecognizeAsync()
        {
            var httpRequest = HttpContext.Current.Request;

            var bytes = httpRequest.Files[0].InputStream.ToBytes();

            var recognizer = this.speechFactory.GetRecognizer(Recognizer.YandexApi);

            return recognizer.RecognizeAsync(bytes);
        }

        [HttpGet]
        [Route("v1/synthesize/")]
        public Task<byte[]> SynthesizeAsync(string text)
        {
            var synthesizer = this.speechFactory.GetSynthesizer(Synthesizer.MicrosoftVoice);

            return synthesizer.SynthesizeAsync(text);
        }

        [Route("v1/audio/"), HttpGet]
        public async Task<HttpResponseMessage> AudioAsync(string text)
        {
            var synthesizer = this.speechFactory.GetSynthesizer(Synthesizer.MicrosoftVoice);

            var data = await synthesizer.SynthesizeAsync(text);

            var memStream = new MemoryStream(data);

            var mediaTypeHeaderValue = new MediaTypeHeaderValue("audio/mpeg");
            if (this.Request.Headers.Range != null)
            {
                try
                {
                    var partialResponse = this.Request.CreateResponse(HttpStatusCode.PartialContent);
                    partialResponse.Content =
                        new ByteRangeStreamContent(memStream, this.Request.Headers.Range, mediaTypeHeaderValue);

                    return partialResponse;
                }
                catch (InvalidByteRangeException invalidByteRangeException)
                {
                    return this.Request.CreateErrorResponse(invalidByteRangeException);
                }
            }

            var fullResponse = this.Request.CreateResponse(HttpStatusCode.OK);
            fullResponse.Content = new StreamContent(memStream);
            fullResponse.Content.Headers.ContentType = mediaTypeHeaderValue;

            return fullResponse;
        }
    }
}
