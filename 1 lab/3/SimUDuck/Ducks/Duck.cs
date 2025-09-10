namespace SimUDuck.Ducks;

public class Duck : IDuck
{
    private Func<int> _flyBehavior;
    private readonly Action _quackBehavior;
    private readonly Action _danceBehavior;
    private bool _isCanFly;

    public bool IsQuackOnEvenFlight { get; private set; } = false;

    public Duck( (Func<int>, bool) flyBehavior, Action quakBehavior, Action danceBehavior )
    {
        _flyBehavior = flyBehavior.Item1;
        _isCanFly = flyBehavior.Item2;
        _quackBehavior = quakBehavior;
        _danceBehavior = danceBehavior;
    }

    public virtual void Dance()
    {
        _danceBehavior();
    }

    public virtual void Display()
    {
    }

    public void Quak()
    {
        _quackBehavior();
    }

    public void Swim()
    {
        Console.WriteLine( "I am swimming!" );
    }

    public void Fly()
    {
        if ( _isCanFly )
        {
            int flyCount = _flyBehavior();

            if ( flyCount % 2 == 0 )
            {
                Quak();
            }

            Console.WriteLine( $"This'll be my {flyCount} flight!" );
        }
    }

    public void SetFlyBehavior( (Func<int>, bool) flyBehavior )
    {
        _flyBehavior = flyBehavior.Item1;
        _isCanFly |= flyBehavior.Item2;
    }
}