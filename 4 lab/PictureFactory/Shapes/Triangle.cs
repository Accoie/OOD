using PictureFactory.Canvases;
using PictureFactory.Shapes.Params;
using PictureFactory.Types;

namespace PictureFactory.Shapes
{
    public class Triangle : Shape
    {
        public Point FirstVertex { get; private set; }
        public Point SecondVertex { get; private set; }
        public Point ThirdVertex { get; private set; }

        public Triangle( TriangleParams @params ) : base( @params.Color )
        {
            FirstVertex = @params.FirstVertex;
            SecondVertex = @params.SecondVertex;
            ThirdVertex = @params.ThirdVertex;
        }

        public override void Draw( ICanvas canvas )
        {
            canvas.SetColor( Color );

            canvas.DrawLine( FirstVertex, SecondVertex );
            canvas.DrawLine( SecondVertex, ThirdVertex );
            canvas.DrawLine( ThirdVertex, FirstVertex );
        }

        public override ShapeType GetShapeType()
        {
            return ShapeType.Triangle;
        }
    }
}