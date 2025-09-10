using SimUDuck.DucksActions.Dance;
using SimUDuck.DucksActions.Fly;
using SimUDuck.DucksActions.Quack;

namespace SimUDuck.Ducks;

public class Duck : IDuck
{
    private int _flyCount = 0;

    private IFlyBehavior _flyBehavior;
    private readonly IQuackBehavior _quackBehavior;
    private readonly IDanceBehavior _danceBehavior;
    public bool IsQuackOnEvenFlight { get; private set; } = false;

    public Duck( IFlyBehavior flyBehavior, IQuackBehavior quakBehavior, IDanceBehavior danceBehavior )
    {
        _flyBehavior = flyBehavior;
        _quackBehavior = quakBehavior;
        _danceBehavior = danceBehavior;
    }

    public virtual void Dance()
    {
        _danceBehavior.Dance();
    }

    public virtual void Display()
    {
    }

    public void Quak()
    {
        _quackBehavior.Quack();
    }

    public void Swim()
    {
        Console.WriteLine( "I am swimming!" );
    }

    public void Fly()
    {
        if ( _flyBehavior.GetType() != typeof( FlyNoWay ) )
        {
            OnFly();
        }
        _flyBehavior.Fly();
    }

    public void SetFlyBehavior( IFlyBehavior flyBehavior )
    {
        _flyBehavior = flyBehavior;
    }

    private void OnFly()
    {
        int quackModulo = IsQuackOnEvenFlight ? 1 : 0;

        if ( ++_flyCount % 2 == quackModulo )
        {
            Quak();
        }

        Console.WriteLine( $"This'll be my {_flyCount} flight!" );
    }
}