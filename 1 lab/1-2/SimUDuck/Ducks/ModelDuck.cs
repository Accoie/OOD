using SimUDuck.DucksActions.Dance;
using SimUDuck.DucksActions.Fly;
using SimUDuck.DucksActions.Quack;

namespace SimUDuck.Ducks
{
    public class ModelDuck : Duck
    {
        public ModelDuck() : base( new FlyNoWay(), new QuackBehavior(), new NoDance() )
        {
        }

        public override void Display()
        {
            Console.WriteLine( "I'm model duck!" );
        }

        public override void Dance()
        {
        }
    }
}