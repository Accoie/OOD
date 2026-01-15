using TiledImage.Types;

namespace TiledImage
{
    public interface ITile
    {
        uint GetPixel( Point point );
        void SetPixel( Point point, uint color );
    }
}
