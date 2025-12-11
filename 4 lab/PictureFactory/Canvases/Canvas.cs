using PictureFactory.Types;

namespace PictureFactory.Canvases
{
    public class Canvas : ICanvas
    {
        private Color _color;

        public void SetColor( Color color )
        {
            _color = color;
        }

        public void DrawLine( Point from, Point to )
        {
            Console.WriteLine( $"Drawed line from {from.ToStringFormatted()} to {to.ToStringFormatted()}" );
        }

        public void DrawEllipse( Point center, double radiusX, double radiusY )
        {
            Console.WriteLine( $"Drawed {_color.ToString()} ellipse with center in {center.ToStringFormatted()}," +
                $" radius X axis - {radiusX:F3}, radius Y axis - {radiusY:F3}" );
        }
    }
}