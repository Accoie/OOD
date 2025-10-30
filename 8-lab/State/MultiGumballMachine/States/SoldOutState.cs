using GumballMachineApp.GumballMachines;

namespace GumballMachineApp.States
{
    public class SoldOutState : IState
    {
        private readonly IGumballMachine _gumballMachine;

        public SoldOutState( IGumballMachine gumballMachine )
        {
            _gumballMachine = gumballMachine;
        }

        public void InsertQuarter()
        {
            Console.WriteLine( "You can't insert a quarter, the machine is sold out" );
        }

        public void EjectQuarter()
        {
            if ( _gumballMachine.GetQuarters() != 0 )
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
            Console.WriteLine( "You turned but there's no gumballs" );
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