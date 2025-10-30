namespace GumballMachineApp
{
    class Program
    {
        static void Main( string[] args )
        {
            Console.WriteLine( "Gumball Machine" );
            Console.WriteLine();

            GumballMachines.GumballMachine machine = new GumballMachines.GumballMachine( 5 );
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