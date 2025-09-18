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
        if ( args.Length < 2 )
        {
            throw new Exception( "Invalid command format! Must be: MovePicture <dx> <dy>" );
        }

        var dx = _commandContext.ShapeParser.ParseDouble( args[ 0 ] );
        var dy = _commandContext.ShapeParser.ParseDouble( args[ 1 ] );

        _commandContext.Picture.MovePicture( dx, dy );
    }
}