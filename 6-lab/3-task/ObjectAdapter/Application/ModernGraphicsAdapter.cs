using ObjectAdapter.GraphicsLib;
using ObjectAdapter.ModernGraphicsLib;

namespace ObjectAdapter.Application
{
    public class ModernGraphicsRendererAdapter : ICanvas
    {
        private readonly IModernGraphicsRenderer _modernGraphicsRenderer;

        private Point _currentPoint = new( 0, 0 );
        private RGBAColor _color = new( 0, 0, 0, 1.0 );

        public ModernGraphicsRendererAdapter( IModernGraphicsRenderer renderer )
        {
            _modernGraphicsRenderer = renderer;
        }

        public void MoveTo( double x, double y )
        {
            _currentPoint = new Point( x, y );
        }

        public void LineTo( double x, double y )
        {
            Point toPoint = new( x, y );

            _modernGraphicsRenderer.BeginDraw();
            _modernGraphicsRenderer.DrawLine( _currentPoint, toPoint, _color );
            _modernGraphicsRenderer.EndDraw();

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