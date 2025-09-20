using SimUDuck.DucksActions;

namespace SimUDuck.Ducks
{
    public class RedheadDuck : Duck
    {
        public RedheadDuck()
            : base( FlyBehavior.FlyWithWings(), QuackBehaviour.Quack(), DanceBehavior.Minuet() )
        {
        }

        public override void Display()
        {
            Console.WriteLine( "I'm redhead duck!" );
        }
    }
}