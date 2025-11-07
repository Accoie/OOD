namespace TiledImage
{
    public class Image
    {
        public Size Size { get; }

        public uint Height
        {
            get { return Size.Height; }
        }

        private List<List<CoW<Tile>>> _tiles;

        public Image( Size size, uint color = 0xFFFFFF )
        {
            if ( size.Width <= 0 || size.Height <= 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( size ), "Size dimensions must be positive" );
            }

            Size = size;

            uint tilesWidth = ( size.Width + Tile.SIZE - 1 ) / Tile.SIZE;
            uint tilesHeight = ( size.Height + Tile.SIZE - 1 ) / Tile.SIZE;

            _tiles = new List<List<CoW<Tile>>>( ( int )tilesHeight );

            for ( int y = 0; y < tilesHeight; y++ )
            {
                List<CoW<Tile>> row = new List<CoW<Tile>>( ( int )tilesWidth );
                for ( int x = 0; x < tilesWidth; x++ )
                {
                    row.Add( new CoW<Tile>( new Tile( color ) ) );
                }
                _tiles.Add( row );
            }
        }

        public uint GetPixel( Point p )
        {
            if ( !IsPointInSize( p, Size ) )
            {
                return 0xFFFFFF;
            }

            int tileX = p.X / Tile.SIZE;
            int tileY = p.Y / Tile.SIZE;
            int pixelX = p.X % Tile.SIZE;
            int pixelY = p.Y % Tile.SIZE;

            return _tiles[ tileY ][ tileX ].Value.GetPixel( new Point( pixelX, pixelY ) );
        }

        public void SetPixel( Point p, uint color )
        {
            if ( !IsPointInSize( p, Size ) )
            {
                return;
            }

            int tileX = p.X / Tile.SIZE;
            int tileY = p.Y / Tile.SIZE;
            int pixelX = p.X % Tile.SIZE;
            int pixelY = p.Y % Tile.SIZE;

            _tiles[ tileY ][ tileX ].Modify( tile =>
            {
                tile.SetPixel( new Point( pixelX, pixelY ), color );
            } );
        }

        private bool IsPointInSize( Point p, Size size )
        {
            return p.X >= 0 && p.X < size.Width && p.Y >= 0 && p.Y < size.Height;
        }

        public uint GetPixel( int x, int y )
        {
            return GetPixel( new Point( x, y ) );
        }

        public void SetPixel( int x, int y, uint color )
        {
            SetPixel( new Point( x, y ), color );
        }
    }
}