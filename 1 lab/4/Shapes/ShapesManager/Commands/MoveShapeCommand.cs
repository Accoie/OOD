namespace Shapes.ShapesManager.Commands;

public class MoveShapeCommand : ICommand
{
    private readonly CommandContext _commandContext;

    public MoveShapeCommand( CommandContext commandContext )
    {
        _commandContext = commandContext;
    }

    public void Execute( string[] args )
    {
        if ( args.Length < 3)
        {
            throw new Exception( "Invalid command format! Must be: MoveShape <id> <dx> <dy>" );
        }

        var id = args[ 0 ];
        var dx = _commandContext.ShapeParser.ParseDouble( args[ 1 ] );
        var dy = _commandContext.ShapeParser.ParseDouble( args[ 2 ] );

        _commandContext.Picture.MoveShape( id, dx, dy );
    }
}