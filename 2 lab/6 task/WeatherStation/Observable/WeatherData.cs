using WeatherStation.Info;

namespace WeatherStation.Observable;

public class WeatherData : Observable<WeatherInfo>
{
    public double Temperature { get; protected set; } = 0;
    public double Humidity { get; protected set; } = 0;
    public double Pressure { get; protected set; } = 760;

    public void MeasurementsChanged()
    {
        NotifyObservers();
    }

    public void SetMeasurements( double temperature, double humidity, double pressure )
    {
        Temperature = temperature;
        Humidity = humidity;
        Pressure = pressure;

        MeasurementsChanged();
    }

    protected override WeatherInfo GetChangedData()
    {
        return new WeatherInfo
        {
            Humidity = Humidity,
            Pressure = Pressure,
            Temperature = Temperature,
        };
    }
}