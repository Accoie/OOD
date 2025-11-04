namespace MultiGumballMachineApp.GumballMachines
{
    public class GumballMachine : IGumballMachineClient
    {
        private const int _quartersLimit = 5;
        private int _quarters;
        private int _gumballsCount;
        private State _currentState;

        public GumballMachine( int numBalls )
        {
            _gumballsCount = numBalls;

            if ( numBalls < 0 )
            {
                throw new ArgumentException( "Balls count can not be negative number" );
            }

            _currentState = _gumballsCount > 0
                ? State.NoQuarter
                : _currentState = State.SoldOut;
        }

        public void EjectQuarter()
        {
            switch ( _currentState )
            {
                case State.NoQuarter:
                    Console.WriteLine( "You haven't inserted a quarter" );
                    break;
                case State.HasQuarter:
                    Console.WriteLine( "Quarters ejected successfully" );
                    ResetQuarters();
                    SetNoQuarterState();
                    break;
                case State.Sold:
                    Console.WriteLine( "Sorry, you already turned the crank" );
                    break;
                case State.SoldOut:
                    if ( GetQuartersCount() != 0 )
                    {
                        Console.WriteLine( "Quarters ejected" );
                    }
                    else
                    {
                        Console.WriteLine( "You can't eject, you haven't inserted a quarter yet" );
                    }
                    break;
                default:
                    break;
            }
        }

        private void AddQuarter()
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

        private void DeleteQuarter()
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

        private void ResetQuarters()
        {
            _quarters = 0;
        }

        public void InsertQuarter()
        {
            switch ( _currentState )
            {
                case State.NoQuarter:
                    Console.WriteLine( "You inserted a quarter" );
                    AddQuarter();
                    SetHasQuarterState();
                    break;
                case State.HasQuarter:
                    if ( GetGumballsCount() == GetQuartersLimit() )
                    {
                        Console.WriteLine( "You can't insert another one quarter" );
                    }
                    else
                    {
                        Console.WriteLine( "You inserted another one quarter" );
                        AddQuarter();
                    }
                    break;
                case State.Sold:
                    Console.WriteLine( "Please wait, we're already giving you a gumball" );
                    break;
                case State.SoldOut:
                    Console.WriteLine( "You can't insert a quarter, the machine is sold out" );
                    break;
                default:
                    break;
            }
        }

        public void TurnCrank()
        {
            switch ( _currentState )
            {
                case State.NoQuarter:
                    Console.WriteLine( "You turned but there're no quarters" );
                    Dispence();
                    break;
                case State.HasQuarter:
                    Console.WriteLine( "You turned the crank" );
                    DeleteQuarter();
                    SetSoldState();
                    Dispence();
                    break;
                case State.Sold:
                    Console.WriteLine( "Turning twice doesn't get you another gumball" );
                    Dispence();
                    break;
                case State.SoldOut:
                    Console.WriteLine( "You turned but there're no gumballs" );
                    Dispence();
                    break;
                default:
                    break;
            }
        }

        public void Dispence()
        {
            switch ( _currentState )
            {
                case State.NoQuarter:
                    Console.WriteLine( "You need to pay first" );
                    break;
                case State.HasQuarter:
                    Console.WriteLine( "You need to insert a quarter first" );
                    break;
                case State.Sold:
                    ReleaseBall();
                    if ( GetGumballsCount() == 0 )
                    {
                        Console.WriteLine( "Oops, out of gumballs" );
                        SetSoldOutState();
                    }
                    else if ( GetQuartersCount() != 0 )
                    {
                        SetHasQuarterState();
                    }
                    else
                    {
                        SetNoQuarterState();
                    }
                    break;
                case State.SoldOut:
                    Console.WriteLine( "No gumball dispensed" );
                    break;
                default:
                    break;
            }
        }

        public override string ToString()
        {
            return $"Mighty Gumball, Inc.\nC#-enabled Standing Gumball Model #2025 (with state)\nInventory: {_gumballsCount} gumball{( _gumballsCount != 1 ? "s" : "" )}\n" + $"Quarters: {_quarters}" +
                $"Machine is {StateToString()}\n";
        }

        private void SetSoldOutState()
        {
            _currentState = State.SoldOut;
        }

        private void SetNoQuarterState()
        {
            _currentState = State.NoQuarter;
        }

        private void SetSoldState()
        {
            _currentState = State.Sold;
        }

        private void SetHasQuarterState()
        {
            _currentState = State.HasQuarter;
        }

        private void ReleaseBall()
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

        private string StateToString()
        {
            switch ( _currentState )
            {
                case State.NoQuarter:
                    return "no quarter";
                case State.HasQuarter:
                    return "has quarter";
                case State.Sold:
                    return "sold";
                case State.SoldOut:
                    return "sold out";
                default:
                    return "error";
            }
        }
    }
}
