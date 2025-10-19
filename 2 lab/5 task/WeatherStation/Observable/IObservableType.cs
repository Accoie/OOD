using WeatherStation.Observer;

namespace WeatherStation.Observable;

public interface IObservableType<T>
{
    void RegisterObserver( IObserverType<T> observer, int priority );
    void NotifyObservers();
    void RemoveObserver( IObserverType<T> observer );
}