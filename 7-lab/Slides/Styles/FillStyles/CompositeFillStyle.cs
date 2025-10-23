using Slides.Types;

namespace Slides.Styles.FillStyles
{
    public class CompositeFillStyle : IFillStyle
    {
        private IFillStyleEnumerator _fillStyles;

        public CompositeFillStyle( IFillStyleEnumerator styles )
        {
            _fillStyles = styles;
        }

        public RGBAColor? GetColor()
        {
            if ( _fillStyles.EnumerateAll().Count == 0 )
            {
                return null;
            }

            IFillStyle firstElement = _fillStyles.EnumerateAll().First();

            foreach ( IFillStyle style in _fillStyles.EnumerateAll() )
            {
                if ( style.GetColor() != firstElement.GetColor() )
                {
                    return null;
                }
            }

            return firstElement.GetColor();
        }

        public bool? IsEnabled()
        {
            if ( _fillStyles.EnumerateAll().Count == 0 )
            {
                return null;
            }

            bool? firstElementEnabled = _fillStyles.EnumerateAll().First().IsEnabled();

            foreach ( IFillStyle style in _fillStyles.EnumerateAll() )
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
            _fillStyles.EnumerateAll().ForEach( x => x.SetColor( color ) );
        }
    }
}