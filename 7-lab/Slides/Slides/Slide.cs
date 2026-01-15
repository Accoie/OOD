using Slides.Canvas;
using Slides.Shapes;

namespace Slides.Slides
{
    public class Slide : ISlide
    {
        private List<IShape> _shapes = [];

        public void AddShape( IShape shape )
        {
            _shapes.Add( shape );
        }

        public void Draw( ICanvas canvas )
        {
            _shapes.ForEach( shape => shape.Draw( canvas ) );
        }
    }
}
