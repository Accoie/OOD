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
        string id = args[ 0 ];
        string color = _commandContext.ShapeParser.ValidateColor( args[ 1 ] );
        _commandContext.Picture.ChangeColor( id, color );
    }
}