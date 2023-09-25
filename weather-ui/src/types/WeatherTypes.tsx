export interface ForecastData {
  properties: Properties;
}

export interface Properties {
  periods: Period[];
}

export interface ProbabilityOfPrecipitation {
  unitCode: string;
  value: string;
}

export interface Dewpoint {
  unitCode: string;
  value: number;
}

export interface RelativeHumidity {
  unitCode: string;
  value: number;
}

export interface Period {
  number: number;
  name: string;
  startTime: Date;
  endTime: Date;
  isDaytime: boolean;
  temperature: number;
  temperatureUnit: string;
  temperatureTrend: string;
  probabilityOfPrecipitation: ProbabilityOfPrecipitation;
  dewpoint: Dewpoint;
  relativeHumidity: RelativeHumidity;
  windSpeed: string;
  windDirection: string;
  icon: string;
  shortForecast: string;
  detailedForecast: string;
}
