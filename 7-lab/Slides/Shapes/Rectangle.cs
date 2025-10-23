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
            Point firstPoint = _leftTop;
            Point secondPoint = new Point( firstPoint.X, firstPoint.Y + _height );
            Point thirdPoint = new Point( secondPoint.X + _width, secondPoint.Y );
            Point fourthPoint = new Point( thirdPoint.X, secondPoint.Y - _height );

            if ( HasFill() )
            {
                canvas.SetFillColor( GetFillColor() );
                canvas.FillPolygon( [ firstPoint, secondPoint, thirdPoint, fourthPoint ] );
            }

            if ( HasLine() )
            {
                canvas.SetLineColor( GetLineColor() );
                canvas.SetLineWidth( GetLineWidth() );

                canvas.DrawLine( firstPoint, secondPoint );
                canvas.DrawLine( secondPoint, thirdPoint );
                canvas.DrawLine( thirdPoint, fourthPoint );
                canvas.DrawLine( fourthPoint, firstPoint );
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
        }
    }
}
