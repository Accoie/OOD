namespace Shapes.ShapesManager.Commands;

public class DrawShapeCommand : ICommand
{
    private readonly CommandContext _commandContext;

    public DrawShapeCommand( CommandContext commandContext )
    {
        _commandContext = commandContext;
    }

    public void Execute( string[] args )
    {
        string id = args[ 0 ];
        _commandContext.Picture.DrawShape( id );
    }
}