using PictureFactory.Types;

namespace PictureFactory.Shapes.Params
{
    public class RectangleParams : ShapeParams
    {
        public RectangleParams( Point leftTop, Point rightBottom, Color color ) : base( ShapeType.Rectangle, color )
        {
            LeftTop = leftTop;
            RightBottom = rightBottom;
        }

        public Point LeftTop { get; private set; }
        public Point RightBottom { get; private set; }
    }
}
