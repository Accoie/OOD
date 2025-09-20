using SimUDuck.DucksActions.Dance;
using SimUDuck.DucksActions.Fly;
using SimUDuck.DucksActions.Quack;

namespace SimUDuck.Ducks;

public abstract class Duck
{
    private IFlyBehavior _flyBehavior;
    private readonly IQuackBehavior _quackBehavior;
    private readonly IDanceBehavior _danceBehavior;

    public Duck( IFlyBehavior flyBehavior, IQuackBehavior quakBehavior, IDanceBehavior danceBehavior )
    {
        _flyBehavior = flyBehavior;
        _quackBehavior = quakBehavior;
        _danceBehavior = danceBehavior;
    }

    public abstract void Display();

    public virtual void Dance()
    {
        _danceBehavior.Dance();
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
        int flyCount = _flyBehavior.FlyCount;

        if ( flyCount % 2 == 0 && flyCount != 0 )
        {
            Quak();
        }

        Console.WriteLine( $"This'll be my {++flyCount} flight!" );
    }
}