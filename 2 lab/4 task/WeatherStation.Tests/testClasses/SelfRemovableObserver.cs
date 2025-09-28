using WeatherStation.Info;
using WeatherStation.Observable;
using WeatherStation.Observer;

namespace WeatherStation.Tests.testClasses;

public class SelfRemovableObserver : IObserverType<WeatherInfo>
{
    public int CountUpdate { get; private set; } = 0;

    public void Update( WeatherInfo data, IObservableType<WeatherInfo> observable )
    {
        observable.RemoveObserver( this );
        CountUpdate++;
    }
}
