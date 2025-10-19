using Shapes.Canvas;
using Shapes.ShapesParams;
using Shapes.Types;

namespace Shapes.ShapesManager.DrawingStrategy;

public abstract class BaseDrawingStrategy
{
    abstract public IShapeParams ShapeParams { get; }

    abstract public void Draw( ICanvas canvas );

    abstract public string GetInfo();

    public void Move( ICanvas canvas, double dx, double dy )
    {
        ShapeParams.Vertices = ShapeParams.Vertices
            .Select( vertex => new Point( vertex.X + dx, vertex.Y + dy ) )
            .ToList();
    }
}