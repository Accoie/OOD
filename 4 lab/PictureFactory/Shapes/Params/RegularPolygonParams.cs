using PictureFactory.Types;

namespace PictureFactory.Shapes.Params
{
    public class RegularPolygonParams : ShapeParams
    {
        public int VertexCount { get; }
        public Point Center { get; }
        public double Radius { get; }

        public RegularPolygonParams( int vertexCount, Point center, double radius, Color color )
            : base( ShapeType.RegularPolygon, color )
        {
            VertexCount = vertexCount;
            Center = center;
            Radius = radius;
        }
    }
}
