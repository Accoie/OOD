using Slides.Canvas;
using Slides.Styles.FillStyles;
using Slides.Styles.LineStyles;
using Slides.Types;

namespace Slides.Shapes
{
    public class Ellipse : Shape
    {
        private Point _center;
        private double _radiusX;
        private double _radiusY;

        public Ellipse( Frame frame,
            ILineStyle lineStyle,
            IFillStyle fillStyle,
            Point center,
            double radiusX,
            double radiusY ) : base( frame, lineStyle, fillStyle )
        {
            _center = center;
            _radiusX = radiusX;
            _radiusY = radiusY;
        }

        public override void Draw( ICanvas canvas )
        {
            if ( HasFill() )
            {
                canvas.SetFillColor( GetFillColor() );
                canvas.FillEllipse( _center, _radiusX, _radiusY );
            }

            if ( HasLine() )
            {
                canvas.SetLineColor( GetLineColor() );
                canvas.SetFillColor( GetFillColor() );

                canvas.DrawEllipse( _center, _radiusX, _radiusY );
            }
        }

        protected override void Move( double dx, double dy )
        {
            _center.X += dx;
            _center.Y += dy;
        }

        protected override void Scale( double scaleX, double scaleY )
        {
            _radiusX *= scaleX;
            _radiusY *= scaleY;
        }
    }
}
