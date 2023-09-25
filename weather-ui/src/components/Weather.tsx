import React, { useState } from "react";
import axios from "axios";
import { ForecastData, Period } from "../types/WeatherTypes";

const Weather = () => {
  const [forecast, setForecast] = useState<ForecastData | null>(null);
  const [address, setAddress] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const getForecast = async () => {
    setLoading(true);
    setError(null);

    try {
      const response = await axios.get(
        `http://localhost:5055/api/Weather/forecast?address=${address}`
      );
      setForecast(response.data);
    } catch (error) {
      console.error("Error fetching weather data:", error);
      setError("An error occurred while fetching weather data.");
    } finally {
      setLoading(false);
    }
  };

  const groupedPeriods: Period[][] = forecast
    ? forecast.properties.periods.reduce<Period[][]>(
        (result, value, index, array) => {
          if (index % 2 === 0) result.push(array.slice(index, index + 2));
          return result;
        },
        []
      )
    : [];

  const WeatherCard: React.FC<{ periods: Period[] }> = ({ periods }) => (
    <div className="bg-slate-800 p-2 rounded-lg shadow-lg text-white w-64">
      {periods.map((period) => (
        <div key={period.number} className="mb-4 p-2">
          <h2 className="text-lg font-semibold mb-1">{period.name}</h2>
          <p className="text-sm">
            High: {period.temperature.toFixed(2)} {period.temperatureUnit}
          </p>
          <p className="text-sm">
            Low: {period.dewpoint.value.toFixed(2)} {period.temperatureUnit}
          </p>
          <p className="text-sm">Conditions: {period.shortForecast}</p>
        </div>
      ))}
    </div>
  );

  return (
    <div className="min-h-screen flex flex-col justify-center items-center bg-slate-900 text-white">
      <h1 className="text-2xl font-semibold mb-4">7-Day Weather Forecast</h1>

      <div className="flex">
        <input
          type="text"
          placeholder="Enter Postal Address"
          className="border p-2 rounded-lg mr-2 text-black text-sm"
          value={address}
          onChange={(e) => setAddress(e.target.value)}
          onKeyUp={(e) => {
            if (e.key === "Enter") {
              getForecast();
            }
          }}
        />
        <button
          className="bg-indigo-500 hover:bg-indigo-600 text-white font-bold py-2 px-4 rounded-lg text-sm"
          onClick={getForecast}
          disabled={loading}
        >
          {loading ? "Loading..." : "Get Forecast"}
        </button>
      </div>

      {error && <p className="text-red-500 text-sm">{error}</p>}

      <div className="flex justify-center space-x-4 mt-4">
        {groupedPeriods.length > 0 && (
          <WeatherCard periods={groupedPeriods[0]} />
        )}
      </div>
      <div className="flex justify-center space-x-4 mt-4">
        {groupedPeriods.slice(1).map((periods, index) => (
          <WeatherCard key={index} periods={periods} />
        ))}
      </div>
    </div>
  );
};

export default Weather;
