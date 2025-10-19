using SimUDuck.DucksActions;

namespace SimUDuck.Ducks
{
    public class RubberDuck : Duck
    {
        public RubberDuck() : base( FlyBehavior.FlyNoWay(), QuackBehaviour.Squeek(), DanceBehavior.NoDance() )
        {
        }

        public override void Display()
        {
            Console.WriteLine( "I'm rubber duck!" );
        }

        public override void Dance()
        {
        }
    }
}