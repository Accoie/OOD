using TiledImage.Types;

namespace TiledImage;

public class Image
{
    private readonly List<List<ITile>> _tiles;

    public Size Size { get; }

    public Image( Size size, uint color = 0 )
    {
        Size = size;
        _tiles = TileFactory.CreateTiles( size, color );
    }

    public uint GetPixel( Point p )
    {
        if ( !Geom.IsPointInSize( p, Size ) )
        {
            throw new ArgumentOutOfRangeException( $"GetPixel: point {p} is out of range" );
        }

        int tileX = p.X / Tile.Size;
        int tileY = p.Y / Tile.Size;
        Point pixel = new( p.X % Tile.Size, p.Y % Tile.Size );

        return _tiles[ tileY ][ tileX ].GetPixel( pixel );
    }

    public void SetPixel( Point p, uint color )
    {
        if ( !Geom.IsPointInSize( p, Size ) )
        {
            throw new ArgumentOutOfRangeException( $"SetPixel: point {p} is out of range" );
        }

        int tileX = p.X / Tile.Size;
        int tileY = p.Y / Tile.Size;
        Point pixel = new( p.X % Tile.Size, p.Y % Tile.Size );

        _tiles[ tileY ][ tileX ].SetPixel( pixel, color );
    }
}
