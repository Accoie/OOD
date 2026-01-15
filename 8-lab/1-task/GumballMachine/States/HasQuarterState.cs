using GumballMachineApp.GumballMachines;

namespace GumballMachineApp.States
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
            Console.WriteLine( "You can't insert another one quarter" );
        }

        public void EjectQuarter()
        {
            Console.WriteLine( "Quarter ejected successfully" );
            _gumballMachine.SetNoQuarterState();
        }

        public void TurnCrank()
        {
            Console.WriteLine( "You turned the crank" );
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