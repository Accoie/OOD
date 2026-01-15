using ObjectAdapter.GraphicsLib;
using ObjectAdapter.ModernGraphicsLib;

namespace ObjectAdapter.Application
{
    public class ModernGraphicsRendererAdapter : ModernGraphicsRenderer, ICanvas
    {
        private Point _currentPoint = new Point( 0, 0 );
        private RGBAColor _color = new RGBAColor( 0, 0, 0, 1.0 );

        public void MoveTo( double x, double y )
        {
            _currentPoint = new Point( x, y );
        }

        public void LineTo( double x, double y )
        {
            Point toPoint = new Point( x, y );

            BeginDraw();
            DrawLine( _currentPoint, toPoint, _color );
            EndDraw();

            _currentPoint = toPoint;
        }

        public void SetColor( int rgbColor )
        {
            double red = ( ( rgbColor >> 16 ) & 0xFF ) / 255.0;
            double green = ( ( rgbColor >> 8 ) & 0xFF ) / 255.0;
            double blue = ( rgbColor & 0xFF ) / 255.0;

            _color = new RGBAColor( red, green, blue, 1.0 );
        }
    }
}