using WeatherStation.Observer;

namespace WeatherStation.Observable;

public abstract class Observable<T> : IObservableType<T>
{
    private readonly SortedDictionary<int, HashSet<IObserverType<T>>> _observers = new();

    protected abstract T GetChangedData();

    public void RegisterObserver( IObserverType<T> observer, int priority )
    {
        if (ExistObserver( observer ))
        {
            return;
        }

        if ( !_observers.TryGetValue( priority, out var observersWithPriority ) )
        {
            observersWithPriority = new HashSet<IObserverType<T>>();

            _observers[ priority ] = observersWithPriority;
        }

        observersWithPriority.Add( observer );
    }

    public void NotifyObservers()
    {
        T data = GetChangedData();
        
        var tempObservers = new SortedDictionary<int, HashSet<IObserverType<T>>>(_observers);

        foreach ( var pair in tempObservers )
        {
            foreach ( var observer in pair.Value )
            {
                observer.Update( data );
            }
        }
    }

    public void RemoveObserver( IObserverType<T> observer )
    {
        // сделать лучше чем за линейное время
        foreach ( var pair in _observers )
        {
            if ( !pair.Value.Remove( observer ) )
            {
                continue;
            }
            if ( pair.Value.Count == 0 )
            {
                _observers.Remove( pair.Key );

                break;
            }
        }
    }

    private bool ExistObserver( IObserverType<T> observer )
    {
        foreach ( var pair in _observers )
        {
            if ( pair.Value.Contains( observer ) )
            {
                return true;
            }
        }

        return false;
    }
}