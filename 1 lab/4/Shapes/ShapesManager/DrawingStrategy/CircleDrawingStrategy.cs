using Shapes.Canvas;
using Shapes.ShapesParams;

namespace Shapes.ShapesManager.DrawingStrategy;

public class CircleDrawingStrategy : BaseDrawingStrategy
{
    private CircleParams _circleParams;

    public override IShapeParams ShapeParams { get; }

    public CircleDrawingStrategy( CircleParams circleParams )
    {
        ShapeParams = circleParams;
        _circleParams = circleParams;
    }

    public override void Draw( ICanvas canvas )
    {
        var color = _circleParams.Color;
        var center = _circleParams.Vertices[ 0 ];
        var radius = _circleParams.Radius;

        canvas.SetColor( color );
        canvas.DrawEllipse( center, radius, radius );
    }

    public override string GetInfo()
    {
        string finalString = "circle - color: #" +
            _circleParams.Color +
            " - center: " +
            _circleParams.Vertices[ 0 ].ToString() +
            " - radius: " +
            _circleParams.Radius;

        return finalString;
    }
}