using PictureFactory.PictureDrafts;

namespace PictureFactory.Designers
{
    public interface IDesigner
    {
        IPictureDraft CreateDraft( Stream stream );
    }
}
