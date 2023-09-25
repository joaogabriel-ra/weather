public class Forecast
{
    public Properties properties { get; set; }
}

public class Properties
{
    public List<Period> Periods { get; set; }
}

public class ProbabilityOfPrecipitation
{
    public string UnitCode { get; set; }
    public object Value { get; set; }
}

public class Dewpoint
{
    public string UnitCode { get; set; }
    public double Value { get; set; }
}

public class RelativeHumidity
{
    public string UnitCode { get; set; }
    public int Value { get; set; }
}

public class Period
{
    public int Number { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsDaytime { get; set; }
    public int Temperature { get; set; }
    public string TemperatureUnit { get; set; }
    public object TemperatureTrend { get; set; }
    public ProbabilityOfPrecipitation ProbabilityOfPrecipitation { get; set; }
    public Dewpoint Dewpoint { get; set; }
    public RelativeHumidity RelativeHumidity { get; set; }
    public string WindSpeed { get; set; }
    public string WindDirection { get; set; }
    public string Icon { get; set; }
    public string ShortForecast { get; set; }
    public string DetailedForecast { get; set; }
}
