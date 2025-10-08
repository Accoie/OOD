using PictureFactory.Shapes;
using PictureFactory.Shapes.Params;

namespace PictureFactory.Factories
{
    public class ShapeFactory : IShapeFactory
    {
        ShapeFactoryParser _parser = new();

        public Shape CreateShape( string descr )
        {
            ShapeParams shapeParams = _parser.ParseShapeParams( descr );

            return shapeParams.ShapeType switch
            {
                ShapeType.RegularPolygon => new RegularPolygon( ( RegularPolygonParams )shapeParams ),
                ShapeType.Ellipse => new Ellipse( ( EllipseParams )shapeParams ),
                ShapeType.Triangle => new Triangle( ( TriangleParams )shapeParams ),
                ShapeType.Rectangle => new Rectangle( ( RectangleParams )shapeParams ),

                _ => throw new ArgumentException( "Unknown shape type!" ),
            };
        }
    }
}
