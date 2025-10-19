using WeatherStation.Observer;

namespace WeatherStation.Observable;

public class ObserverElement<T>
{
    public ObserverElement( IObserverType<T> observer, HashSet<string> events )
    {
        Observer = observer;
        Events = events;
    }

    public IObserverType<T> Observer { get; init; }
    public HashSet<string> Events { get; set; }
}

public abstract class Observable<T> : IObservableType<T>
{
    private readonly SortedDictionary<int, HashSet<ObserverElement<T>>> _observers = new();

    protected abstract T GetChangedData();

    public void RegisterObserver( IObserverType<T> observer, string[] events, int priority )
    {
        var eventsSet = new HashSet<string>( events );
        var observerElement = FindObserverElement( observer );

        if ( observerElement is not null )
        {
            foreach ( var eventName in eventsSet )
            {
                observerElement.Events.Add( eventName );
            }

            return;
        }

        observerElement = new ObserverElement<T>( observer, eventsSet );

        if ( !_observers.TryGetValue( priority, out var observersWithPriority ) )
        {
            observersWithPriority = new HashSet<ObserverElement<T>>();
            _observers[ priority ] = observersWithPriority;
        }

        observersWithPriority.Add( observerElement );
    }

    public void NotifyObservers( string[] events ) // сделать чтобы было быстро
    {
        T data = GetChangedData();
        var tempObservers = new SortedDictionary<int, HashSet<ObserverElement<T>>>( _observers );

        foreach ( var pair in tempObservers )
        {
            foreach ( var observerElement in pair.Value )
            {
                if ( observerElement.Events.Overlaps( events ) )
                {
                    observerElement.Observer.Update( data );
                }
            }
        }
    }

    public void NotifyObservers()
    {
        T data = GetChangedData();
        var tempObservers = new SortedDictionary<int, HashSet<ObserverElement<T>>>( _observers );

        foreach ( var pair in tempObservers )
        {
            foreach ( var observerElement in pair.Value )
            {
                observerElement.Observer.Update( data );
            }
        }
    }

    public void RemoveObserver( IObserverType<T> observer, string[] events )
    {
        foreach ( var eventName in events )
        {
            RemoveObserver( observer, eventName );
        }
    }

    public void RemoveObserver( IObserverType<T> observer )
    {
        foreach ( var pair in _observers )
        {
            var observerElement = FindObserverElementInSet( pair.Value, observer );

            if ( observerElement is null )
            {
                continue;
            }

            pair.Value.Remove( observerElement );

            if ( pair.Value.Count == 0 )
            {
                _observers.Remove( pair.Key );
            }

            break;
        }
    }

    public void RemoveObserver( IObserverType<T> observer, string eventName )
    {
        foreach ( var pair in _observers )
        {
            var observerElement = FindObserverElementInSet( pair.Value, observer );
            if ( observerElement == null || !observerElement.Events.Contains( eventName ) )
            {
                continue;
            }

            observerElement.Events.Remove( eventName );
            if ( observerElement.Events.Count > 0 )
            {
                break;
            }

            pair.Value.Remove( observerElement );
            if ( pair.Value.Count > 0 )
            {
                break;
            }

            _observers.Remove( pair.Key );

            break;
        }
    }

    public bool IsObserverSubscribedToEvent( IObserverType<T> observer, string eventName )
    {
        var observerElement = FindObserverElement( observer );

        return observerElement?.Events.Contains( eventName ) == true;
    }

    public IEnumerable<string> GetObserverEvents( IObserverType<T> observer )
    {
        var observerElement = FindObserverElement( observer );

        return observerElement?.Events ?? Enumerable.Empty<string>();
    }

    private ObserverElement<T>? FindObserverElement( IObserverType<T> observer )
    {
        foreach ( var pair in _observers )
        {
            var observerElement = FindObserverElementInSet( pair.Value, observer );

            if ( observerElement is not null )
            {
                return observerElement;
            }
        }

        return null;
    }

    private ObserverElement<T>? FindObserverElementInSet( HashSet<ObserverElement<T>> set, IObserverType<T> observer )
    {
        return set.FirstOrDefault( element => element.Observer?.Equals( observer ) == true );
    }
}