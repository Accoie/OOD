using Shapes.Canvas;
using Shapes.ShapesManager.DrawingStrategy;
using Shapes.Types;

namespace Shapes.ShapesManager;

public class Shape
{
    private BaseDrawingStrategy _drawingStrategy;

    public Shape( BaseDrawingStrategy drawingStrategy )
    {
        _drawingStrategy = drawingStrategy;
    }

    public void SetColor( string color )
    {
        _drawingStrategy.ShapeParams.Color = color;
    }

    public string GetColor()
    {
        return _drawingStrategy.ShapeParams.Color;
    }

    public void SetDrawingStrategy( BaseDrawingStrategy newDrawingStrategy )
    {
        _drawingStrategy = newDrawingStrategy;
    }

    public List<Point> GetVertices()
    {
        return _drawingStrategy.ShapeParams.Vertices;
    }

    public void Draw( ICanvas canvas )
    {
        _drawingStrategy.Draw( canvas );
    }

    public void Move( ICanvas canvas, double dx, double dy )
    {
        _drawingStrategy.Move( canvas, dx, dy );
    }

    public string GetInfo()
    {
        return _drawingStrategy.GetInfo();
    }
}