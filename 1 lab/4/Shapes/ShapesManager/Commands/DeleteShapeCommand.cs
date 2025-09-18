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
        if ( args.Length < 1)
        {
            throw new Exception( "Invalid command format! Must be: DeleteShape <id>" );
        }

        var id = args[ 0 ];

        _commandContext.Picture.DeleteShape( id );
    }
}