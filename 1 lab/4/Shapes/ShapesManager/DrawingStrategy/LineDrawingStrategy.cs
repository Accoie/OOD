using Shapes.Canvas;
using Shapes.ShapesParams;

namespace Shapes.ShapesManager.DrawingStrategy;

public class LineDrawingStrategy : BaseDrawingStrategy
{
    public override IShapeParams ShapeParams { get; }

    public LineDrawingStrategy( LineParams lineParams )
    {
        ShapeParams = lineParams;
    }

    public override void Draw( ICanvas canvas )
    {
        canvas.SetColor( ShapeParams.Color );
        canvas.MoveTo( ShapeParams.Vertices[ 0 ] );
        canvas.LineTo( ShapeParams.Vertices[ 1 ] );
    }

    public override string GetInfo()
    {
        var vertices = ShapeParams.Vertices;

        return "line; color: #" + $"{ShapeParams.Color}; from: {vertices[ 0 ]}; to: {vertices[ 1 ]}";
    }
}