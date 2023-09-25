namespace weather_api.Classes
{
    public class Geocoding
    {
        public Result Result { get; set; }
    }

    public class Result
    {
        public AddressMatch[] AddressMatches { get; set; }
    }

    public class AddressMatch
    {
        public Coordinates Coordinates { get; set; }
    }

    public class Coordinates
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
