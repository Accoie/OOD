using PictureFactory.Types;

namespace PictureFactory.Canvases
{
    public interface ICanvas
    {
        void SetColor( Color color );
        void DrawLine( Point from, Point to );
        void DrawEllipse( Point center, double radiusX, double radiusY );
    }
}