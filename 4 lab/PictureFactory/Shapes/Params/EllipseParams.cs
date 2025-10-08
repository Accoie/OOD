using PictureFactory.Types;

namespace PictureFactory.Shapes.Params
{
    public class EllipseParams : ShapeParams
    {
        public EllipseParams( Point center, double radiusX, double radiusY, Color color ) : base( ShapeType.Ellipse, color )
        {
            Center = center;
            RadiusX = radiusX;
            RadiusY = radiusY;
        }

        public Point Center { get; private set; }
        public double RadiusX { get; private set; }
        public double RadiusY { get; private set; }
    }
}
