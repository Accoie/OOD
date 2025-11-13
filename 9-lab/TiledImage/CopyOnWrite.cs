using TiledImage.Types;

namespace TiledImage
{
    public class CopyOnWrite : ITile
    {
        private Tile _tile;
        private ReferencesCounter _referencesCounter;

        private CopyOnWrite( Tile tile, ReferencesCounter counter )
        {
            _tile = tile;
            _referencesCounter = counter;
            _referencesCounter++;
        }

        public static CopyOnWrite CreateShared( uint color = 0 )
        {
            Tile tile = new Tile( color );
            ReferencesCounter counter = new ReferencesCounter();
            return new CopyOnWrite( tile, counter );
        }

        public uint GetPixel( Point point ) => _tile.GetPixel( point );

        public void SetPixel( Point point, uint color )
        {
            if ( _referencesCounter.Count > 1 )
            {
                _referencesCounter--;
                _tile = _tile.Clone();
                _referencesCounter = new ReferencesCounter();
                _referencesCounter++;
            }

            _tile.SetPixel( point, color );
        }

        private class ReferencesCounter
        {
            public int Count { get; private set; }

            public static ReferencesCounter operator --( ReferencesCounter counter )
            {
                counter.Count--;
                return counter;
            }

            public static ReferencesCounter operator ++( ReferencesCounter counter )
            {
                counter.Count++;
                return counter;
            }
        }
    }
}
