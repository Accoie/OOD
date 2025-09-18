namespace Shapes.ShapesManager.Commands;

public class DrawPictureCommand : IShapeCommand
{
    public CommandContext Context { get; }

    public DrawPictureCommand( CommandContext commandContext )
    {
        Context = commandContext;
    }

    public void Execute( string[] args )
    {
        Context.Picture.DrawPicture();
    }
}