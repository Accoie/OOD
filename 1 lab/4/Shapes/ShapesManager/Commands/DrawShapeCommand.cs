namespace Shapes.ShapesManager.Commands;

public class DrawShapeCommand : BasePictureCommand, IShapeCommand
{
    public DrawShapeCommand( Picture picture ) : base( picture )
    {
    }

    public override void Execute( string[] args )
    {
        if (args.Length < 1)
        {
            throw new Exception( "Invalid command format! Must be: DrawShape <id>" );
        }

        var id = args[ 0 ];

        _picture.DrawShape( id );
    }
}