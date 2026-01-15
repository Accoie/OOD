using ObjectAdapter.GraphicsLib;
using ObjectAdapter.ModernGraphicsLib;

namespace ObjectAdapter.Application
{
    public class ModernGraphicsRendererAdapter : ICanvas
    {
        private ModernGraphicsLib.Point _currentPoint = new ModernGraphicsLib.Point( 0, 0 );
        private readonly IModernGraphicsRenderer _modernGraphicsRenderer;

        public ModernGraphicsRendererAdapter( IModernGraphicsRenderer renderer )
        {
            _modernGraphicsRenderer = renderer;
        }

        public void MoveTo( double x, double y )
        {
            _currentPoint = new ModernGraphicsLib.Point( x, y );
        }

        public void LineTo( double x, double y )
        {
            ModernGraphicsLib.Point toPoint = new ModernGraphicsLib.Point( x, y );

            _modernGraphicsRenderer.BeginDraw();
            _modernGraphicsRenderer.DrawLine( _currentPoint, toPoint );
            _modernGraphicsRenderer.EndDraw();

            _currentPoint = toPoint;
        }
    }
}