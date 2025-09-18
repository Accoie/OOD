namespace Shapes.ShapesManager.Commands;

public class MovePictureCommand : IShapeCommand
{
    public CommandContext Context { get; }

    public MovePictureCommand( CommandContext commandContext )
    {
        Context = commandContext;
    }

    public void Execute( string[] args )
    {
        if ( args.Length < 2 )
        {
            throw new Exception( "Invalid command format! Must be: MovePicture <dx> <dy>" );
        }

        var dx = Context.ShapeParser.ParseDouble( args[ 0 ] );
        var dy = Context.ShapeParser.ParseDouble( args[ 1 ] );

        Context.Picture.MovePicture( dx, dy );
    }
}