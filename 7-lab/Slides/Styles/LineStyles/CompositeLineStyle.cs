using Slides.Types;

namespace Slides.Styles.LineStyles
{
    public class CompositeLineStyle : ILineStyle
    {
        private Func<List<ILineStyle>> _getStyles;

        public CompositeLineStyle( Func<List<ILineStyle>> getLineStyles )
        {
            _getStyles = getLineStyles;
        }

        public RGBAColor? GetColor()
        {
            List<ILineStyle> lineStyles = _getStyles();
            if ( lineStyles.Count == 0 )
            {
                return null;
            }

            ILineStyle firstElement = lineStyles.First();
            foreach ( ILineStyle style in lineStyles )
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
            List<ILineStyle> lineStyles = _getStyles();
            if ( lineStyles.Count == 0 )
            {
                return 0;
            }

            int firstElementWidth = lineStyles.First().GetLineWidth();
            foreach ( ILineStyle style in lineStyles )
            {
                if ( style.GetLineWidth() != firstElementWidth )
                {
                    return 0;
                }
            }

            return firstElementWidth;
        }

        public bool? IsEnabled()
        {
            List<ILineStyle> lineStyles = _getStyles();
            if ( lineStyles.Count == 0 )
            {
                return null;
            }

            bool? firstElementEnabled = lineStyles.First().IsEnabled();
            foreach ( ILineStyle style in lineStyles )
            {
                if ( style.IsEnabled() != firstElementEnabled )
                {
                    return null;
                }
            }

            return firstElementEnabled;
        }

        public void SetLineWidth( int width )
        {
            _getStyles().ForEach( x => x.SetLineWidth( width ) );
        }

        public void SetColor( RGBAColor color )
        {
            _getStyles().ForEach( x => x.SetColor( color ) );
        }
    }
}