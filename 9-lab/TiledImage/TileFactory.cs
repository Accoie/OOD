using TiledImage.Types;

namespace TiledImage
{
    public static class TileFactory
    {
        public static List<List<ITile>> CreateTiles( Size size, uint color = 0 )
        {
            List<List<ITile>> result = new();
            if ( size.Width <= 0 || size.Height <= 0 )
            {
                throw new ArgumentOutOfRangeException( "Size dimensions must be positive" );
            }

            uint tilesWidth = ( size.Width + Tile.Size - 1 ) / Tile.Size;
            uint tilesHeight = ( size.Height + Tile.Size - 1 ) / Tile.Size;

            result = new List<List<ITile>>( ( int )tilesHeight );

            CopyOnWrite prototypeTile = CopyOnWrite.CreateShared( color );

            for ( int y = 0; y < tilesHeight; y++ )
            {
                List<ITile> row = new( ( int )tilesWidth );
                for ( int x = 0; x < tilesWidth; x++ )
                {
                    row.Add( prototypeTile.CreateSharedCopy() );
                }
                result.Add( row );
            }

            return result;
        }
    }
}
