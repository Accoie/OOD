namespace Shapes.ShapesManager.Commands;

public class MoveShapeCommand : BasePictureCommand, IShapeCommand
{
    public MoveShapeCommand( Picture picture ) : base( picture )
    {
    }

    public override void Execute( string[] args )
    {
        if ( args.Length < 3)
        {
            throw new Exception( "Invalid command format! Must be: MoveShape <id> <dx> <dy>" );
        }

        var id = args[ 0 ];
        var dx = ShapeParser.ParseDouble( args[ 1 ] );
        var dy = ShapeParser.ParseDouble( args[ 2 ] );

        _picture.MoveShape( id, dx, dy );
    }
}