using MultiGumballMachineApp.GumballMachines;

namespace MultiGumballMachineApp.States
{
    public class NoQuarterState : IState
    {
        private readonly IGumballMachine _gumballMachine;

        public NoQuarterState( IGumballMachine gumballMachine )
        {
            _gumballMachine = gumballMachine;
        }

        public void RefillGumballs( int count )
        {
            _gumballMachine.SetGumballsCount( count );

            if ( count == 0 )
            {
                _gumballMachine.SetSoldOutState();
            }

            Console.WriteLine( $"Gumballs was changed on {count}" );
        }

        public void InsertQuarter()
        {
            Console.WriteLine( "You inserted a quarter" );
            _gumballMachine.AddQuarter();
            _gumballMachine.SetHasQuarterState();
        }

        public void EjectQuarter()
        {
            Console.WriteLine( "You haven't inserted a quarter" );
        }

        public void TurnCrank()
        {
            Console.WriteLine( "You turned but there're no quarters" );
        }

        public void Dispense()
        {
            Console.WriteLine( "You need to pay first" );
        }

        public override string ToString()
        {
            return "waiting for quarter";
        }
    }
}