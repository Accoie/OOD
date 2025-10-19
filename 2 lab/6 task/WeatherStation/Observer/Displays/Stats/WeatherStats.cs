using WeatherStation.Info;

namespace WeatherStation.Observer.Displays.Stats;

public class WeatherStats
{
    private double _min = double.MaxValue;
    private double _max = double.MinValue;
    private double _accumulated = 0;
    private double _accumulatedCount = 0;

    public WeatherStatsInfo GetStats()
    {
        return new WeatherStatsInfo
        {
            max = _max,
            min = _min,
            average = CalculateAverage()
        };
    }

    public void Update( double newValue )
    {
        if ( newValue < _min )
        {
            _min = newValue;
        }

        if ( newValue > _max )
        {
            _max = newValue;
        }

        _accumulated += newValue;
        _accumulatedCount++;
    }

    private double CalculateAverage()
    {
        if ( _accumulatedCount == 0 )
        {
            return 0;
        }

        return _accumulated / _accumulatedCount;
    }
}