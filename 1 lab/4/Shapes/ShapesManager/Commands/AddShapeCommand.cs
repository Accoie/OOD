namespace Shapes.ShapesManager.Commands;

public class AddShapeCommand : IShapeCommand
{
    public CommandContext Context { get; }

    public AddShapeCommand(CommandContext commandContext)
    {
        Context = commandContext;
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

        var drawingStrategy = Context.ShapeParser.ParseDrawingStrategy( unparsedParams, color );

        Context.Picture.AddShape( id, drawingStrategy );
    }
}