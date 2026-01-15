namespace GumballMachineApp.GumballMachines
{
    public interface IGumballMachineClient
    {
        void EjectQuarter();
        void InsertQuarter();
        void TurnCrank();
        string ToString();
    }
}