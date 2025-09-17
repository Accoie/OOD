using Shapes.Types;

namespace Shapes.ShapesParams;

public class CircleParams : ShapeParams
{
    public double Radius { get; set; }

    public CircleParams( Point center, float radius, string color )
        : base( color, [ center ] )
    {
        Radius = radius;
    }
}