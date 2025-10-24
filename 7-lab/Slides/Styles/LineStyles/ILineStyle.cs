using Slides.Types;

namespace Slides.Styles.LineStyles
{
    public interface ILineStyle
    {
        bool? IsEnabled();
        int GetLineWidth();
        RGBAColor? GetColor();
        void SetColor( RGBAColor color );
        void SetLineWidth( int width );
    }
}