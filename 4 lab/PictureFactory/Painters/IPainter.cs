using PictureFactory.Canvases;
using PictureFactory.PictureDrafts;

namespace PictureFactory.Painters
{
    public interface IPainter
    {
        public void DrawPicture( IPictureDraft draft, ICanvas canvas );
    }
}
