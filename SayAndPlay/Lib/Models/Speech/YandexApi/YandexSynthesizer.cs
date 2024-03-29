﻿using System;
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
        private readonly string voice;

        public YandexSynthesizer(string voice)
        {
            this.voice = voice;
        }

        public async Task<byte[]> SynthesizeAsync(string text)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + IamToken);
            var values = new Dictionary<string, string>
            {
                { "text", text},
                { "lang", Lang},
                { "folderId", FolderId},
                { "voice", this.voice}
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(SynthesizeApiUrl, content);

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
