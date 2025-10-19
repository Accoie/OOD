using SimUDuck.DucksActions;

namespace SimUDuck.Ducks
{
    public class MallardDuck : Duck
    {
        public MallardDuck() : base( FlyBehavior.FlyWithWings(), QuackBehaviour.Quack(), DanceBehavior.Waltz() )
        {
        }

        public override void Display()
        {
            Console.WriteLine( "I'm mallard duck!" );
        }
    }
}