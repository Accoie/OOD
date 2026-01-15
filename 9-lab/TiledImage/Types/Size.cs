namespace TiledImage.Types
{
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

        public static bool operator ==( Size left, Size right )
        {
            return left.Equals( right );
        }

        public static bool operator !=( Size left, Size right )
        {
            return !( left == right );
        }
    }
}