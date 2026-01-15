using TiledImage.Types;

namespace TiledImage
{
    public class Tile : ITile
    {
        private readonly uint[,] _pixels = new uint[ Size, Size ];

        public const int Size = 8;

        public Tile( uint color = 0 )
        {
            for ( int y = 0; y < Size; y++ )
                for ( int x = 0; x < Size; x++ )
                    _pixels[ y, x ] = color;
        }

        public Tile( Tile other )
        {
            for ( int y = 0; y < Size; y++ )
                for ( int x = 0; x < Size; x++ )
                    _pixels[ y, x ] = other._pixels[ y, x ];
        }

        public uint GetPixel( Point point )
        {
            return CheckPointInSize( point ) ? _pixels[ point.Y, point.X ] : 0;
        }

        public void SetPixel( Point point, uint color )
        {
            if ( CheckPointInSize( point ) )
            {
                _pixels[ point.Y, point.X ] = color;
            }
        }

        public Tile Clone() => new Tile( this );

        private static bool CheckPointInSize( Point point )
        {
            return Geom.IsPointInSize( point, new Size( Size, Size ) );
        }
    }
}
