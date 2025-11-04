namespace MultiGumballMachineApp.GumballMachines
{
    public interface IGumballMachineClient
    {
        void RefillGumballs(int count);
        int GetGumballsCount();
        void TurnCrank();
        void InsertQuarter();
        void EjectQuarter();
        int GetQuartersCount();
        int GetQuartersLimit();
    }
}
