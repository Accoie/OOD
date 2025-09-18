using SFML.Graphics;
using SFML.Window;
using Shapes.ShapesManager;
using ShapesCanvas;

public class ShapesApplication
{
    private const int WindowWidth = 800;
    private const int WindowHeight = 600;
    private const string WindowTitle = "Shapes!";
    private const uint FrameRateLimit = 60;

    private RenderWindow _renderWindow;
    private ShapesManager _shapesManager;

    public ShapesApplication()
    {
        _renderWindow = CreateRenderWindow();
        var canvas = new Canvas( _renderWindow );
        var picture = new Picture( canvas );
        _shapesManager = new ShapesManager( picture );
    }

    public void Run()
    {
        ConfigureWindow();

        RunMainLoop();
    }

    private RenderWindow CreateRenderWindow()
    {
        var videoMode = new VideoMode( WindowWidth, WindowHeight );

        var renderWindow = new RenderWindow( videoMode, WindowTitle ); ;

        return renderWindow;
    }

    private void ConfigureWindow()
    {
        _renderWindow.SetFramerateLimit( FrameRateLimit );
    }

    private void RunMainLoop()
    {
        while ( _renderWindow.IsOpen )
        {
            _renderWindow.DispatchEvents();

            _renderWindow.Clear( Color.White );

            ProcessUserInput();

            _renderWindow.Display();
        }
    }

    private void ProcessUserInput()
    {
        string userInput = Console.ReadLine() ?? string.Empty;

        _shapesManager.HandleCommandString( userInput );
    }
}