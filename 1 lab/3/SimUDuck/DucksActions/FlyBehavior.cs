namespace SimUDuck.DucksActions
{
    public static class FlyBehavior
    {
        public static (Func<int>, bool) FlyWithWings()
        {
            int flyCount = 0;

            Func<int> action = () =>
            {
                ++flyCount;
                Console.WriteLine( "I'm flying with wings!!" );

                return flyCount;
            };

            return (action, true);
        }

        public static (Func<int>, bool) FlyNoWay()
        {
            Func<int> action = () => { return 0; };

            return (action, false);
        }
    }
}