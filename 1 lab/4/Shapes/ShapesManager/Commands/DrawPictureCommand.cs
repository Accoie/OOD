namespace Shapes.ShapesManager.Commands;

public class DrawPictureCommand : BasePictureCommand, IShapeCommand
{
    public DrawPictureCommand( Picture picture) : base( picture )
    {
        _picture = picture;
    }

    public override void Execute( string[] args )
    {
        _picture.DrawPicture();
    }
}