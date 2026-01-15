using Slides.Canvas;
using Slides.Styles.FillStyles;
using Slides.Styles.LineStyles;
using Slides.Types;

namespace Slides.Shapes
{
    public class Rectangle : Shape
    {
        private Point _leftTop;
        private double _width;
        private double _height;

        public Rectangle( Frame frame,
            ILineStyle lineStyle,
            IFillStyle fillStyle,
            Point leftTop,
            double width,
            double height ) : base( frame, lineStyle, fillStyle )
        {
            _leftTop = leftTop;
            _width = width;
            _height = height;
        }

        public override void Draw( ICanvas canvas )
        {
            Point topLeft = new Point( _leftTop.X, _leftTop.Y );
            Point topRight = new Point( _leftTop.X + _width, _leftTop.Y );
            Point bottomRight = new Point( _leftTop.X + _width, _leftTop.Y + _height );
            Point bottomLeft = new Point( _leftTop.X, _leftTop.Y + _height );

            if ( HasFill() )
            {
                canvas.SetFillColor( GetFillColor() );
                canvas.FillPolygon( [ topLeft, topRight, bottomRight, bottomLeft ] );
            }

            if ( HasLine() )
            {
                canvas.SetLineColor( GetLineColor() );
                canvas.SetLineWidth( GetLineWidth() );

                canvas.DrawLine( topLeft, topRight );
                canvas.DrawLine( topRight, bottomRight );
                canvas.DrawLine( bottomRight, bottomLeft );
                canvas.DrawLine( bottomLeft, topLeft );
            }
        }

        protected override void Move( double dx, double dy )
        {
            _leftTop.X += dx;
            _leftTop.Y += dy;
        }

        protected override void Scale( double scaleX, double scaleY )
        {
            _width *= scaleX;
            _height *= scaleY;
            _leftTop.X *= scaleX;
            _leftTop.Y *= scaleY;
        }
    }
}