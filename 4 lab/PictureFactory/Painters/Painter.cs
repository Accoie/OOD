using PictureFactory.Canvases;
using PictureFactory.PictureDrafts;
using PictureFactory.Shapes;

namespace PictureFactory.Painters
{
    public class Painter : IPainter
    {
        public void DrawPicture( IPictureDraft draft, ICanvas canvas )
        {
            for ( int i = 0; i < draft.GetShapesSize(); i++ )
            {
                Shape shape = draft.GetShape( i );
                Console.WriteLine( $"Drawing {shape.GetShapeType()}:" );
                shape.Draw( canvas );
                Console.WriteLine();
            }
        }
    }
}