using WeatherStation.Observer;

namespace WeatherStation.Observable;

public interface IObservableType<T>
{
    void RegisterObserver( IObserverType<T> observer, string[] events, int priority );
    void NotifyObservers( string[] events );
    void RemoveObserver( IObserverType<T> observer, string[] events );
}