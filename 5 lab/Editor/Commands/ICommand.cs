namespace Editor.Commands
{
    public interface ICommand
    {
        void Execute();
        void Unexecute();
    }
}
