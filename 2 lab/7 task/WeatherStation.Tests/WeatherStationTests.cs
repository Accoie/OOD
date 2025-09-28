using Moq;
using WeatherStation.Events;
using WeatherStation.Info;
using WeatherStation.Observable;
using WeatherStation.Observer;
using WeatherStation.Tests.testClasses;

namespace WeatherStation.Tests;

public class WeatherStationTests
{
    [Test]
    public void NotifyObservers_RemoveDuringUpdate_NoThrow()
    {
        var weatherData = new WeatherData( );

        var observer = new SelfRemovableObserver( weatherData );
        weatherData.RegisterObserver( observer, [WeatherDataEvents.Pressure], 1 );
        Assert.DoesNotThrow( () => weatherData.SetMeasurements(3, 1.2, 760 ) );
    }

    [Test]
    public void NotifyObservers_RemoveDuringUpdate_WillRemoved()
    {
        var weatherData = new WeatherData();

        var observer = new SelfRemovableObserver( weatherData );
        weatherData.RegisterObserver( observer, [ WeatherDataEvents.Temperature ], 1 );
        weatherData.SetMeasurements(3, 1.2, 760 );
        weatherData.SetMeasurements(4, 1.7, 765 );

        Assert.That( observer.CountUpdate, Is.EqualTo( 1 ) );
    }
}