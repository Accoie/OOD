namespace Shapes.ShapesManager.Commands;

public class MovePictureCommand : BasePictureCommand, IShapeCommand
{
    public MovePictureCommand( Picture picture ) : base( picture )
    {
    }

    public override void Execute( string[] args )
    {
        if ( args.Length < 2 )
        {
            throw new Exception( "Invalid command format! Must be: MovePicture <dx> <dy>" );
        }

        var dx = ShapeParser.ParseDouble( args[ 0 ] );
        var dy = ShapeParser.ParseDouble( args[ 1 ] );

        _picture.MovePicture( dx, dy );
    }
}