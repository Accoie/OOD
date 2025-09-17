using SFML.Graphics;
using SFML.System;

using Shapes.Canvas;
using Shapes.Types;

namespace ShapesCanvas;

public class Canvas : ICanvas
{
    private readonly RenderWindow _window;
    private Color _currentColor = Color.Black;
    private Vector2f _currentPosition;
    private readonly Font _fontFamily = new Font( "Canvas/arialmt.ttf" );

    public Canvas( RenderWindow window )
    {
        _window = window;
    }

    public void DrawEllipse( Point centerPoint, double radiusWidth, double radiusHeight )
    {
        var ellipse = new CircleShape( ( float )radiusWidth, 100 );

        ellipse.Scale = new Vector2f( 1.0f, ( float )( radiusHeight / radiusWidth ) );
        ellipse.FillColor = _currentColor;
        ellipse.OutlineColor = _currentColor;
        ellipse.OutlineThickness = 1;
        ellipse.Position = new Vector2f( ( float )( centerPoint.X - ( float )radiusWidth ), ( float )( centerPoint.Y - ( float )radiusHeight ) );

        _window.Draw( ellipse );
    }
    public void FillPolygon( List<Point> points )
    {
        if ( points.Count < 3 )
            return;

        var vertices = new Vertex[ points.Count ];
        for ( int i = 0; i < points.Count; i++ )
        {
            vertices[ i ] = new Vertex(
                new Vector2f( ( float )points[ i ].X, ( float )points[ i ].Y ),
                _currentColor
            );
        }

        _window.Draw( vertices, PrimitiveType.TriangleFan );
    }

    public void LineTo( Point point )
    {
        var line = new VertexArray( PrimitiveType.Lines, 2 );

        line[ 0 ] = new Vertex( _currentPosition, _currentColor );
        line[ 1 ] = new Vertex( new Vector2f( ( float )point.X, ( float )point.Y ), _currentColor );

        _window.Draw( line );
        _currentPosition = new Vector2f( ( float )point.X, ( float )point.Y );
    }

    public void MoveTo( Point point )
    {
        _currentPosition = new Vector2f( ( float )point.X, ( float )point.Y );
    }

    public void PrintText( Point leftTop, double size, string text, string color )
    {
        var sfText = new Text( text, _fontFamily, ( uint )size )
        {
            FillColor = ParseColor( color ),
            Position = new Vector2f( ( float )leftTop.X, ( float )leftTop.Y )
        };

        _window.Draw( sfText );
    }

    public void SetColor( string color )
    {
        _currentColor = ParseColor( color );
    }

    private static Color ParseColor( string colorName )
    {
        try
        {
            byte r = Convert.ToByte( colorName.Substring( 0, 2 ), 16 );
            byte g = Convert.ToByte( colorName.Substring( 2, 2 ), 16 );
            byte b = Convert.ToByte( colorName.Substring( 4, 2 ), 16 );

            return new Color( r, g, b );
        }
        catch
        {
            return Color.Black;
        }
    }
}
