using Shapes.Canvas;
using Shapes.ShapesParams;
using Shapes.Types;

namespace Shapes.ShapesManager.DrawingStrategy;

public class RectangleDrawingStrategy : BaseDrawingStrategy
{
    public override IShapeParams ShapeParams { get; }

    public RectangleDrawingStrategy( RectangleParams shapeParams )
    {
        ShapeParams = shapeParams;
    }

    public override void Draw( ICanvas canvas )
    {
        var color = ShapeParams.Color;
        var vertices = ShapeParams.Vertices;

        canvas.SetColor( color );

        var polygonPoints = new List<Point>
        {
            vertices[0], new Point(vertices[1].X, vertices[0].Y),
            vertices[1], new Point(vertices[0].X, vertices[1].Y)
        };

        canvas.FillPolygon( polygonPoints );

        canvas.MoveTo( vertices[ 0 ] );
        canvas.LineTo( new Point( vertices[ 1 ].X, vertices[ 0 ].Y ) );
        canvas.LineTo( new Point( vertices[ 1 ].X, vertices[ 1 ].Y ) );
        canvas.LineTo( new Point( vertices[ 0 ].X, vertices[ 1 ].Y ) );
        canvas.LineTo( vertices[ 0 ] );
    }

    public override string GetInfo()
    {
        var vertices = ShapeParams.Vertices;

        var info = "rectangle - color: #" +
            ShapeParams.Color +
            " - leftTop: " +
            vertices[ 0 ].ToString() +
            " - rightBottom: " +
            vertices[ 1 ].ToString();

        return info;
    }
}