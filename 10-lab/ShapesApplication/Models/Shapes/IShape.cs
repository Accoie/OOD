using System;
using System.Windows;
using System.Windows.Media;
using ShapesApplication.Models.Shapes.Styles;

namespace ShapesApplication.Models.Shapes
{
    public interface IShape
    {
        Guid Id { get; }
        ShapeType Type { get; }
        Rect Bounds { get; }
        ShapeStyle Style { get; }
        bool IsSelected { get; }
        Geometry GetGeometry();
    }
}
