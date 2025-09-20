using Shapes.Types;

namespace Shapes.Canvas;

public interface ICanvas
{
    void SetColor( string color );
    public void FillPolygon( List<Point> points );
    void MoveTo( Point point );
    void LineTo( Point point );
    void DrawEllipse( Point centerPoint, double radiusWidth, double radiusHeight );
    void PrintText( Point leftTop, double size, string text, string color );
}