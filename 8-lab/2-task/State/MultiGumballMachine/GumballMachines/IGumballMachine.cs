namespace MultiGumballMachineApp.GumballMachines
{
    public interface IGumballMachine
    {
        void ReleaseBall();
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