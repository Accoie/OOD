using Shapes.Canvas;
using Shapes.ShapesParams;

namespace Shapes.ShapesManager.DrawingStrategy;

public class TextDrawingStrategy : BaseDrawingStrategy
{
    private TextParams _textParams;

    public override IShapeParams ShapeParams { get; }

    public TextDrawingStrategy( TextParams textParams )
    {
        ShapeParams = textParams;
        _textParams = textParams;
    }

    public override void Draw( ICanvas canvas )
    {
        canvas.PrintText( _textParams.Vertices[ 0 ], _textParams.FontSize, _textParams.Text, _textParams.Color );
    }

    public override string GetInfo()
    {
        string finalString = "text - color: #" +
            _textParams.Color +
            " - leftTop: " +
            _textParams.Vertices[ 0 ].ToString() +
            " - fontSize: " +
            _textParams.FontSize +
            " - text: " +
            _textParams.Text;

        return finalString;
    }
}