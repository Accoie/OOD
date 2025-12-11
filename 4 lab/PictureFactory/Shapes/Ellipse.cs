using PictureFactory.Canvases;
using PictureFactory.Shapes.Params;
using PictureFactory.Types;

namespace PictureFactory.Shapes
{
    public class Ellipse : Shape
    {
        public Point Center { get; private set; }
        public double RadiusX { get; private set; }
        public double RadiusY { get; private set; }

        public Ellipse( EllipseParams @params ) : base( @params.Color )
        {
            Center = @params.Center;
            RadiusX = @params.RadiusX;
            RadiusY = @params.RadiusY;
        }

        public override void Draw( ICanvas canvas )
        {
            canvas.SetColor( Color );

            canvas.DrawEllipse( Center, RadiusX, RadiusY );
        }

        public override ShapeType GetShapeType()
        {
            return ShapeType.Ellipse;
        }
    }
}