using System.Windows;
using System.Windows.Media;
using ShapesApplication.Models.Shapes.Styles;

namespace ShapesApplication.Models
{
    public class RectangleShape : Shape
    {
        public RectangleShape( Rect frame, ShapeStyle style ) : base( ShapeType.Rectangle, frame, style )
        {
        }

        public override Geometry GetGeometry()
        {
            return new RectangleGeometry( Bounds );
        }

        public override bool ContainsPoint( Point point )
        {
            return Bounds.Contains( point );
        }
    }
}