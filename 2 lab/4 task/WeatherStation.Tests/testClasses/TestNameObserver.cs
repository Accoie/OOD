using WeatherStation.Info;
using WeatherStation.Observable;
using WeatherStation.Observer;

namespace WeatherStation.Tests.testClasses;

public class TestNameObserver : IObserverType<WeatherInfo>
{
    public IObservableType<WeatherInfo>? Observable;

    public void Update( WeatherInfo data, IObservableType<WeatherInfo> observable )
    {
        Observable = observable;
    }
}