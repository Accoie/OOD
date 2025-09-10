namespace SimUDuck.Ducks
{
    public interface IDuck
    {
        bool IsQuackOnEvenFlight { get; }

        void Dance();
        void Display();
        void Fly();
        void Quak();
        void SetFlyBehavior( (Func<int>, bool) flyBehavior );
        void Swim();
    }
}