namespace Editor.History;

public interface ICommandHistoryUser
{
    bool CanUndo();
    void Undo();
    bool CanRedo();
    void Redo();
}
