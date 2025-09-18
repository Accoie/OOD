namespace Shapes.ShapesManager.Commands;

public class DeleteShapeCommand : IShapeCommand
{
    public CommandContext Context { get; }

    public DeleteShapeCommand( CommandContext commandContext )
    {
        Context = commandContext;
    }

    public void Execute( string[] args )
    {
        if ( args.Length < 1)
        {
            throw new Exception( "Invalid command format! Must be: DeleteShape <id>" );
        }

        var id = args[ 0 ];

        Context.Picture.DeleteShape( id );
    }
}