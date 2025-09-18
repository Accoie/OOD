namespace Shapes.ShapesManager.Commands;

public class ChangeShapeCommand : ICommand
{
    private readonly CommandContext _commandContext;

    public ChangeShapeCommand( CommandContext commandContext )
    {
        _commandContext = commandContext;
    }

    public void Execute( string[] args )
    {
        if (args.Length < 1 )
        {
            throw new Exception( "Invalid command format! Must be: ChangeShape <id> <shapeType> <parameters>" );
        }

        var id = args[ 0 ];
        var unparsedParams = args.Skip( 1 ).ToArray();
        var color = _commandContext.Picture.GetShapeColorById( id );

        var drawingStrategy = _commandContext.ShapeParser.ParseDrawingStrategy( unparsedParams, color );

        _commandContext.Picture.ChangeShape( id, drawingStrategy );
    }
}