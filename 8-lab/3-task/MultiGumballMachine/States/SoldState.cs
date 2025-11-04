using MultiGumballMachineApp.GumballMachines;

namespace MultiGumballMachineApp.States
{
    public class SoldState : IState
    {
        private readonly IGumballMachine _gumballMachine;

        public SoldState( IGumballMachine gumballMachine )
        {
            _gumballMachine = gumballMachine;
        }

        public void RefillGumballs( int count )
        {
            return;
        }

        public void InsertQuarter()
        {
            Console.WriteLine( "Please wait, we're already giving you a gumball" );
        }

        public void EjectQuarter()
        {
            Console.WriteLine( "Sorry, you already turned the crank" );
        }

        public void TurnCrank()
        {
            Console.WriteLine( "Turning twice doesn't get you another gumball" );
        }

        public void Dispense()
        {
            _gumballMachine.ReleaseBall();
            if ( _gumballMachine.GetGumballsCount() == 0 )
            {
                Console.WriteLine( "Oops, out of gumballs" );
                _gumballMachine.SetSoldOutState();
            }
            else if ( _gumballMachine.GetQuartersCount() != 0 )
            {
                _gumballMachine.SetHasQuarterState();
            }
            else
            {
                _gumballMachine.SetNoQuarterState();
            }
        }

        public override string ToString()
        {
            return "delivering a gumball";
        }
    }
}