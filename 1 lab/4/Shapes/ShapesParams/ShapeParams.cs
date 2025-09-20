using Shapes.Types;

namespace Shapes.ShapesParams;

public abstract class ShapeParams : IShapeParams
{
    public string Color { get; set; }
    public List<Point> Vertices { get; set; }

    protected ShapeParams( string color, List<Point> drawPoints )
    {
        Color = color;
        Vertices = drawPoints;
    }
}