using Shapes.ShapesManager.Commands;

namespace Shapes.ShapesManager;

public class ShapesManager
{
    private readonly Picture _picture;
    private readonly ShapeParser _shapeParser = new();
    private readonly CommandContext _commandContext;

    public ShapesManager( Picture picture )
    {
        _picture = picture;
        _commandContext = new( _picture, _shapeParser );
    }

    public void HandleCommandString( string commandStr )
    {
        string[] commandItems = commandStr.Split( ' ' );
        var commandName = commandItems[ 0 ];
        var shapeParams = commandItems.Skip( 1 ).ToArray();

        try
        {
            var commandFactory = new CommandFactory( _commandContext );

            ICommand command = commandFactory.Create( commandName );

            command.Execute( shapeParams );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }
}