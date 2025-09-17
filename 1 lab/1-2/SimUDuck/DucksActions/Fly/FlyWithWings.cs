namespace SimUDuck.DucksActions.Fly
{
    public class FlyWithWings : IFlyBehavior
    {
        public int FlyCount { get; private set; }

        public void Fly()
        {
            Console.WriteLine( "I'm flying with wings!!" );

            FlyCount++;
        }
    }
}