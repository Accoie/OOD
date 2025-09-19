namespace Shapes.ShapesManager.Commands;

public abstract class BasePictureCommand : IShapeCommand
{
    protected Picture _picture;

    protected BasePictureCommand( Picture picture )
    {
        _picture = picture;
    }

    public abstract void Execute( string[] args );
}