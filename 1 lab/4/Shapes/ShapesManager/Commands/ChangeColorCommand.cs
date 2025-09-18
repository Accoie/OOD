namespace Shapes.ShapesManager.Commands;

public class ChangeColorCommand : ICommand
{
    private readonly CommandContext _commandContext;

    public ChangeColorCommand( CommandContext commandContext )
    {
        _commandContext = commandContext;
    }

    public void Execute( string[] args )
    {
        if ( args.Length < 1 )
        {
            throw new Exception( "Invalid command format! Must be: ChangeColor <id> <color>" );
        }

        var id = args[ 0 ];
        var color = _commandContext.ShapeParser.ValidateColor( args[ 1 ] );

        _commandContext.Picture.ChangeColor( id, color );
    }
}