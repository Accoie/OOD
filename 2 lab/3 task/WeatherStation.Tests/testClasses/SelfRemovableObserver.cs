using WeatherStation.Info;
using WeatherStation.Observable;
using WeatherStation.Observer;

namespace WeatherStation.Tests.testClasses;

public class SelfRemovableObserver : IObserverType<WeatherInfo>
{
    private IObservableType<WeatherInfo> _observer;

    public int CountUpdate { get; private set; } = 0;

    public SelfRemovableObserver( IObservableType<WeatherInfo> observable )
    {
        _observer = observable;
    }

    public void Update( WeatherInfo data )
    {
        _observer.RemoveObserver( this );
        CountUpdate++;
    }
}
