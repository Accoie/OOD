namespace Shapes.ShapesManager.Commands;

public class DrawPictureCommand : ICommand
{
    private readonly CommandContext _commandContext;

    public DrawPictureCommand( CommandContext commandContext )
    {
        _commandContext = commandContext;
    }

    public void Execute( string[] args )
    {
        _commandContext.Picture.DrawPicture();
    }
}
