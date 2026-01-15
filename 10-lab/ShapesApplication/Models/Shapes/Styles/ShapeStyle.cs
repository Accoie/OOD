using System.Windows.Media;

namespace ShapesApplication.Models.Shapes.Styles
{
    public class ShapeStyle
    {
        public Brush FillColor { get; set; }
        public Brush StrokeColor { get; set; }
        public int StrokeWidth { get; set; }

        public static ShapeStyle UseDefault()
        {
            return new ShapeStyle { FillColor = RandomColorGenerator.GetRandomColor(), StrokeColor = Brushes.Black, StrokeWidth = 1 };
        }

        public static ShapeStyle UseOnlyStroke()
        {
            return new ShapeStyle { FillColor = Brushes.Transparent, StrokeColor = Brushes.Black, StrokeWidth = 1 };
        }
    }
}
