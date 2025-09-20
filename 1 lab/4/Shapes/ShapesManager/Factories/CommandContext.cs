namespace Shapes.ShapesManager.Factories;

public class CommandContext
{
    public Picture Picture { get; }
    public ShapeParser ShapeParser { get; }

    public CommandContext(Picture picture, ShapeParser shapeParser)
    {
        Picture = picture;
        ShapeParser = shapeParser;
    }
}