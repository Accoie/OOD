using Slides.Types;

namespace Slides.Styles.FillStyles
{
    public class FillStyle : IFillStyle
    {
        private RGBAColor _color;
        private bool _enabled;

        public FillStyle( RGBAColor color, bool enabled )
        {
            _color = color;
            _enabled = enabled;
        }

        public bool? IsEnabled()
        {
            return _enabled;
        }

        public RGBAColor? GetColor()
        {
            return _color;
        }

        public void SetColor( RGBAColor color )
        {
            _color = color;
        }
    }
}