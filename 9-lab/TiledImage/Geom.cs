using TiledImage.Types;

namespace TiledImage
{
    public static class Geom
    {
        public static bool IsPointInSize( Point p, Size size )
        {
            return p.X >= 0 && p.Y >= 0 && p.X < size.Width && p.Y < size.Height;
        }
    }
}