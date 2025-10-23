using Slides.Types;

namespace Slides.Styles.LineStyles
{
    public class LineStyle : ILineStyle
    {
        private RGBAColor _color;
        private int _lineWidth;
        private bool _enabled;

        public LineStyle( RGBAColor color, int lineWidth, bool enabled )
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

        public int GetLineWidth()
        {
            return _lineWidth;
        }
    }
}