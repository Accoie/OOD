using Shapes.ShapesManager.Commands;

namespace Shapes.ShapesManager.Factories;

public class CommandFactory
{
    private readonly CommandContext _context;

    public CommandFactory( CommandContext commandContext )
    {
        _context = commandContext;
    }

    public IShapeCommand Create( string commandName )
    {
        return commandName.ToLower() switch
        {
            "addshape" => new AddShapeCommand( _context.Picture, _context.ShapeParser ),
            "changeshape" => new ChangeShapeCommand( _context.Picture, _context.ShapeParser ),
            "changecolor" => new ChangeColorCommand( _context.Picture, _context.ShapeParser ),
            "deleteshape" => new DeleteShapeCommand( _context.Picture ),
            "moveshape" => new MoveShapeCommand( _context.Picture ),
            "movepicture" => new MovePictureCommand( _context.Picture ),
            "drawshape" => new DrawShapeCommand( _context.Picture ),
            "drawpicture" => new DrawPictureCommand( _context.Picture ),
            "list" => new ListCommand( _context.Picture ),
            _ => throw new InvalidOperationException( $"Unknown command: {commandName}" )
        };
    }
}