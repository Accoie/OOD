using WeatherStation.Observable;

namespace WeatherStation.Tests;

public class WeatherStationTests
{
    [Test]
    public void NotifyObservers_RemoveDuringUpdate_NoThrow()
    {
        var weatherData = new WeatherData();

        var observer = new SelfRemovableObserver( weatherData );
        weatherData.RegisterObserver( observer );
        Assert.DoesNotThrow( () => weatherData.SetMeasurements( 3, 1.2, 760 ) );
    }

    [Test]
    public void NotifyObservers_RemoveDuringUpdate_WillRemoved()
    {
        var weatherData = new WeatherData();

        var observer = new SelfRemovableObserver( weatherData );
        weatherData.RegisterObserver( observer );
        weatherData.SetMeasurements( 3, 1.2, 760 );
        weatherData.SetMeasurements( 4, 1.7, 765 );

        Assert.That( observer.CountUpdate, Is.EqualTo( 1 ) );
    }
}