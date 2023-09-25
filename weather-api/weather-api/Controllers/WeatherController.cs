using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web.Http.Cors;
using weather_api.Classes;

namespace weather_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public WeatherController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("forecast")]
        public async Task<IActionResult> GetForecast([FromQuery] string address)
        {
            try
            {
                GeocodingService geocodingService = new GeocodingService(new HttpClient());
                var (latitude, longitude) = await geocodingService.GetCoordinatesAsync(address);
                string apiUrl = $"https://api.weather.gov/points/{Math.Round(latitude, 4)},{Math.Round(longitude, 4)}";
                Weather weatherResponse = await GetResponseFromUrl<Weather>(apiUrl);
                string forecastUrl = weatherResponse.properties.forecast;
                Forecast forecastResponse = await GetResponseFromUrl<Forecast>(forecastUrl);

                return Ok(forecastResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        public async Task<T> GetResponseFromUrl<T>(string url) where T : class
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "Weather");

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    T responseObject = JsonConvert.DeserializeObject<T>(content);

                    return responseObject;
                }

                throw new Exception($"Unable to fetch data from {url}. Status code: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching data from {url}.", ex);
            }
        }
    }
}