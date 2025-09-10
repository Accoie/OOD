using SimUDuck.DucksActions.Fly;

namespace SimUDuck.Ducks
{
    public interface IDuck
    {
        void Dance();
        void Display();
        void Fly();
        void Quak();
        void SetFlyBehavior( IFlyBehavior flyBehavior );
        void Swim();
    }
}