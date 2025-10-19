using WeatherStation.Info;

namespace WeatherStation.Observable;

public class WeatherDataPro : Observable<WeatherInfoPro>
{
    public double Temperature { get; protected set; } = 0;
    public double Humidity { get; protected set; } = 0;
    public double Pressure { get; protected set; } = 760;
    public double WindSpeed { get; private set; } = 0;
    public double WindDirection { get; private set; } = 0;

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

    protected override WeatherInfoPro GetChangedData()
    {
        return new WeatherInfoPro
        {
            Humidity = Humidity,
            Pressure = Pressure,
            Temperature = Temperature,
            Wind = new WindInfo()
            {
                speed = WindSpeed,
                direction = WindDirection
            }
        };
    }
}