using Slides.Types;

namespace Slides.Canvas
{
    public interface ICanvas
    {
        void DrawLine( Point from, Point to );
        void DrawEllipse( Point center, double radiusX, double radiusY );

        void FillEllipse( Point center, double radiusX, double radiusY );
        void FillPolygon( Point[] points );

        void SetFillColor( RGBAColor color );
        void SetLineColor( RGBAColor color );
        void SetLineWidth( double width );

        void Save( string path );
    }
}
