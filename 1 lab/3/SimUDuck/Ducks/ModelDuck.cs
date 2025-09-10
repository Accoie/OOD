using SimUDuck.DucksActions;

namespace SimUDuck.Ducks
{
    public class ModelDuck : Duck
    {
        public ModelDuck() : base( FlyBehavior.FlyNoWay(), QuackBehaviour.Quack(), DanceBehavior.NoDance() )
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