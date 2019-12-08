using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace DialogFlow.PriceEstimatorApi
{
    public class PriceEstimator
    {
        private static readonly string host = "http://beta8.price-estimator.stage.cian.ru";

        public static async Task<string> GetFlatPriceAsync(GetFlatPriceRequest request)
        {
            var json = JsonConvert.SerializeObject(request);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var response = await httpClient.PostAsync($"{host}/public/v1/get-estimation-web/", new StringContent(json));
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            return responseJson;
        }

        
        public static async Task<GetFlatPriceResponse> GetFlatPriceAsync2(GetFlatPriceRequest request)
        {
            var json = JsonConvert.SerializeObject(request);

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await httpClient.PostAsync($"{host}/public/v1/get-estimation-web/", new StringContent(json));
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GetFlatPriceResponse>(responseJson);
        }
        
    }

    public class GetFlatPriceRequest
    {
        public string address { get; set; }
        public int area { get; set; }
        public Filter1[] filters { get; set; }
        public int roomsCount { get; set; }
    }

    public class Filter1
    {
        public string key { get; set; }
        public string[] value { get; set; }
    }

    public class GetFlatPriceResponse
    {
        public Rent rent { get; set; }
        public Sale sale { get; set; }
    }

    public class Rent
    {
        public int accuracy { get; set; }
        public int price { get; set; }
        public int priceFrom { get; set; }
        public int priceTo { get; set; }
        public int taxPrice { get; set; }
        public double yieldPercent { get; set; }
    }

    public class Sale
    {
        public int accuracy { get; set; }
        public int price { get; set; }
        public int priceFrom { get; set; }
        public int priceSqm { get; set; }
        public int priceTo { get; set; }
    }
}
