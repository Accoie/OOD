using PictureFactory.Shapes;

namespace PictureFactory.PictureDrafts
{
    public interface IPictureDraft
    {
        public int GetShapesSize();
        public Shape GetShape( int index );
    }
}
