using WeatherStation.Observable;

namespace WeatherStation.Observer;

public interface IObserverType<T>
{
    void Update( T data, IObservableType<T> observable );
}