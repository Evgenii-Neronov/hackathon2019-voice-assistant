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
        protected const string IamToken = "CggVAgAAABoBMxKABLT_3RdGnPTTS49VaX_ZAM4li5KNyADTDQ5zms0n0PWuCPxuQQA8fg6SCrNAzs3SOeeFomgMQhqt6ml68At7KILU5v4d8YFNEq6PI_7YaPcYBxS4ma_YkS4upsRKTZbPCu7TrSdWQ2qp6sxzUiJ2b8926OEFX94Xo13tFWPDOm9CLbpVQbpomJD7EfdD1cqR5A3hO38DeRHh-GtNTCsJ-Wgxze13QZl8Uc_-N0RE9bLfkU5qh-Eju7uEi6AQT5vYWR9FAdqZfF1cim7LHlrtfr3_iPJhGNLeaCCXwB9mPZqnyC7elKzwhkHQkUtsJVE_WxoLV3AUMGHzV_HuXHajcxGHw2UDiRUz1-i2jaPtHmeNjjxW2WPRU59d82PlABadRXkZSc7d4-cabQZQ7mA5nyRMENm57U9ATgBKJNkfyR20wVMjqG8N_VipjrAs4WmTZjvXH8ATCEumKkefV2kYz_XjqatO0NI_PQyJkztRuRzwA--FqI0Nr2JTEV6-BpejDS7JwdkDrsBA9QIavSEd2OdHWPaaIrJl4Mbaa6k9UHsBF9owODTdk5dgyNqBlIBEtE_akkJTvR-emMLQ-TeodWYKephMu-gsEt9Hv9STdDRjjOfJCAZFZdyKFFAIK32JagE80UwZrsvbbMWAwcYis_Wa_PutAR1ibkFic-JmuLKDGiQQ4vSr7wUYosau7wUiFgoUYWplNWJtazQwZXMwN2NoNjQ3MjE="; // Укажите IAM-токен.
        
        protected const string Lang = "ru-RU";
        
        protected const string Topic = "maps";  // другой вариант: "general";
        protected const string Format = "lpcm";
        protected const string SampleRateHertz = "48000";
    }
}
