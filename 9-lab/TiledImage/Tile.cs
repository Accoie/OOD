using System;

namespace TiledImage
{
    public class Tile : IDisposable
    {
        public const int SIZE = 8;
        private static int _instanceCount = 0;
        private uint[] _pixels;

        public Tile()
        {
            _pixels = new uint[ SIZE * SIZE ];
            Array.Fill( _pixels, 0xFFFFFFu );
            _instanceCount++;
        }

        public Tile( uint color )
        {
            _pixels = new uint[ SIZE * SIZE ];
            Array.Fill( _pixels, color );
            _instanceCount++;
        }

        public Tile( Tile other )
        {
            if ( other == null )
                throw new ArgumentNullException( nameof( other ) );

            _pixels = new uint[ SIZE * SIZE ];
            Array.Copy( other._pixels, _pixels, other._pixels.Length );
            _instanceCount++;
        }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( disposing )
            {
                _instanceCount--;
            }
        }

        public void SetPixel( Point p, uint color )
        {
            if ( p.X >= 0 && p.X < SIZE && p.Y >= 0 && p.Y < SIZE )
            {
                _pixels[ p.Y * SIZE + p.X ] = color;
            }
        }

        public uint GetPixel( Point p )
        {
            if ( p.X >= 0 && p.X < SIZE && p.Y >= 0 && p.Y < SIZE )
            {
                return _pixels[ p.Y * SIZE + p.X ];
            }

            return 0xFFFFFF;
        }

        public static int GetInstanceCount()
        {
            return _instanceCount;
        }

        public void SetPixel( int x, int y, uint color )
        {
            SetPixel( new Point( x, y ), color );
        }

        public uint GetPixel( int x, int y )
        {
            return GetPixel( new Point( x, y ) );
        }

        public uint[] Pixels => _pixels;
    }
}