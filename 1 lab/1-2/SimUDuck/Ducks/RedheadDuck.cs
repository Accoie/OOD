using SimUDuck.DucksActions.Dance;
using SimUDuck.DucksActions.Fly;
using SimUDuck.DucksActions.Quack;

namespace SimUDuck.Ducks
{
    public class RedheadDuck : Duck
    {
        public RedheadDuck()
            : base( new FlyWithWings(), new QuackBehavior(), new Minuet() )
        {
        }

        public override void Display()
        {
            Console.WriteLine( "I'm redhead duck!" );
        }
    }
}