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
        if (args.Length < 1)
        {
            throw new Exception( "Invalid command format! Must be: DrawShape <id>" );
        }

        var id = args[ 0 ];

        _commandContext.Picture.DrawShape( id );
    }
}