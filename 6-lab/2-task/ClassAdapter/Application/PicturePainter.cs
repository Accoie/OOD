using ObjectAdapter.GraphicsLib;
using ObjectAdapter.ModernGraphicsLib;
using ObjectAdapter.ShapeDrawingLib;

namespace ObjectAdapter.Application
{
    public static class PicturePainter
    {
        private static void PaintPicture( CanvasPainter painter )
        {
            CTriangle triangle = new CTriangle(
                new ShapeDrawingLib.Point( 10, 15 ),
                new ShapeDrawingLib.Point( 100, 200 ),
                new ShapeDrawingLib.Point( 150, 250 )
            );

            CRectangle rectangle = new CRectangle(
                new ShapeDrawingLib.Point( 30, 40 ), 18, 24
            );

            painter.Draw( triangle );
            painter.Draw( rectangle );
        }

        public static void PaintPictureOnCanvas()
        {
            CCanvas simpleCanvas = new CCanvas();
            CanvasPainter painter = new CanvasPainter( simpleCanvas );

            PaintPicture( painter );
        }

        public static void PaintPictureOnModernGraphicsRenderer()
        {
            ModernGraphicsRendererAdapter adapter = new();
            CanvasPainter painter = new CanvasPainter( adapter );

            PaintPicture( painter );
        }
    }
}