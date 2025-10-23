using Slides.Canvas;
using Slides.Shapes;

namespace Slides.Slides
{
    public interface ISlide
    {
        void AddShape( IShape shape );

        void Draw( ICanvas canvas );
    }
}
