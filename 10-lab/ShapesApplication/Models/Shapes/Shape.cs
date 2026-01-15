using System;
using System.Windows;
using System.Windows.Media;
using ShapesApplication.Models.Shapes;
using ShapesApplication.Models.Shapes.Styles;

namespace ShapesApplication.Models
{
    public abstract class Shape : IShape
    {
        protected Shape( ShapeType type, Rect frame, ShapeStyle style )
        {
            Type = type;
            Bounds = frame;
            Style = style;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public ShapeType Type { get; }
        public Rect Bounds { get; set; }
        public ShapeStyle Style { get; set; }
        public bool IsSelected { get; set; }

        public abstract Geometry GetGeometry();
        public abstract bool ContainsPoint( Point point );

        public virtual void Move( Vector offset )
        {
            Bounds = new Rect( Bounds.X + offset.X, Bounds.Y + offset.Y, Bounds.Width, Bounds.Height );
        }

        public virtual void Resize( Point anchorPoint, Point newPoint )
        {
            double x = Math.Min( anchorPoint.X, newPoint.X );
            double y = Math.Min( anchorPoint.Y, newPoint.Y );
            double width = Math.Abs( newPoint.X - anchorPoint.X );
            double height = Math.Abs( newPoint.Y - anchorPoint.Y );

            Bounds = new Rect( x, y, width, height );
        }
    }
}