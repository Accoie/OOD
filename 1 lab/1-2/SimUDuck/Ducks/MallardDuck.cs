using SimUDuck.DucksActions.Dance;
using SimUDuck.DucksActions.Fly;
using SimUDuck.DucksActions.Quack;

namespace SimUDuck.Ducks
{
    public class MallardDuck : Duck
    {
        public MallardDuck() : base( new FlyWithWings(), new QuackBehavior(), new Waltz() )
        {
        }

        public override void Display()
        {
            Console.WriteLine( "I'm mallard duck!" );
        }
    }
}