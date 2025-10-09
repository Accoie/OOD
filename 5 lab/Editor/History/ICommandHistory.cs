using Editor.Commands;

namespace Editor.History
{
    public interface ICommandHistory
    {
        bool CanUndo();
        void Undo();
        bool CanRedo();
        void Redo();
        void AddAndExecuteCommand( ICommand command );
    }
}