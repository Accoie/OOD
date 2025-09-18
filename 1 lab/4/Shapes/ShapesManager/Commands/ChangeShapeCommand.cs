namespace Shapes.ShapesManager.Commands;

public class ChangeShapeCommand : IShapeCommand
{
    public CommandContext Context { get; }

    public ChangeShapeCommand( CommandContext commandContext )
    {
        Context = commandContext;
    }

    public void Execute( string[] args )
    {
        if (args.Length < 1 )
        {
            throw new Exception( "Invalid command format! Must be: ChangeShape <id> <shapeType> <parameters>" );
        }

        var id = args[ 0 ];
        var unparsedParams = args.Skip( 1 ).ToArray();
        var color = Context.Picture.GetShapeColorById( id );

        var drawingStrategy = Context.ShapeParser.ParseDrawingStrategy( unparsedParams, color );

        Context.Picture.ChangeShape( id, drawingStrategy );
    }
}