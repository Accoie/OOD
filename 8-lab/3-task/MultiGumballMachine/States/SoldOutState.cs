using MultiGumballMachineApp.GumballMachines;

namespace MultiGumballMachineApp.States
{
    public class SoldOutState : IState
    {
        private readonly IGumballMachine _gumballMachine;

        public SoldOutState( IGumballMachine gumballMachine )
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
            else if ( _gumballMachine.GetQuartersCount() > 0 )
            {
                _gumballMachine.SetHasQuarterState();
            }
            else
            {
                _gumballMachine.SetNoQuarterState();
            }

            Console.WriteLine( $"Gumballs was changed on {count}" );
        }

        public void InsertQuarter()
        {
            Console.WriteLine( "You can't insert a quarter, the machine is sold out" );
        }

        public void EjectQuarter()
        {
            if ( _gumballMachine.GetQuartersCount() != 0 )
            {
                Console.WriteLine( "Quarters ejected" );
            }
            else
            {
                Console.WriteLine( "You can't eject, you haven't inserted a quarter yet" );
            }
        }

        public void TurnCrank()
        {
            Console.WriteLine( "You turned but there're no gumballs" );
        }

        public void Dispense()
        {
            Console.WriteLine( "No gumball dispensed" );
        }

        public override string ToString()
        {
            return "sold out";
        }
    }
}