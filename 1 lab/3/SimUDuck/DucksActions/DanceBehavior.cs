namespace SimUDuck.DucksActions
{
    public static class DanceBehavior
    {
        public static Action Minuet()
        {
            return () => Console.WriteLine( "Waltz" );
        }

        public static Action Waltz()
        {
            return () => Console.WriteLine( "Waltz" );
        }

        public static Action NoDance()
        {
            return () => { };
        }
    }
}