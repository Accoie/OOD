using SimUDuck;
using SimUDuck.Ducks;
using SimUDuck.DucksActions;

public class Program
{
    private static void Main( string[] args )
    {
        MallardDuck mallardDuck = new();
        DuckFunctions.PlayWithDuck( mallardDuck );

        RedheadDuck redheadDuck = new();
        DuckFunctions.PlayWithDuck( redheadDuck );

        RubberDuck rubberDuck = new();
        DuckFunctions.PlayWithDuck( rubberDuck );

        DecoyDuck decoyDuck = new();
        DuckFunctions.PlayWithDuck( decoyDuck );

        ModelDuck modelDuck = new();
        DuckFunctions.PlayWithDuck( modelDuck );

        modelDuck.SetFlyBehavior( FlyBehavior.FlyWithWings() );
        DuckFunctions.PlayWithDuck( modelDuck );
    }
}