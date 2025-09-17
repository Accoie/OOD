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
        string id = args[ 0 ];
        double dx = _commandContext.ShapeParser.ParseDouble( args[ 1 ] );
        double dy = _commandContext.ShapeParser.ParseDouble( args[ 2 ] );
        _commandContext.Picture.MoveShape( id, dx, dy );
    }
}
