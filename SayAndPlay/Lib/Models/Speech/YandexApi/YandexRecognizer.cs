using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lib.Models.Speech.YandexApi
{
    public class YandexRecognizer : YandexDefines, IRecognizer
    {
        public async Task<string> RecognizeAsync(byte[] bytes)
        {
            var url = $"{RecognizeApiUrl}?topic={Topic}&folderId={FolderId}&format={Format}&sampleRateHertz={SampleRateHertz}&lang={Lang}";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + IamToken);
            
            var byteContent = new ByteArrayContent(bytes);
            var response = await client.PostAsync(url, byteContent);

            var jsonString = await response.Content.ReadAsStringAsync();

            var recoginzeResponse = JsonConvert.DeserializeObject<RecoginzeResponse>(jsonString);

            return recoginzeResponse.Result;
        }

        private class RecoginzeResponse
        {
            public string Result { get; set; }
        }
    }
}
