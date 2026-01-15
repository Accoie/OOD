using System.Windows;
using System.Windows.Media;
using ShapesApplication.Models.Shapes.Styles;

namespace ShapesApplication.Models
{
    public class EllipseShape : Shape
    {
        public EllipseShape( Rect frame, ShapeStyle style ) : base( ShapeType.Ellipse, frame, style )
        {
        }

        public override Geometry GetGeometry()
        {
            return new EllipseGeometry( new Point( Bounds.X + Bounds.Width / 2, Bounds.Y + Bounds.Height / 2 ),
                                      Bounds.Width / 2, Bounds.Height / 2 );
        }

        public override bool ContainsPoint( Point point )
        {
            Point center = new Point( Bounds.X + Bounds.Width / 2, Bounds.Y + Bounds.Height / 2 );
            Geometry geometry = GetGeometry();
            return geometry.FillContains( point ) || geometry.StrokeContains( new Pen( Brushes.Black, 1 ), point );
        }
    }
}