using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Lib.Helpers;
using Lib.Models;
using Lib.Models.History;
using Lib.Models.Settings;

namespace Web.Controllers
{
    public class SpeechApiController : ApiController
    {
        private readonly SpeechFactory speechFactory = new SpeechFactory();

        private UserSettings UserSettings => UserSettings.Load(this.GetUserId());

        [HttpPost]
        [Route("v1/recognize/")]
        public async Task<string> RecognizeAsync()
        {
            var httpRequest = HttpContext.Current.Request;

            var bytes = httpRequest.Files[0].InputStream.ToBytes();

            var recognizer = this.speechFactory.GetRecognizer(this.UserSettings);

            var text = await recognizer.RecognizeAsync(bytes);

            UserHistory.Save(this.GetUserId(), new HistoryItem(Who.User, text));

            return text;
        }

        [HttpGet]
        [Route("v1/synthesize/")]
        public Task<byte[]> SynthesizeAsync(string text)
        {
            UserHistory.Save(this.GetUserId(), new HistoryItem(Who.Assistent, text));

            var synthesizer = this.speechFactory.GetSynthesizer(this.UserSettings);

            return synthesizer.SynthesizeAsync(text);
        }

        [Route("v1/audio/"), HttpGet]
        public async Task<HttpResponseMessage> AudioAsync(string text)
        {
            var data = await this.SynthesizeAsync(text);

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

        private Guid GetUserId()
        {
            var cookie = Request.Headers.GetCookies("Hackathon2019_PlayAndSay").FirstOrDefault();

            var userId = cookie?.Cookies?.FirstOrDefault()?.Values["UserId"];

            return userId != null ? Guid.Parse(userId) : new Guid();
        }
    }
}
