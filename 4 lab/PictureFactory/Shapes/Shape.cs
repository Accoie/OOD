using PictureFactory.Canvases;
using PictureFactory.Shapes.Params;
using PictureFactory.Types;

namespace PictureFactory.Shapes
{
    public abstract class Shape
    {
        public Color Color { get; private set; }

        public Shape( Color color )
        {
            Color = color;
        }

        public abstract void Draw( ICanvas canvas );

        public abstract ShapeType GetShapeType();
    }
}