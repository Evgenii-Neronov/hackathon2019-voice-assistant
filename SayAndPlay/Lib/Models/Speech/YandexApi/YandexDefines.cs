using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models.Speech.YandexApi
{
    public class YandexDefines
    {
        protected const string SynthesizeApiUrl = "https://tts.api.cloud.yandex.net/speech/v1/tts:synthesize";
        protected const string RecognizeApiUrl = "https://stt.api.cloud.yandex.net/speech/v1/stt:recognize";

        protected const string FolderId = "b1gtgbgn9s7i2cvuc1m6";
        protected const string IamToken = "CggVAgAAABoBMxKABEwNULBJiJvAstuAE9GJLm95dDr_lvTJRV8h5Zd1HQ74ZTlFtaYYcqqwwt7Z9A9XtC40el9hZFReBLsk7OFZHNi6K3BrgoyrS-hU5EJBuyGVD7TiCRrN9xwB_aQhSELaD-Du6ySK7WCXUytxi1uklPVOb4wEpc9mtNQF868UITYRVj1ggisIV1Wmc6pPxx8hXiS3G0GT5uWKL2TsWrhNIkfycFsZ1yz-eaLI-oUv34ljGqCo6VwMneX7Mmer4w52f3TLKddOisy4D3XyDyQ2gWegjXjxXoJtn4u7QCfonCeZ7F_tX0ZOqYL0WhdeG5Hahm7x_M7k7_dFiyLsJfiB9AMTaMuzh7JQjEV1BYlSTmghk5fC0VjQ0qDchTyaSbXdusELncb6Re3I-zr-_tuvVH4p4jF6wxHcWt-2PNrHOAatIeD13uiLd5VnuSMpJSQITb8LXcKzm6oEz6Y8sFjy26XdBuXkiru7KQuIpqV1oMPS-bGYiLDVps6sVPByIvJFSHoI-fP2dFPCEWcqDAkW9c_1hO7kxebQyQpkXJTo56BYqbFQz3YgwFOM0syM-CzPd6UZUSoNoqjvHM9h82pm1F4zwC7e0-crHcsxXdkPqPJYP0Dy-ifAFsdr1ObdG8xJmQMswt9vXwuaMxlHfQV9HezO5_fXazdD0LZS1ZiMoVzHGiQQ-Oqx7wUYuLy07wUiFgoUYWplNWJtazQwZXMwN2NoNjQ3MjE="; // Укажите IAM-токен.
        
        protected const string Lang = "ru-RU";
        
        protected const string Topic = "maps";  // другой вариант: "general";
        protected const string Format = "lpcm";
        protected const string SampleRateHertz = "48000";
    }
}