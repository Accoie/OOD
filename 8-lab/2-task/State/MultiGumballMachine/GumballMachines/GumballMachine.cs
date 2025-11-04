using MultiGumballMachineApp.States;

namespace MultiGumballMachineApp.GumballMachines
{
    public class GumballMachine : IGumballMachine, IGumballMachineClient
    {
        private const int _quartersLimit = 5;
        private int _quarters;
        private int _gumballsCount;
        private IState _currentState;

        public GumballMachine( int numBalls )
        {
            _gumballsCount = numBalls;

            if ( numBalls < 0 )
            {
                throw new ArgumentException( "Balls count can not be negative number" );
            }

            _currentState = _gumballsCount > 0
                ? new NoQuarterState( this )
                : _currentState = new SoldOutState( this );
        }

        public void EjectQuarter()
        {
            _currentState.EjectQuarter();
        }

        public void AddQuarter()
        {
            if ( _quarters == _quartersLimit )
            {
                Console.WriteLine( $"Can't add quarter, bcs its too more.\n Limit is {_quartersLimit}" );
            }
            else
            {
                _quarters++;
            }
        }

        public void DeleteQuarter()
        {
            if ( _quarters == 0 )
            {
                Console.WriteLine( "There is no quarters" );
            }
            else
            {
                _quarters--;
            }
        }

        public void ResetQuarters()
        {
            _quarters = 0;
        }

        public void InsertQuarter()
        {
            _currentState.InsertQuarter();
        }

        public void TurnCrank()
        {
            _currentState.TurnCrank();
            _currentState.Dispense();
        }

        public override string ToString()
        {
            return $"Mighty Gumball, Inc.\nC#-enabled Standing Gumball Model #2025 (with state)\nInventory: {_gumballsCount} gumball{( _gumballsCount != 1 ? "s" : "" )}\n" + $"Quarters: {_quarters}" +
                $"Machine is {_currentState.ToString()}\n";
        }

        public void SetSoldOutState()
        {
            _currentState = new SoldOutState( this );
        }

        public void SetNoQuarterState()
        {
            _currentState = new NoQuarterState( this );
        }

        public void SetSoldState()
        {
            _currentState = new SoldState( this );
        }

        public void SetHasQuarterState()
        {
            _currentState = new HasQuarterState( this );
        }

        public void ReleaseBall()
        {
            if ( _gumballsCount > 0 )
            {
                Console.WriteLine( "A gumball comes rolling out the slot..." );
                _gumballsCount--;
            }
        }

        public int GetQuartersCount()
        {
            return _quarters;
        }

        public int GetQuartersLimit()
        {
            return _quartersLimit;
        }

        public int GetGumballsCount()
        {
            return _gumballsCount;
        }
    }
}
