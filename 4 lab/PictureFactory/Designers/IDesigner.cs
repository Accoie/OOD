using PictureFactory.PictureDrafts;

namespace PictureFactory.Designers
{
    public interface IDesigner
    {
        PictureDraft CreateDraft( Stream stream );
    }
}
