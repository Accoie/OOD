using SimUDuck.DucksActions.Dance;
using SimUDuck.DucksActions.Fly;
using SimUDuck.DucksActions.Quack;

namespace SimUDuck.Ducks
{
    public class DecoyDuck : Duck
    {
        public DecoyDuck() : base( new FlyWithWings(), new MuteQuackBehavior(), new NoDance() )
        {
        }

        public override void Display()
        {
            Console.WriteLine( "I'm decoy duck!" );
        }
    }
}
