using ObjectAdapter.GraphicsLib;
using ObjectAdapter.ModernGraphicsLib;

namespace ObjectAdapter.Application
{
    public class ModernGraphicsRendererAdapter : ModernGraphicsRenderer, ICanvas
    {
        private Point _currentPoint = new Point( 0, 0 );

        public void MoveTo( double x, double y )
        {
            _currentPoint = new Point( x, y );
        }

        public void LineTo( double x, double y )
        {
            Point toPoint = new Point( x, y );

            BeginDraw();
            DrawLine( _currentPoint, toPoint );
            EndDraw();

            _currentPoint = toPoint;
        }
    }
}