using WeatherStation.Info;

namespace WeatherStation.Observable;

public class WeatherDataIn : Observable<WeatherInfo>
{
    public double Temperature { get; private set; } = 0;
    public double Humidity { get; private set; } = 0;
    public double Pressure { get; private set; } = 760;

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
            humidity = Humidity,
            pressure = Pressure,
            temperature = Temperature
        };
    }
}