namespace SimUDuck.DucksActions.Quack
{
    public class SqueekBehavior : IQuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine( "Squeek!!!" );
        }
    }
}