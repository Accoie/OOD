using Shapes.ShapesManager.Commands;

namespace Shapes.ShapesManager.Factories;

public class CommandFactory
{
    private readonly CommandContext _commandContext;

    public CommandFactory( CommandContext commandContext )
    {
        _commandContext = commandContext;
    }

    public ICommand Create( string commandName )
    {
        return commandName.ToLower() switch
        {
            "addshape" => new AddShapeCommand( _commandContext ),
            "changeshape" => new ChangeShapeCommand( _commandContext ),
            "changecolor" => new ChangeColorCommand( _commandContext ),
            "deleteshape" => new DeleteShapeCommand( _commandContext ),
            "moveshape" => new MoveShapeCommand( _commandContext ),
            "movepicture" => new MovePictureCommand( _commandContext ),
            "drawshape" => new DrawShapeCommand( _commandContext ),
            "drawpicture" => new DrawPictureCommand( _commandContext ),
            "list" => new ListCommand( _commandContext ),
            _ => throw new InvalidOperationException( $"Unknown command: {commandName}" )
        };
    }
}