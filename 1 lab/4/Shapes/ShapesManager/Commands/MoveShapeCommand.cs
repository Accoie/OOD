namespace Shapes.ShapesManager.Commands;

public class MoveShapeCommand : IShapeCommand
{
    public CommandContext Context { get; }

    public MoveShapeCommand( CommandContext commandContext )
    {
        Context = commandContext;
    }

    public void Execute( string[] args )
    {
        if ( args.Length < 3)
        {
            throw new Exception( "Invalid command format! Must be: MoveShape <id> <dx> <dy>" );
        }

        var id = args[ 0 ];
        var dx = Context.ShapeParser.ParseDouble( args[ 1 ] );
        var dy = Context.ShapeParser.ParseDouble( args[ 2 ] );

        Context.Picture.MoveShape( id, dx, dy );
    }
}