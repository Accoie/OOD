namespace Shapes.ShapesManager.Commands;

public class ChangeColorCommand : BasePictureCommand, IShapeCommand
{
    private ShapeParser _parser;

    public ChangeColorCommand( Picture picture, ShapeParser parser ) : base( picture )
    {
        _parser = parser;
    }

    public override void Execute( string[] args )
    {
        if ( args.Length < 1 )
        {
            throw new Exception( "Invalid command format! Must be: ChangeColor <id> <color>" );
        }

        var id = args[ 0 ];
        var color = _parser.ValidateColor( args[ 1 ] );

        _picture.ChangeColor( id, color );
    }
}