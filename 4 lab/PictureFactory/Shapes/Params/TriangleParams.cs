using PictureFactory.Types;

namespace PictureFactory.Shapes.Params
{
    public class TriangleParams : ShapeParams
    {
        public Point FirstVertex { get; }
        public Point SecondVertex { get; }
        public Point ThirdVertex { get; }

        public TriangleParams( Point firstVertex, Point secondVertex, Point thirdVertex, Color color )
            : base( ShapeType.Triangle, color )
        {
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
            ThirdVertex = thirdVertex;
        }
    }
}