using WeatherStation.Observer;

namespace WeatherStation.Observable;

public abstract class Observable<T> : IObservableType<T>
{
    private HashSet<IObserverType<T>> _observers = new();

    public void RegisterObserver( IObserverType<T> observer )
    {
        _observers.Add( observer );
    }

    public void NotifyObservers()
    {
        T data = GetChangedData();

        var tempObservers = new HashSet<IObserverType<T>>( _observers );

        foreach ( var observer in tempObservers )
        {
            observer.Update( data );
        }
    }

    public void RemoveObserver( IObserverType<T> observer )
    {
        _observers.Remove( observer );
    }

    protected abstract T GetChangedData();
}