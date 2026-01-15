namespace ShapesApplication.Models.History
{
    public interface IHistory
    {
        void AddAndExecuteCommand( ICommand command );
        bool CanUndo { get; }
        bool CanRedo { get; }
        void Undo();
        void Redo();
    }
}