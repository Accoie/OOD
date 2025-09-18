namespace Shapes.ShapesManager.Commands;

public class ListCommand : IShapeCommand
{
    public CommandContext Context { get; }

    public ListCommand( CommandContext commandContext )
    {
        Context = commandContext;
    }

    public void Execute( string[] args )
    {
        Console.WriteLine( Context.Picture.GetShapesInfo() );
    }
}