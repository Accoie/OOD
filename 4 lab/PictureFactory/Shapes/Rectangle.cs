using PictureFactory.Canvases;
using PictureFactory.Shapes.Params;
using PictureFactory.Types;

namespace PictureFactory.Shapes
{
    public class Rectangle : Shape
    {
        public Point LeftTop { get; private set; }
        public Point RightBottom { get; private set; }

        public Rectangle( RectangleParams @params ) : base( @params.Color )
        {
            LeftTop = @params.LeftTop;
            RightBottom = @params.RightBottom;
        }

        public override void Draw( ICanvas canvas )
        {
            canvas.SetColor( Color );

            Point rightTop = new Point( RightBottom.X, LeftTop.Y );
            Point leftBottom = new Point( LeftTop.X, RightBottom.Y );

            canvas.DrawLine( LeftTop, rightTop );
            canvas.DrawLine( rightTop, RightBottom );
            canvas.DrawLine( RightBottom, leftBottom );
            canvas.DrawLine( leftBottom, LeftTop );
        }

        public override ShapeType GetShapeType()
        {
            return ShapeType.Rectangle;
        }
    }
}