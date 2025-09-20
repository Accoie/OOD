namespace Shapes.ShapesManager.Commands;

public class DeleteShapeCommand : BasePictureCommand, IShapeCommand
{
    public DeleteShapeCommand( Picture picture ) : base( picture )
    {
    }

    public override void Execute( string[] args )
    {
        if ( args.Length < 1)
        {
            throw new Exception( "Invalid command format! Must be: DeleteShape <id>" );
        }

        var id = args[ 0 ];

        _picture.DeleteShape( id );
    }
}