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
        protected const string IamToken = "CggVAgAAABoBMxKABLWyW4RYR-xEiTCSRFMyxmN8rNaePY5FAN-Es3mXI92JkYHtex6TbgsMMa4FUD799_6PmfL95xqQ2TJ9YVtHmu_uAPQies_TrwbiteYrjbrRuS15nZEcFuE2lbsR0yfKyTMAGIjjm3g0Os1EHaoUB5JqrImF68lCEOFNZGvP7QF0LsZjwKH2WLJeT1EUaAeJOJ23hCBe2-ziCcuFI5AL8nZOprdjI5KyHKlJCKr7KduOZKMlvdBwK0OK37IHKzeJbMud6bSfZJbjZ9Lw5BxqTG1kZCaxWCwZ1xaLQtDY5IWjv9lH_7e_PlsrsFVUaMnrG5SpeXD0NAaoD3EAJgFnJRC83GWAt37GsA_1rXpegt9X9eI4ivESCw1vE0VK1S4epchYNqVcKDNnF6x_9zdnIkNPChMguyBVn9mkvYivvTgeBWIs3zErye3LFrXJZpEOsAOpNn3ftLxozngFVcmIR5E5uEKwPaKszUrlLfxuN9a77WxzRWRm5XT21Jgu9zamEf6syO2XYZnsZE2ZLIhkvkICQNt9enSidAZ8BYD8Q2qBCIo4WXw29UKT1du0IVaNKCsuXMhkWl8uKXOvd9c1D7EmfrP8A8U_u1z6pheQnDg018E6C_MmBhcDN8UuJ1vnvYqinb1r3RbIAaao2L5x8jTuwDjizPgkKYOzBcwviIxVGiQQ6JKv7wUYqOSx7wUiFgoUYWplNWJtazQwZXMwN2NoNjQ3MjE="; // Укажите IAM-токен.
        
        protected const string Lang = "ru-RU";
        
        protected const string Topic = "maps";  // другой вариант: "general";
        protected const string Format = "lpcm";
        protected const string SampleRateHertz = "48000";
    }
}