using Moq;
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
        var weatherData = new WeatherDataIn();

        var observer = new SelfRemovableObserver();
        weatherData.RegisterObserver( observer, 1 );
        Assert.DoesNotThrow( () => weatherData.SetMeasurements( 3, 1.2, 760 ) );
    }

    [Test]
    public void NotifyObservers_RemoveDuringUpdate_WillRemoved()
    {
        var weatherData = new WeatherDataIn();

        var observer = new SelfRemovableObserver();
        weatherData.RegisterObserver( observer, 1 );
        weatherData.SetMeasurements( 3, 1.2, 760 );
        weatherData.SetMeasurements( 4, 1.7, 765 );

        Assert.That( observer.CountUpdate, Is.EqualTo( 1 ) );
    }

    public void NotifyObservers_CallsUpdate_InPriorityOrder()
    {
        var weatherData = new WeatherDataIn();

        var highMock = new Mock<IObserverType<WeatherInfo>>();
        var mediumMock = new Mock<IObserverType<WeatherInfo>>();
        var lowMock = new Mock<IObserverType<WeatherInfo>>();

        var callOrder = new List<string>();

        highMock.Setup( x => x.Update( It.IsAny<WeatherInfo>(), weatherData ) ).Callback( () => callOrder.Add( "high" ) );
        mediumMock.Setup( x => x.Update( It.IsAny<WeatherInfo>(), weatherData ) ).Callback( () => callOrder.Add( "medium" ) );
        lowMock.Setup( x => x.Update( It.IsAny<WeatherInfo>() , weatherData )).Callback( () => callOrder.Add( "low" ) );

        weatherData.RegisterObserver( highMock.Object, 100 );
        weatherData.RegisterObserver( mediumMock.Object, 50 );
        weatherData.RegisterObserver( lowMock.Object, 1 );

        weatherData.SetMeasurements( 25, 0.6, 755 );

        Assert.Equals( new[] { "high", "medium", "low" }, callOrder );
    }

    [Test]
    public void RegisterObserver_PreventsDuplicateRegistration()
    {
        var weatherData = new WeatherDataIn();
        var observerMock = new Mock<IObserverType<WeatherInfo>>();

        weatherData.RegisterObserver( observerMock.Object, 50 );
        weatherData.RegisterObserver( observerMock.Object, 100 );

        weatherData.SetMeasurements( 22, 0.7, 758 );

        observerMock.Verify( x => x.Update( It.IsAny<WeatherInfo>(), weatherData ), Times.Once );
    }

    [Test]
    public void NotifyObservers_ReceiveNameFromStationIn_WillReceived()
    {
        var weatherDataIn = new WeatherDataIn();
        var weatherDataOut = new WeatherDataOut();
        var name = string.Empty;

        var observer = new TestNameObserver();

        weatherDataIn.RegisterObserver( observer, 50 );

        weatherDataIn.SetMeasurements( 23, 0.5, 788 );

        Assert.That( observer.Observable?.GetType() == typeof( WeatherDataIn ) );
    }
}