namespace Shapes.ShapesManager.Commands;

public class AddShapeCommand : ICommand
{
    private readonly CommandContext _commandContext;

    public AddShapeCommand(CommandContext commandContext)
    {
        _commandContext = commandContext;
    }

    public void Execute( string[] args )
    {
        string id = args[ 0 ];
        string color = args[ 1 ];
        string[] unparsedParams = args.Skip( 2 ).ToArray();

        var drawingStrategy = _commandContext.ShapeParser.Parse( unparsedParams, color );
        _commandContext.Picture.AddShape( id, drawingStrategy );
    }
}