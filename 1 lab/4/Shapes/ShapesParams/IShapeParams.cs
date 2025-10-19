using Shapes.Types;

namespace Shapes.ShapesParams;

public interface IShapeParams
{
    string Color { get; set; }
    List<Point> Vertices { get; set; } // переделать vertices 
}