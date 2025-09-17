using Shapes.Types;

namespace Shapes.ShapesParams;

public class LineParams : ShapeParams
{
    public LineParams( Point from, Point to, string color )
        : base( color, [ from, to ] )
    {
    }
}