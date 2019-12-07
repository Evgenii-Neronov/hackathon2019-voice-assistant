using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models.Speech.YandexApi
{
    public class YandexSynthesizer : YandexDefines, ISynthesizer
    {
        public byte[] Synthesize(string text)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + IamToken);
            var values = new Dictionary<string, string>
            {
                {"text", text},
                { "lang", Lang},
                { "folderId", FolderId}
            };
            var content = new FormUrlEncodedContent(values);
            var response = client.PostAsync(SynthesizeApiUrl, content).GetAwaiter().GetResult();
            var responseBytes = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();

            return responseBytes;
        }
    }
}
