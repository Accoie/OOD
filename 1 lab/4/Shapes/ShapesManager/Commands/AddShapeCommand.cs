namespace Shapes.ShapesManager.Commands;

public class AddShapeCommand : BasePictureCommand, IShapeCommand
{
    private ShapeParser _parser;

    public AddShapeCommand( Picture picture, ShapeParser parser ) : base( picture )
    {
        _parser = parser;
    }

    public override void Execute( string[] args )
    {
        if ( args.Length < 2 )
        {
            throw new Exception( "Invalid command format! Must be: AddShape <id> <color> <shapeType> <parameters>" );
        }

        var id = args[ 0 ];
        var color = args[ 1 ];
        var unparsedParams = args.Skip( 2 ).ToArray();

        var drawingStrategy = _parser.ParseDrawingStrategy( unparsedParams, color );

        _picture.AddShape( id, drawingStrategy );
    }
}