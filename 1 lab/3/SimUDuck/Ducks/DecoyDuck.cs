using SimUDuck.DucksActions;

namespace SimUDuck.Ducks
{
    public class DecoyDuck : Duck
    {
        public DecoyDuck() : base( FlyBehavior.FlyNoWay(), QuackBehaviour.MuteQuack(), DanceBehavior.NoDance() )
        {
        }

        public override void Display()
        {
            Console.WriteLine( "I'm decoy duck!" );
        }
    }
}
