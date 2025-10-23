using Slides.Types;

namespace Slides.Styles.LineStyles
{
    public class CompositeLineStyle : ILineStyle
    {
        private ILineStyleEnumerator _lineStyles;

        public CompositeLineStyle( ILineStyleEnumerator lineStyles )
        {
            _lineStyles = lineStyles;
        }

        public RGBAColor? GetColor()
        {
            if ( _lineStyles.EnumerateAll().Count == 0 )
            {
                return null;
            }

            ILineStyle firstElement = _lineStyles.EnumerateAll().First();

            foreach ( ILineStyle style in _lineStyles.EnumerateAll() )
            {
                if ( style.GetColor() != firstElement.GetColor() )
                {
                    return null;
                }
            }

            return firstElement.GetColor();
        }

        public int GetLineWidth()
        {
            if ( _lineStyles.EnumerateAll().Count == 0 )
            {
                return 0;
            }

            int firstElementEnabled = _lineStyles.EnumerateAll().First().GetLineWidth();

            foreach ( ILineStyle style in _lineStyles.EnumerateAll() )
            {
                if ( style.GetLineWidth() != firstElementEnabled )
                {
                    return 0;
                }
            }

            return firstElementEnabled;
        }

        public bool? IsEnabled()
        {
            if ( _lineStyles.EnumerateAll().Count == 0 )
            {
                return null;
            }

            bool? firstElementEnabled = _lineStyles.EnumerateAll().First().IsEnabled();

            foreach ( ILineStyle style in _lineStyles.EnumerateAll() )
            {
                if ( style.IsEnabled() != firstElementEnabled )
                {
                    return null;
                }
            }

            return firstElementEnabled;
        }

        public void SetColor( RGBAColor color )
        {
            _lineStyles.EnumerateAll().ForEach( x => x.SetColor( color ) );
        }
    }
}
