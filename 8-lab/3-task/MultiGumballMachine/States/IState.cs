namespace MultiGumballMachineApp.States
{
    public interface IState
    {
        void RefillGumballs( int count );
        void InsertQuarter();
        void EjectQuarter();
        void TurnCrank();
        void Dispense();
        string ToString();
    }
}