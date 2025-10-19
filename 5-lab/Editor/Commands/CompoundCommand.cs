namespace Editor.Commands;

public class CompoundCommand : ICommand
{
    private bool _execute = false;

    public IReadOnlyList<ICommand> Commands { get; }

    public CompoundCommand( ICommand[] commands )
    {
        Commands = commands;
    }

    public void Execute()
    {
        if ( _execute )
        {
            return;
        }
        _execute = !_execute;

        foreach ( ICommand command in Commands )
        {
            command.Execute();
        }
    }

    public void Unexecute()
    {
        if ( _execute )
        {
            for ( int i = Commands.Count - 1; i >= 0; i-- )
            {
                Commands[ i ].Unexecute();
            }
            _execute = false;
        }
    }
}
