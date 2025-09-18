namespace Shapes.ShapesManager.Commands;

public class AddShapeCommand : ICommand
{
    private readonly CommandContext _commandContext;

    public AddShapeCommand(CommandContext commandContext)
    {
        _commandContext = commandContext;
    }

    public void Execute( string[] args )
    {
        if ( args.Length < 2 )
        {
            throw new Exception( "Invalid command format! Must be: AddShape <id> <color> <shapeType> <parameters>" );
        }

        var id = args[ 0 ];
        var color = args[ 1 ];
        var unparsedParams = args.Skip( 2 ).ToArray();

        var drawingStrategy = _commandContext.ShapeParser.ParseDrawingStrategy( unparsedParams, color );

        _commandContext.Picture.AddShape( id, drawingStrategy );
    }
}