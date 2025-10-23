using Slides.Canvas;
using Slides.Styles.FillStyles;
using Slides.Styles.LineStyles;
using Slides.Types;

namespace Slides.Shapes
{
    public class Triangle : Shape
    {
        private Point _firstPoint;
        private Point _secondPoint;
        private Point _thirdPoint;

        public Triangle( ILineStyle lineStyle,
            IFillStyle fillStyle,
            Frame frame,
            Point firstPoint,
            Point secondPoint,
            Point thirdPoint ) : base( frame, lineStyle, fillStyle )
        {
            _firstPoint = firstPoint;
            _secondPoint = secondPoint;
            _thirdPoint = thirdPoint;
        }

        public override void Draw( ICanvas canvas )
        {
            if ( HasFill() )
            {
                canvas.SetFillColor( GetFillColor() );
                canvas.FillPolygon( [ _firstPoint, _secondPoint, _thirdPoint ] );
            }

            if ( HasLine() )
            {
                canvas.SetLineColor( GetLineColor() );
                canvas.SetLineWidth( GetLineWidth() );
                canvas.DrawLine( _firstPoint, _secondPoint );
                canvas.DrawLine( _secondPoint, _thirdPoint );
                canvas.DrawLine( _thirdPoint, _firstPoint );
            }
        }

        protected override void Move( double dx, double dy )
        {
            _firstPoint.X += dx;
            _firstPoint.Y += dy;

            _secondPoint.X += dx;
            _secondPoint.Y += dy;

            _thirdPoint.X += dx;
            _thirdPoint.Y += dy;
        }

        protected override void Scale( double scaleX, double scaleY )
        {
            _frame.Width *= scaleX;
            _frame.Height *= scaleY;
        }
    }
}
