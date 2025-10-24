using Slides.Types;

namespace Slides.Styles.FillStyles
{
    public class CompositeFillStyle : IFillStyle
    {
        private Func<List<IFillStyle>> _getStyles;

        public CompositeFillStyle( Func<List<IFillStyle>> getStyles )
        {
            _getStyles = getStyles;
        }

        public RGBAColor? GetColor()
        {
            List<IFillStyle> styles = _getStyles();

            if ( styles.Count == 0 )
            {
                return null;
            }

            IFillStyle firstElement = styles.First();
            foreach ( IFillStyle style in styles )
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
            List<IFillStyle> styles = _getStyles();
            if ( styles.Count == 0 )
            {
                return null;
            }

            bool? firstElementEnabled = styles.First().IsEnabled();
            foreach ( IFillStyle style in styles )
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
            _getStyles().ForEach( x => x.SetColor( color ) );
        }
    }
}