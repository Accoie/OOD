using MultiGumballMachineApp.GumballMachines;

namespace MultiGumballMachineApp.States
{
    public class HasQuarterState : IState
    {
        private readonly IGumballMachine _gumballMachine;

        public HasQuarterState( IGumballMachine gumballMachine )
        {
            _gumballMachine = gumballMachine;
        }

        public void InsertQuarter()
        {
            if ( _gumballMachine.GetGumballsCount() == _gumballMachine.GetQuartersLimit() )
            {
                Console.WriteLine( "You can't insert another one quarter" );
            }
            else
            {
                Console.WriteLine( "You inserted another one quarter" );
                _gumballMachine.AddQuarter();
            }
        }

        public void EjectQuarter()
        {
            Console.WriteLine( "Quarters ejected successfully" );
            _gumballMachine.ResetQuarters();
            _gumballMachine.SetNoQuarterState();
        }

        public void TurnCrank()
        {
            Console.WriteLine( "You turned the crank" );
            _gumballMachine.DeleteQuarter();
            _gumballMachine.SetSoldState();
        }

        public void Dispense()
        {
            Console.WriteLine( "You need to insert a quarter first" );
        }

        public override string ToString()
        {
            return "waiting for turning the crank";
        }
    }
}