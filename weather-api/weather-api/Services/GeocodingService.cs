using Newtonsoft.Json;
using weather_api.Classes;

public class GeocodingService
{
    private readonly HttpClient _httpClient;

    public GeocodingService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<(double Latitude, double Longitude)> GetCoordinatesAsync(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException("Address cannot be empty or null", nameof(address));
        }

        try
        {
            string apiUrl = $"https://geocoding.geo.census.gov/geocoder/locations/onelineaddress?address={Uri.EscapeDataString(address).Replace("%20", "+")}&benchmark=2020&format=json";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            string content = await response.Content.ReadAsStringAsync();
            Geocoding geocodingResponse = JsonConvert.DeserializeObject<Geocoding>(content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<Geocoding>();

                if (result.Result?.AddressMatches?.Length > 0)
                {
                    var firstMatch = result.Result.AddressMatches[0];
                    return (firstMatch.Coordinates.Y, firstMatch.Coordinates.X); //Lat - Lon
                }
            }

            throw new Exception("Unable to geocode the address.");
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while geocoding the address.", ex);
        }
    }
}