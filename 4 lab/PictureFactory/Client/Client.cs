using PictureFactory.Canvases;
using PictureFactory.Designers;
using PictureFactory.Factories;
using PictureFactory.PictureDrafts;
using PictureFactory.Painters;

namespace PictureFactory.Client
{
    public class Client
    {
        public void MakeOrder()
        {
            IShapeFactory factory = new ShapeFactory();
            IDesigner designer = new Designer( factory );
            ICanvas canvas = new Canvas();
            IPainter painter = new Painter();
            IPictureDraft draft = designer.CreateDraft( Console.OpenStandardInput() );
            painter.DrawPicture( draft, canvas );
        }
    }
}
