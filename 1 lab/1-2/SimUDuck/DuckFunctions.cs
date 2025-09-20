using SimUDuck.Ducks;

namespace SimUDuck
{
    public static class DuckFunctions
    {
        public static void DrawDuck( Duck duck )
        {
            duck.Display();
        }

        public static void PlayWithDuck( Duck duck )
        {
            DrawDuck( duck );
            duck.Quak();
            duck.Fly();
            duck.Dance();
            Console.WriteLine();
        }
    }
}