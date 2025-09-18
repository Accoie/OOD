namespace Shapes.ShapesManager.Commands;

public class ChangeColorCommand : IShapeCommand
{
    public CommandContext Context { get; }

    public ChangeColorCommand( CommandContext commandContext )
    {
        Context = commandContext;
    }

    public void Execute( string[] args )
    {
        if ( args.Length < 1 )
        {
            throw new Exception( "Invalid command format! Must be: ChangeColor <id> <color>" );
        }

        var id = args[ 0 ];
        var color = Context.ShapeParser.ValidateColor( args[ 1 ] );

        Context.Picture.ChangeColor( id, color );
    }
}