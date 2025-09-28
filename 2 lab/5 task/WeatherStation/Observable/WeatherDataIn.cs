using WeatherStation.Info;

namespace WeatherStation.Observable;

public class WeatherDataIn : Observable<WeatherInfo>
{
    public double Temperature { get; private set; } = 0;
    public double Humidity { get; private set; } = 0;
    public double Pressure { get; private set; } = 760;
    public double WindSpeed { get; private set; } = 0;
    public double WindDirection { get; private set; } = 0;

    protected override WeatherInfo GetChangedData()
    {
        return new WeatherInfo
        {
            humidity = Humidity,
            pressure = Pressure,
            temperature = Temperature,
            wind = new WindInfo()
            {
                speed = WindSpeed,
                direction = WindDirection
            }
        };
    }

    public void MeasurementsChanged()
    {
        NotifyObservers();
    }

    public void SetMeasurements( double temperature, double humidity, double pressure, double windSpeed, double windDirection )
    {
        Temperature = temperature;
        Humidity = humidity;
        Pressure = pressure;
        WindSpeed = windSpeed;
        WindDirection = windDirection;

        MeasurementsChanged();
    }
}