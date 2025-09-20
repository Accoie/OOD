namespace SimUDuck.DucksActions.Fly
{
    public interface IFlyBehavior
    {
        int FlyCount { get; }

        void Fly();
    }
}
