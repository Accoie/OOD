namespace Shapes.ShapesManager.Commands;

public class DeleteShapeCommand : ICommand
{
    private readonly CommandContext _commandContext;

    public DeleteShapeCommand( CommandContext commandContext )
    {
        _commandContext = commandContext;
    }

    public void Execute( string[] args )
    {
        string id = args[ 0 ];
        _commandContext.Picture.DeleteShape( id );
    }
}