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
    }
}