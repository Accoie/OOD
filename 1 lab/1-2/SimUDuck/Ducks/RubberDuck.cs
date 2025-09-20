using SimUDuck.DucksActions.Dance;
using SimUDuck.DucksActions.Fly;
using SimUDuck.DucksActions.Quack;

namespace SimUDuck.Ducks
{
    public class RubberDuck : Duck
    {
        public RubberDuck() : base( new FlyNoWay(), new SqueekBehavior(), new NoDance() )
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
