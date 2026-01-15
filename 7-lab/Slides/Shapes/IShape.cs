using Slides.Canvas;
using Slides.Styles.FillStyles;
using Slides.Styles.LineStyles;
using Slides.Types;

namespace Slides.Shapes
{
    public interface IShape
    {
        Frame GetFrame();
        void SetFrame( Frame frame );

        ILineStyle GetLineStyle();
        IFillStyle GetFillStyle();

        void SetLineStyle( ILineStyle style );
        void SetFillStyle( IFillStyle style );


        void Draw( ICanvas canvas );
    }
}