using Shapes.ShapesManager.Commands;
using Shapes.ShapesManager.Factories;

namespace Shapes.ShapesManager;

public class ShapesManager
{
    private readonly CommandContext _commandContext;
    private readonly CommandFactory _commandFactory;

    public ShapesManager( Picture picture )
    {
        _commandContext = new( picture, new ShapeParser() );
        _commandFactory = new CommandFactory( _commandContext );
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

        IShapeCommand command = _commandFactory.Create( commandName );

        command.Execute( shapeParams );
    }
}