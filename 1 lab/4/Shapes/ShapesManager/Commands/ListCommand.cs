namespace Shapes.ShapesManager.Commands;

public class ListCommand : ICommand
{
    private readonly CommandContext _commandContext;

    public ListCommand( CommandContext commandContext )
    {
        _commandContext = commandContext;
    }

    public void Execute( string[] args )
    {
        Console.WriteLine( _commandContext.Picture.GetShapesInfo() );
    }
}