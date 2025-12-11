using Editor.Documents;
using Editor.Documents.Items;
using Editor.Image;

namespace Editor.Commands;

public class ResizeImageCommand : AbstractCommand
{
    private readonly int _width;
    private readonly int _height;
    private int _previousWidth;
    private int _previousHeight;

    public int Position { get; }

    public ResizeImageCommand( IDocument document, int width, int height, int position ) : base( document )
    {
        _width = width;
        _height = height;
        Position = position;
    }

    protected override void DoExecute()
    {
        IImage image = GetImageAtPosition();
        _previousWidth = image.Width;
        _previousHeight = image.Height;
        image.Resize( _width, _height );
    }

    protected override void DoUnexecute()
    {
        IImage image = GetImageAtPosition();
        image.Resize( _previousWidth, _previousHeight );
    }

    private IImage GetImageAtPosition()
    {
        if ( _document.GetItemsCount() <= Position )
        {
            throw new ArgumentException( "Incorrect item position" );
        }

        DocumentItem documentItem = _document.GetItem( Position );

        if ( documentItem.Image is null )
        {
            throw new InvalidOperationException( "Item in the position is not an image" );
        }

        return documentItem.Image;
    }
}
