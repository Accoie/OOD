namespace ShapesApplication.Models
{
    public interface ICommand
    {
        void Execute();
        void Unexecute();
    }
}