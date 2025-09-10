using SimUDuck.DucksActions.Dance;
using SimUDuck.DucksActions.Fly;
using SimUDuck.DucksActions.Quack;

namespace SimUDuck.Ducks
{
    public interface IDuck
    {
        bool IsQuackOnEvenFlight { get; }

        void Dance();
        void Display();
        void Fly();
        void Quak();
        void SetFlyBehavior( IFlyBehavior flyBehavior );
        void Swim();
    }
}