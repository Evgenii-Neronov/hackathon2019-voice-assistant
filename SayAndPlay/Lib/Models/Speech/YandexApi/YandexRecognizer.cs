using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace Lib.Models.Speech.YandexApi
{
    public class YandexRecognizer : YandexDefines, IRecognizer
    {
        public string Recognize(byte[] bytes)
        {
            var url = $"{RecognizeApiUrl}?topic={Topic}&folderId={FolderId}&format={Format}&sampleRateHertz={SampleRateHertz}&lang={Lang}";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + IamToken);
            
            var byteContent = new ByteArrayContent(bytes);
            var response = client.PostAsync(url, byteContent).GetAwaiter().GetResult();

            var jsonString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            var recoginzeResponse = JsonConvert.DeserializeObject<RecoginzeResponse>(jsonString);

            return recoginzeResponse.Result;
        }

        private class RecoginzeResponse
        {
            public string Result { get; set; }
        }
    }
}
