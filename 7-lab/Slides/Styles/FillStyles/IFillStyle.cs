using Slides.Types;

namespace Slides.Styles.FillStyles
{
    public interface IFillStyle
    {
        bool? IsEnabled();
        RGBAColor? GetColor();
        void SetColor( RGBAColor color );
    }
}