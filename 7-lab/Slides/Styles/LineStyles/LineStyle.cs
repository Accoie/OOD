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
            _lineWidth = lineWidth;
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

        public void SetLineWidth( int lineWidth )
        {
            ArgumentOutOfRangeException.ThrowIfNegative( lineWidth );

            _lineWidth = lineWidth;
        }

        public int GetLineWidth()
        {
            return _lineWidth;
        }
    }
}