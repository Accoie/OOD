namespace GumballMachineApp.GumballMachines
{
    public interface IGumballMachine
    {
        void ReleaseBall();
        int GetGumballsCount();

        void SetSoldOutState();
        void SetNoQuarterState();
        void SetSoldState();
        void SetHasQuarterState();
        public int GetQuarters();
        public int GetQuartersLimit();
        void AddQuarter();
        void DeleteQuarter();
        void ResetQuarters();
    }
}