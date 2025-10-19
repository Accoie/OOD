namespace Shapes.ShapesManager.Commands;

public class ChangeShapeCommand : BasePictureCommand, IShapeCommand
{
    private ShapeParser _parser;

    public ChangeShapeCommand( Picture picture, ShapeParser parser ) : base( picture )
    {
        _parser = parser;
    }

    public override void Execute( string[] args )
    {
        if ( args.Length < 1 )
        {
            throw new Exception( "Invalid command format! Must be: ChangeShape <id> <shapeType> <parameters>" );
        }

        var id = args[ 0 ];
        var unparsedParams = args.Skip( 1 ).ToArray();
        var color = _picture.GetShapeColorById( id );

        var drawingStrategy = _parser.ParseDrawingStrategy( unparsedParams, color );

        _picture.ChangeShape( id, drawingStrategy );
    }
}