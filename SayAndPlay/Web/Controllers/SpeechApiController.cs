using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using DialogFlow.PriceEstimatorApi;
using Lib.Helpers;
using Lib.Models;
using Lib.Models.History;
using Lib.Models.Settings;
using Newtonsoft.Json;
using Web.Models;

namespace Web.Controllers
{
    public class SpeechApiController : ApiController
    {
        private readonly SpeechFactory speechFactory = new SpeechFactory();
        private readonly DialogFlow.Model.DialogFlow dialogFlow = new DialogFlow.Model.DialogFlow();

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
        [Route("v1/test/")]
        public async Task<string> TestAsync()
        {
            var response = await PriceEstimator.GetFlatPriceAsync(new GetFlatPriceRequest()
            {
                address = "Санкт-Петербург, Авиаконструкторов дом 42 корпус 3",
                area = 55,
                roomsCount = 1,
                filters = new Filter1[]
                {
                    new Filter1()
                    {
                        key = "entrance",
                        value = new[] {"entranceAfterRepair"}
                    },
                }
            });

            return response;
        }

        [HttpGet]
        [Route("v1/test2/")]
        public async Task<GetFlatPriceResponse> TestAsync2()
        {
            var response = await PriceEstimator.GetFlatPriceAsync2(new GetFlatPriceRequest()
            {
                address = "Санкт-Петербург, Авиаконструкторов дом 42 корпус 3",
                area = 55,
                roomsCount = 1,
                filters = new Filter1[]
                {
                    new Filter1()
                    {
                        key = "entrance",
                        value = new[] {"entranceAfterRepair"}
                    },
                }
            });

            return response;
        }

        [HttpGet]
        [Route("v1/talk/")]
        public async Task<string> TalkAsync(string text)
        {
            this.ChangeVoiceHint(text);

            var flowContext = UserFlowContenxt.Load(this.GetUserId());

            var talkResult = await dialogFlow.TalkAsync(text, flowContext);

            UserFlowContenxt.Save(this.GetUserId(), talkResult);

            UserHistory.Save(this.GetUserId(), new HistoryItem(Who.Assistent, talkResult.Answer));

            return talkResult.Answer;
        }

        [HttpGet]
        [Route("v1/synthesize/")]
        public Task<byte[]> SynthesizeAsync(string text)
        {
            var synthesizer = this.speechFactory.GetSynthesizer(this.UserSettings);

            return synthesizer.SynthesizeAsync(text);
        }

        [HttpGet]
        [Route("v1/audio/")]
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

        private void ChangeVoiceHint(string text)
        {
            if (text.ToLower() == "смени голос" || text.ToLower() == "Поменяй голос")
            {
                UserSettings.Save(this.GetUserId(), new UserSettings()
                {
                    Recognizer = UserSettings.Recognizer,
                    Synthesizer = UserSettings.Synthesizer.Next()
                });
            }
        }
    }
}
