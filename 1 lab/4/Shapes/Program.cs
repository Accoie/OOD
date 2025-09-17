using SFML.Graphics;
using Shapes.Canvas;
using Shapes.ShapesManager;
using ShapesCanvas;

public class Program
{
    private static void Main( string[] args )
    {
        RenderWindow renderWindow = new RenderWindow(
            new SFML.Window.VideoMode( 800, 600 ),
            "Shapes!"
        );
        ICanvas canvas = new Canvas( renderWindow );

        Picture picture = new Picture( canvas );

        ShapesManager shapesManager = new ShapesManager(picture);

        renderWindow.SetFramerateLimit( 60 );

        renderWindow.Closed += ( sender, e ) => renderWindow.Close();

        while ( renderWindow.IsOpen )
        {
            renderWindow.DispatchEvents();
            
            renderWindow.Clear( Color.White );
            shapesManager.HandleCommandString( Console.ReadLine() ?? "" );
            renderWindow.Display();
        }
    }
}