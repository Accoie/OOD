namespace SimUDuck.DucksActions
{
    public static class QuackBehaviour
    {
        public static Action MuteQuack()
        {
            return () => { };
        }

        public static Action Quack()
        {
            return () => Console.WriteLine( "Quack quack!!" );
        }

        public static Action Squeek()
        {
            return () => Console.WriteLine( "Squeek!!" );
        }
    }
}