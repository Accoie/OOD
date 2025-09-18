using Shapes.ShapesManager.Commands;
using Shapes.ShapesManager.Factories;

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
        string[] commandItems = commandStr.Split( ' ', StringSplitOptions.RemoveEmptyEntries );

        try
        {
            HandleCommandItems( commandItems );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    private void HandleCommandItems( string[] commandItems )
    {
        if ( commandItems.Length == 0 )
        {
            throw new Exception( "Please, enter the command" );
        }

        var commandName = commandItems[ 0 ];

        var shapeParams = commandItems.Skip( 1 ).ToArray();

        var commandFactory = new CommandFactory( _commandContext );

        ICommand command = commandFactory.Create( commandName );

        command.Execute( shapeParams );
    }
}