namespace WeatherStation.Observer.Displays;

public class WindDirectionStats
{
    private double _sinSum = 0;
    private double _cosSum = 0;
    private double _average = 0;

    public void Update( double degrees )
    {
        _sinSum += Math.Sin( DegreesToRadians( degrees ) );
        _cosSum += Math.Cos( DegreesToRadians( degrees ) );
        double degree = RadiansToDegrees( Math.Atan2( _sinSum, _cosSum ) ) + 360;
        _average = degree % 360;
    }

    public double GetAverageData()
    {
        return _average;
    }

    private double DegreesToRadians( double degrees )
    {
        return degrees * ( Math.PI / 180 );
    }

    private double RadiansToDegrees( double radians )
    {
        return radians * ( 180 / Math.PI );
    }
}