using Editor.Commands;

namespace Editor.History;

public interface ICommandHistory : ICommandHistoryUser
{
    void AddAndExecuteCommand( ICommand command );
}