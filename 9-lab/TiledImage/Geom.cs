using System;

namespace TiledImage
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point( int x, int y )
        {
            X = x;
            Y = y;
        }
    }

    public struct Size
    {
        public uint Width { get; set; }
        public uint Height { get; set; }

        public Size( uint width, uint height )
        {
            Width = width;
            Height = height;
        }

        public override bool Equals( object obj )
        {
            if ( obj is Size other )
            {
                return other.Width == Width && other.Height == Height;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( Width, Height );
        }

        public static bool operator ==( Size left, Size right )
        {
            return left.Equals( right );
        }

        public static bool operator !=( Size left, Size right )
        {
            return !( left == right );
        }
    }

    public static class Geom
    {
        public static bool IsPointInSize( Point p, Size size )
        {
            return p.X >= 0 && p.Y >= 0 && p.X < size.Width && p.Y < size.Height;
        }
    }
}