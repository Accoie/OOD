namespace Editor.Commands;

public class HelpCommand : ICommand
{
    private readonly Dictionary<string, string> _availableCommandDescriptions;

    public HelpCommand( Dictionary<string, string> availableCommandDescriptions )
    {
        _availableCommandDescriptions = availableCommandDescriptions;
    }

    public void Execute()
    {
        foreach ( (string command, string description) in _availableCommandDescriptions )
        {
            Console.WriteLine( $"{command} — {description}" );
        }
    }

    public void Unexecute()
    {
    }
}