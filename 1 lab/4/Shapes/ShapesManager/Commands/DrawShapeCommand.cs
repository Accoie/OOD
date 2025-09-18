namespace Shapes.ShapesManager.Commands;

public class DrawShapeCommand : IShapeCommand
{
    public CommandContext Context { get; }

    public DrawShapeCommand( CommandContext commandContext )
    {
        Context = commandContext;
    }

    public void Execute( string[] args )
    {
        if (args.Length < 1)
        {
            throw new Exception( "Invalid command format! Must be: DrawShape <id>" );
        }

        var id = args[ 0 ];

        Context.Picture.DrawShape( id );
    }
}