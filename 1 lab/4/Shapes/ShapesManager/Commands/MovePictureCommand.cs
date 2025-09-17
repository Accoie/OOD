namespace Shapes.ShapesManager.Commands;

public class MovePictureCommand : ICommand
{
    private readonly CommandContext _commandContext;

    public MovePictureCommand( CommandContext commandContext )
    {
        _commandContext = commandContext;
    }

    public void Execute( string[] args )
    {
        double dx = _commandContext.ShapeParser.ParseDouble( args[ 0 ] );
        double dy = _commandContext.ShapeParser.ParseDouble( args[ 1 ] );
        _commandContext.Picture.MovePicture( dx, dy );
    }
}
