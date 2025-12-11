using PictureFactory.Types;

namespace PictureFactory.Shapes.Params
{
    public class ShapeParams
    {
        public ShapeType ShapeType { get; }
        public Color Color { get; }

        public ShapeParams( ShapeType shapeType, Color color )
        {
            ShapeType = shapeType;
            Color = color;
        }
    }
}
