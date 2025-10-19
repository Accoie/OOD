using Shapes.Types;

namespace Shapes.ShapesParams;

public class RectangleParams : ShapeParams
{
    public RectangleParams( Point leftTop, Point rightBottom, string color )
        : base( color, [ leftTop, rightBottom ] )
    {
    }
}