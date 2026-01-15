namespace MultiGumballMachineApp.GumballMachines
{
    public interface IGumballMachineClient
    {
        int GetGumballsCount();
        void TurnCrank();
        void InsertQuarter();
        void EjectQuarter();
        int GetQuartersCount();
        int GetQuartersLimit();
    }
}
