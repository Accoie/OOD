using Shapes.Types;

namespace Shapes.ShapesParams;

public class TriangleParams : ShapeParams
{
    public TriangleParams( Point firstPoint, Point secondPoint, Point thirdPoint, string color )
        : base( color, [ firstPoint, secondPoint, thirdPoint ] )
    {
    }
}