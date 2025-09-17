namespace Shapes.ShapesManager.Commands;

public class ChangeShapeCommand : ICommand
{
    private readonly CommandContext _commandContext;

    public ChangeShapeCommand( CommandContext commandContext )
    {
        _commandContext = commandContext;
    }

    public void Execute( string[] args )
    {
        string id = args[ 0 ];
        string[] unparsedParams = args.Skip( 1 ).ToArray();

        var drawingStrategy = _commandContext.ShapeParser.Parse( unparsedParams, _commandContext.Picture.GetShapeColorById( id ) );
        _commandContext.Picture.ChangeShape( id, drawingStrategy );
    }
}