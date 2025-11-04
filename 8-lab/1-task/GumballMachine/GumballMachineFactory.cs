using GumballMachineApp.GumballMachines;

namespace GumballMachineApp
{
    public static class GumballMachineFactory
    {
        public static IGumballMachineClient CreateGumballMachine( int gumballCount )
        {
            return new GumballMachine( gumballCount );
        }
    }
}
