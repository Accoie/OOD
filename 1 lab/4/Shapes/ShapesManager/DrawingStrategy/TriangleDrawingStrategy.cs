using Shapes.Canvas;
using Shapes.ShapesParams;

namespace Shapes.ShapesManager.DrawingStrategy;

public class TriangleDrawingStrategy : BaseDrawingStrategy
{
    public override IShapeParams ShapeParams { get; }

    public TriangleDrawingStrategy( TriangleParams shapeParams )
    {
        ShapeParams = shapeParams;
    }

    public override void Draw( ICanvas canvas )
    {
        canvas.SetColor( ShapeParams.Color );

        canvas.FillPolygon( ShapeParams.Vertices );

        canvas.MoveTo( ShapeParams.Vertices[ 0 ] );
        canvas.LineTo( ShapeParams.Vertices[ 1 ] );
        canvas.LineTo( ShapeParams.Vertices[ 2 ] );
        canvas.LineTo( ShapeParams.Vertices[ 0 ] );
    }

    public override string GetInfo()
    {
        var vertices = ShapeParams.Vertices;

        string finalString = "triangle - color: #" +
            ShapeParams.Color +
            " - firstVertex: " +
            vertices[ 0 ].ToString() +
            " - secondVertex: " +
            vertices[ 1 ].ToString() +
            " - thirdVertex: " +
            vertices[ 2 ].ToString();

        return finalString;
    }
}