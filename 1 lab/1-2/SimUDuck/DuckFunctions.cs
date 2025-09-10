using SimUDuck.Ducks;

namespace SimUDuck
{
    public static class DuckFunctions
    {
        public static void DrawDuck( IDuck duck )
        {
            duck.Display();
        }

        public static void PlayWithDuck( IDuck duck )
        {
            DrawDuck( duck );
            duck.Quak();
            duck.Fly();
            duck.Dance();
            Console.WriteLine();
        }
    }
}