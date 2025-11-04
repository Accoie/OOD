namespace MultiGumballMachineApp.GumballMachines
{
    public interface IGumballMachine
    {
        void ReleaseBall();
        void SetGumballsCount( int count );
        void SetSoldOutState();
        void SetNoQuarterState();
        void SetSoldState();
        void SetHasQuarterState();
        void AddQuarter();
        void DeleteQuarter();
        void ResetQuarters();
        int GetQuartersCount();
        int GetQuartersLimit();
        int GetGumballsCount();
    }
}