using Shapes.Types;

namespace Shapes.ShapesParams;

public class TextParams : ShapeParams
{
    public double FontSize { get; set; }
    public string Text { get; set; }

    public TextParams( Point leftTop, double fontSize, string text, string color )
        : base( color, [ leftTop ] )
    {
        FontSize = fontSize;
        Text = text;
    }
}