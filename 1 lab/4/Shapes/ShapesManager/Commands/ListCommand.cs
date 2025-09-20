namespace Shapes.ShapesManager.Commands;

public class ListCommand : BasePictureCommand, IShapeCommand
{
    public ListCommand( Picture picture ) : base( picture )
    {
    }

    public override void Execute( string[] args )
    {
        Console.WriteLine( _picture.GetShapesInfo() );
    }
}