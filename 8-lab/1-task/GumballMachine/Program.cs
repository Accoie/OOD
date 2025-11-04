using GumballMachineApp.GumballMachines;

namespace GumballMachineApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine( "Gumball Machine" );
            Console.WriteLine();

            IGumballMachineClient machine = GumballMachineFactory.CreateGumballMachine( 5 );
            Console.WriteLine( machine );

            machine.InsertQuarter();
            machine.TurnCrank();
            Console.WriteLine( machine );

            machine.InsertQuarter();
            machine.EjectQuarter();
            Console.WriteLine( machine );

            machine.InsertQuarter();
            machine.TurnCrank();
            Console.WriteLine( machine );

            machine.InsertQuarter();
            machine.TurnCrank();
            Console.WriteLine( machine );

            machine.InsertQuarter();
            machine.TurnCrank();
            Console.WriteLine( machine );

            machine.InsertQuarter();
            machine.TurnCrank();
            Console.WriteLine( machine );
        }
    }
}